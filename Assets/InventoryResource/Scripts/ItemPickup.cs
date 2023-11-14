using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
//using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
//using static UnityEditor.Progress;

public class ItemPickup : MonoBehaviour
{
    public Item itemToAdd;


    public void Pickup()
    {
        //玩家活著的時候才能拾取道具
        if (!GameManager.Instance.GetIsDead())
        {
            //如果為消耗品，檢查是否已存在相同道具
            if (itemToAdd.GetItemType() == Item.ItemType.Consumable)
            {
                ConsumableItem consumableItemToAdd = itemToAdd as ConsumableItem; //把Item轉換成ConsumableItem

                ConsumableItem existingItem = InventoryManager.Instance.Items.Find(item => item.id == itemToAdd.id) as ConsumableItem; //嘗試搜尋有沒有已存在的背包道具

                if (existingItem != null)
                {
                    ChatManager.Instance.SystemMessage($"你拾取了{itemToAdd.itemName}。");
                    existingItem.itemCounts[itemToAdd.itemName]++; //僅增加數量
                    Destroy(gameObject);
                }
                else
                {
                    if (InventoryManager.Instance.Items.Count < InventoryManager.Instance.maxCapacity) //背包還能裝的話才能撿
                    {
                        consumableItemToAdd.itemCounts[itemToAdd.itemName] = 1;
                        ChatManager.Instance.SystemMessage($"你拾取了{itemToAdd.itemName}。");
                        InventoryManager.Instance.AddItem(itemToAdd);
                        Destroy(gameObject);
                    }
                    else
                    {
                        ChatManager.Instance.SystemMessage($"<color=#CC0000>背包已滿!</color>\n");
                    }
                }
            }

            //若是消耗品以外的東西都不疊加

            if (itemToAdd.GetItemType() != Item.ItemType.Consumable)
            {
                if (InventoryManager.Instance.Items.Count < InventoryManager.Instance.maxCapacity)
                {
                    ChatManager.Instance.SystemMessage($"你拾取了{itemToAdd.itemName}。");
                    InventoryManager.Instance.AddItem(itemToAdd);
                    Destroy(gameObject);
                }
                else
                {
                    ChatManager.Instance.SystemMessage($"<color=#CC0000>背包已滿!</color>\n");
                }
            }
            InventoryManager.Instance.UpdateList();
        }

    }
}
