using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseItemController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item thisItem;

    public Text itemNameText;
    public Text itemDescriptionText;

    protected bool isMouseOverItem;
    public GameObject DescriptionPanel;

    protected virtual void Start()
    {
        isMouseOverItem = false; //不會一開始就被摸到
        DescriptionPanel.SetActive(false); //初始面板設為不可見

        //抓取元素
        itemNameText = DescriptionPanel.transform.Find("ItemName").GetComponent<Text>();
        itemDescriptionText = DescriptionPanel.transform.Find("ItemDescription").GetComponent<Text>();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        // 滑鼠懸停事件
        isMouseOverItem = true;

    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        // 滑鼠離開事件
        isMouseOverItem = false;
    }

    protected virtual void Update()
    {
        if (isMouseOverItem)
        {
            DescriptionPanel.SetActive(true);
            itemNameText.text = thisItem.itemName;
            itemDescriptionText.text = thisItem.itemDescript;
        }
        else
        {
            DescriptionPanel.SetActive(false);
        }
    }

    public void SetItem(Item newItem)
    {
        thisItem = newItem;
    }
}
