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

    private float screenHeightThreshold = 0.7f; // 螢幕高度的閾值
    private float screenWidthThreshold = 0.8f; // 螢幕寬度的閾值


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
        if (thisItem == null)
        {
            itemNameText.text = null;
            itemDescriptionText.text = null;
        }
        if (isMouseOverItem && thisItem != null)
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

    protected void FollowMousePos()
    {
        Vector3 mouseScreenPos = Input.mousePosition;

        // 將滑鼠螢幕座標轉換成世界座標
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 10f));

        // 將世界座標轉換成螢幕座標，只取 x 軸值
        float mouseScreenX = Camera.main.WorldToScreenPoint(mouseWorldPos).x;

        // 將世界座標轉換成螢幕座標，只取 y 軸值
        float mouseScreenY = Camera.main.WorldToScreenPoint(mouseWorldPos).y;

        // 判斷滑鼠是否位於螢幕的某處之上
        bool isAboveThreshold = mouseScreenY > Screen.height * screenHeightThreshold;

        // 判斷滑鼠是否位於螢幕的右側
        bool isOnRightSide = mouseScreenX > Screen.width * screenWidthThreshold;

        if (isMouseOverItem)
        {
            if (isAboveThreshold)
            {
                if (!isOnRightSide)
                {
                    DescriptionPanel.transform.position = Input.mousePosition + new Vector3(250, -200, 0);
                }
                else
                {
                    DescriptionPanel.transform.position = Input.mousePosition + new Vector3(-250, -200, 0);
                }
            }
            else
            {
                if (!isOnRightSide)
                {
                    DescriptionPanel.transform.position = Input.mousePosition + new Vector3(250, 100, 0);
                }
                else
                {
                    DescriptionPanel.transform.position = Input.mousePosition + new Vector3(-250, 100, 0);
                }

            }
        }
    }
}
