using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemController : BaseItemController, IPointerEnterHandler, IPointerExitHandler
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update(); //�e����BaseItemController���F��

        if (Input.GetMouseButtonDown(1) && isMouseOverItem) //�ڧ�ϥιD�㪺Ĳ�o�覡�令�k���I���F
        {
            UseItem();
        }

        FollowMousePos();

    }

    public void UseItem()
    {
        if (thisItem.GetItemType() == Item.ItemType.Weapon)
        {
            WeaponItem itemToUse = thisItem as WeaponItem;
            EquipmentManager.Instance.Equip(itemToUse); //��Z����ƶǵ��޲z�˳Ƹ}��
            EquipmentManager.Instance.UpdateSlot();
            ChatManager.Instance.SystemMessage($"�˳ƪZ��<color=#F5EC3D>{itemToUse.itemName}</color>�C\n");
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
