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
        isMouseOverItem = false; //���|�@�}�l�N�Q�N��
        DescriptionPanel.SetActive(false); //��l���O�]�����i��

        //�������
        itemNameText = DescriptionPanel.transform.Find("ItemName").GetComponent<Text>();
        itemDescriptionText = DescriptionPanel.transform.Find("ItemDescription").GetComponent<Text>();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        // �ƹ��a���ƥ�
        isMouseOverItem = true;

    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        // �ƹ����}�ƥ�
        isMouseOverItem = false;
    }

    protected virtual void Update()
    {
        if (thisItem == null) return; //��U�S���D���Ʈɤ��ݭn�������
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
