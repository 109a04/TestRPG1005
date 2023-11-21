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
        //���a���F�������F
        if (GameManager.Instance.GetIsDead()) return;

        //�Y�B�����D��O���ӫ~�A�h�ˬd�I�]�O�_���ۦP�D��A�p�G�ۦP�����[���κި�L����
        if (itemToAdd.GetItemType() == Item.ItemType.Consumable)
        {
            //��Item�ഫ��ConsumableItem
            consumableItemToAdd = itemToAdd as ConsumableItem;
            //���շj�M���S���w�s�b���I�]�D��
            existingItem = InventoryManager.Instance.Items.Find(item => item.id == itemToAdd.id) as ConsumableItem;
            if (existingItem != null)
            {
                existingItem.itemCounts[itemToAdd.itemName]++; //�ȼW�[�ƶq
            }
        }

        //�D��ƶq�w�F�̤j�W����
        if (InventoryManager.Instance.Items.Count == InventoryManager.Instance.maxCapacity) 
        {
            if (itemToAdd.GetItemType() == Item.ItemType.Consumable)
            {
                if (existingItem != null) return; //���D�O�w�s�b�b�I�]�������ӫ~
            }
            
            //�_�h�@�߸T��B��
            ChatManager.Instance.SystemMessage($"<color=#CC0000>�I�]�w��!</color>\n");
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
        ChatManager.Instance.SystemMessage($"�A�B���F<color=#F5EC3D>{itemToAdd.itemName}</color>�C");
        Destroy(gameObject);
        InventoryManager.Instance.UpdateList();
    }
}



