using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryUI;
    public static InventoryManager Instance;
    public StoreManager storeManager;
    public List<Item> Items = new List<Item>(); //�s���~���
    public BaseItemController[] InventoryItems; //����ಾ
    public Transform itemsParent; //��l��������
    public GameObject itemPrefab; //��l��prefab
    public int maxCapacity = 20;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InventoryUI.SetActive(false);
        storeManager.OpenInventoryEvent += OpenInventory; // �q�\
    }

    private void OpenInventory()
    {
        InventoryUI.SetActive(true); // �}�Ұө��ɶ��K���}�I�]UI
        storeManager.OpenInventoryEvent -= OpenInventory; // �����q�\�A�קK���ƥ��}
        storeManager.CloseInventoryEvent += CloseInventory; //�q�\�����I�]�ƥ�
    }

    private void CloseInventory() //�P�W�A�u�O����
    {
        InventoryUI.SetActive(false);
        storeManager.CloseInventoryEvent -= CloseInventory;
        storeManager.OpenInventoryEvent += OpenInventory; // �q�\�}�ҭI�]�ƥ�A�o�˥i�H�@���ʦa�}���I�]�Ӥ��QUpdate�j�[(�I�]���������������k�����}�ҫ��䳣�ٯ�ϥ�)

    }

    public void AddItem(Item newItem)
    {
        if (Items.Count < maxCapacity)
        {
            Items.Add(newItem);
        }
        else
        {
            ChatManager.Instance.SystemMessage($"<color=#CC0000>�I�]�w��!</color>\n");
            Debug.Log("�I�]�w��!");
        }
        UpdateList();
    }


    public void RemoveItem(Item itemToRemove)
    {
        if (itemToRemove.GetItemType() == Item.ItemType.Weapon)
        {
            
            Items.Remove(itemToRemove);
        }
        if (itemToRemove.GetItemType() == Item.ItemType.Consumable)
        {
            ConsumableItem consumableItem = (ConsumableItem)itemToRemove; //���F�ݥ����ƶq�ҥH�ഫ...�R�F
            if (consumableItem.itemCounts.ContainsKey(itemToRemove.itemName))
            {
                consumableItem.itemCounts[itemToRemove.itemName]--;

                if (consumableItem.itemCounts[itemToRemove.itemName] <= 0)
                {
                    Items.Remove(itemToRemove);
                }
            }
        }


        UpdateList();
    }


    public void UpdateList()
    {
        // �M���즳����l
        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }

        InventoryItems = new BaseItemController[Items.Count]; // ��l�� InventoryItems

        // �b UI ����Ҥƪ��~���بó]�m��T

        for (int i = 0; i < Items.Count; i++)
        {


            GameObject obj = Instantiate(itemPrefab, itemsParent);
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();


            itemIcon.sprite = Items[i].itemIcon;

            if (Items[i].GetItemType() == Item.ItemType.Consumable)
            {
                ConsumableItem consumable = Items[i] as ConsumableItem;
                var itemCount = obj.transform.Find("ItemIcon/ItemCount").GetComponent<Text>();
                itemCount.text = consumable.itemCounts[Items[i].itemName].ToString();
            }



            // �]�m InventoryItemController �� item
            InventoryItems[i] = obj.GetComponent<BaseItemController>();
            InventoryItems[i].SetItem(Items[i]);

        }
    }

}
