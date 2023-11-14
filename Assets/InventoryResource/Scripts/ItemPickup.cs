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
        //���a���۪��ɭԤ~��B���D��
        if (!GameManager.Instance.GetIsDead())
        {
            //�p�G�����ӫ~�A�ˬd�O�_�w�s�b�ۦP�D��
            if (itemToAdd.GetItemType() == Item.ItemType.Consumable)
            {
                ConsumableItem consumableItemToAdd = itemToAdd as ConsumableItem; //��Item�ഫ��ConsumableItem

                ConsumableItem existingItem = InventoryManager.Instance.Items.Find(item => item.id == itemToAdd.id) as ConsumableItem; //���շj�M���S���w�s�b���I�]�D��

                if (existingItem != null)
                {
                    ChatManager.Instance.SystemMessage($"�A�B���F{itemToAdd.itemName}�C");
                    existingItem.itemCounts[itemToAdd.itemName]++; //�ȼW�[�ƶq
                    Destroy(gameObject);
                }
                else
                {
                    if (InventoryManager.Instance.Items.Count < InventoryManager.Instance.maxCapacity) //�I�]�ٯ�˪��ܤ~���
                    {
                        consumableItemToAdd.itemCounts[itemToAdd.itemName] = 1;
                        ChatManager.Instance.SystemMessage($"�A�B���F{itemToAdd.itemName}�C");
                        InventoryManager.Instance.AddItem(itemToAdd);
                        Destroy(gameObject);
                    }
                    else
                    {
                        ChatManager.Instance.SystemMessage($"<color=#CC0000>�I�]�w��!</color>\n");
                    }
                }
            }

            //�Y�O���ӫ~�H�~���F�賣���|�[

            if (itemToAdd.GetItemType() != Item.ItemType.Consumable)
            {
                if (InventoryManager.Instance.Items.Count < InventoryManager.Instance.maxCapacity)
                {
                    ChatManager.Instance.SystemMessage($"�A�B���F{itemToAdd.itemName}�C");
                    InventoryManager.Instance.AddItem(itemToAdd);
                    Destroy(gameObject);
                }
                else
                {
                    ChatManager.Instance.SystemMessage($"<color=#CC0000>�I�]�w��!</color>\n");
                }
            }
            InventoryManager.Instance.UpdateList();
        }

    }
}
