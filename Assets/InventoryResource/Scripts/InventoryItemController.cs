using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemController : BaseItemController, IPointerEnterHandler, IPointerExitHandler
{
    public float screenHeightThreshold = 0.7f; // �ù����ת��H��
    public float screenWidthThreshold = 0.8f; // �ù��e�ת��H��

    protected override void Update()
    {
        if (Input.GetMouseButtonDown(1) && isMouseOverItem) //�ڧ�ϥιD�㪺Ĳ�o�覡�令�k���I���F
        {
            UseItem();
        }

        base.Update(); //�e����BaseItemController���F��
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

    public void UseItem()
    {
        if (thisItem.GetItemType() == Item.ItemType.Weapon)
        {
            WeaponItem itemToUse = thisItem as WeaponItem;
            ChatManager.Instance.SystemMessage($"�˳ƹD��<color=#F5EC3D>{itemToUse.itemName}�C</color>\n");
        }
        else if (thisItem.GetItemType() == Item.ItemType.Consumable)
        {
            ConsumableItem itemToUse = thisItem as ConsumableItem;
            switch (itemToUse.consumableType)
            {
                case ConsumableItem.ConsumableType.Health:
                    Player.Instance.IncreaseHealth(itemToUse.itemValue);
                    break;

                case ConsumableItem.ConsumableType.Magic:
                    Player.Instance.IncreaseMp(itemToUse.itemValue);
                    break;

                case ConsumableItem.ConsumableType.Exp:
                    Player.Instance.IncreaseExp(itemToUse.itemValue);
                    break;

            }

        }

        RemoveItem();

    }

    public void RemoveItem()
    {
        switch (thisItem.GetItemType())
        {
            case Item.ItemType.Weapon:
                InventoryManager.Instance.RemoveItem(thisItem);
                Destroy(gameObject);
                break;

            case Item.ItemType.Consumable:
                ConsumableItem itemToRemove = thisItem as ConsumableItem;
                InventoryManager.Instance.RemoveItem(thisItem);

                if (itemToRemove.itemCounts[thisItem.itemName] == 0)
                {
                    Destroy(gameObject);
                }
                break;
        }
    }
}
