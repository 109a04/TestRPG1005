using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponItemController : BaseItemController
{
    public static WeaponItemController Instance;
    public Transform playerHand;

    public float screenHeightThreshold = 0.7f; // �ù����ת��H��
    public float screenWidthThreshold = 0.8f; // �ù��e�ת��H��


    private void Awake()
    {
        Instance = this;
    }

    private Image itemIcon;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        itemIcon = transform.Find("ItemIcon").GetComponent<Image>(); //�Z���ϥ�

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (thisItem == null)
        {
            Debug.Log("�Z������");
            itemIcon.gameObject.SetActive(false); //���ùϥ�
        }
        else
        {
            itemIcon.sprite = thisItem.itemIcon;
            itemIcon.gameObject.SetActive(true); //�_�h��ܹϥ�
        }

        if (Input.GetMouseButtonDown(1) && isMouseOverItem)
        {
            //�����Z���^�I�]
            InventoryManager.Instance.AddItem(thisItem); //��Z���D���^�h
            ChatManager.Instance.SystemMessage($"���U�Z��<color=#F5EC3D>{thisItem.itemName}�C</color>\n");
            thisItem = null;
        }

        //�o�@�j��u���n��A���ᦳ�ɶ��|²��
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

    public override void OnPointerEnter(PointerEventData eventData)
    {
        // �b�o�̹�{�ƹ��a���ƥ�A�A�i�H���g�Ϊ̽եΰ�������k
        base.OnPointerEnter(eventData);
        // �Z���S�����ƹ��a���޿�
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        // �b�o�̹�{�ƹ����}�ƥ�A�A�i�H���g�Ϊ̽եΰ�������k
        base.OnPointerExit(eventData);
    }

    public void ChangeWeapon(Item newWeaponItem)
    {
        Debug.Log("�����Z��");
        if (thisItem == null) //�p�G��e�S������˳�
        {
            thisItem = newWeaponItem; //�������W�˳�
        }
        else
        {
            InventoryManager.Instance.AddItem(thisItem); //��Z���D���^�h
            thisItem = newWeaponItem;
        }
    }

}
