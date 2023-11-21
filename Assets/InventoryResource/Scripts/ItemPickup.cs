using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
//using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
//using static UnityEditor.Progress;

public class ItemPickup : MonoBehaviour
{
    public Item itemToAdd;
    ConsumableItem consumableItemToAdd;
    ConsumableItem existingItem;

    public void Pickup()
    {
        //玩家死了直接不幹
        if (GameManager.Instance.GetIsDead()) return;

        //若拾取的道具是消耗品，則檢查背包是否有相同道具，如果相同直接加不用管其他條件
        if (itemToAdd.GetItemType() == Item.ItemType.Consumable)
        {
            //把Item轉換成ConsumableItem
            consumableItemToAdd = itemToAdd as ConsumableItem;
            //嘗試搜尋有沒有已存在的背包道具
            existingItem = InventoryManager.Instance.Items.Find(item => item.id == itemToAdd.id) as ConsumableItem;
            if (existingItem != null)
            {
                existingItem.itemCounts[itemToAdd.itemName]++; //僅增加數量
            }
        }

        //道具數量已達最大上限時
        if (InventoryManager.Instance.Items.Count == InventoryManager.Instance.maxCapacity) 
        {
            if (itemToAdd.GetItemType() == Item.ItemType.Consumable)
            {
                if (existingItem != null) return; //除非是已存在在背包內的消耗品
            }
            
            //否則一律禁止拾取
            ChatManager.Instance.SystemMessage($"<color=#CC0000>背包已滿!</color>\n");
            return;
        }

        AddItemToBag();

        if (itemToAdd.GetItemType() != Item.ItemType.Consumable)
        {
            InventoryManager.Instance.AddItem(itemToAdd);
        }
        else if (existingItem == null) 
        {
            consumableItemToAdd.itemCounts[itemToAdd.itemName] = 1;
            InventoryManager.Instance.AddItem(itemToAdd);
        }
        
    }

    public void AddItemToBag()
    {
        ChatManager.Instance.SystemMessage($"你拾取了<color=#F5EC3D>{itemToAdd.itemName}</color>。");
        Destroy(gameObject);
        InventoryManager.Instance.UpdateList();
    }
}



