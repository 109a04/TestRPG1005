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

    private float screenHeightThreshold = 0.7f; // �ù����ת��H��
    private float screenWidthThreshold = 0.8f; // �ù��e�ת��H��


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

        // �N�ƹ��ù��y���ഫ���@�ɮy��
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 10f));

        // �N�@�ɮy���ഫ���ù��y�СA�u�� x �b��
        float mouseScreenX = Camera.main.WorldToScreenPoint(mouseWorldPos).x;

        // �N�@�ɮy���ഫ���ù��y�СA�u�� y �b��
        float mouseScreenY = Camera.main.WorldToScreenPoint(mouseWorldPos).y;

        // �P�_�ƹ��O�_���ù����Y�B���W
        bool isAboveThreshold = mouseScreenY > Screen.height * screenHeightThreshold;

        // �P�_�ƹ��O�_���ù����k��
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
