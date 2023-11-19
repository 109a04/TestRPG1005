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
    public List<Item> Items = new List<Item>(); //存物品資料
    public BaseItemController[] InventoryItems; //資料轉移
    public Transform itemsParent; //格子的父物件
    public GameObject itemPrefab; //格子的prefab
    public int maxCapacity = 20;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InventoryUI.SetActive(false);
        storeManager.OpenInventoryEvent += OpenInventory; // 訂閱
    }

    private void OpenInventory()
    {
        InventoryUI.SetActive(true); // 開啟商店時順便打開背包UI
        storeManager.OpenInventoryEvent -= OpenInventory; // 取消訂閱，避免重複打開
        storeManager.CloseInventoryEvent += CloseInventory; //訂閱關閉背包事件
    }

    private void CloseInventory() //同上，只是關閉
    {
        InventoryUI.SetActive(false);
        storeManager.CloseInventoryEvent -= CloseInventory;
        storeManager.OpenInventoryEvent += OpenInventory; // 訂閱開啟背包事件，這樣可以一次性地開閉背包而不被Update綁架(背包本身的關閉按鍵跟右側的開啟按鍵都還能使用)

    }

    public void AddItem(Item newItem)
    {
        if (Items.Count < maxCapacity)
        {
            Items.Add(newItem);
        }
        else
        {
            ChatManager.Instance.SystemMessage($"<color=#CC0000>背包已滿!</color>\n");
            Debug.Log("背包已滿!");
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
            ConsumableItem consumableItem = (ConsumableItem)itemToRemove; //為了看它的數量所以轉換...吐了
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
        // 清除原有的格子
        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }

        InventoryItems = new BaseItemController[Items.Count]; // 初始化 InventoryItems

        // 在 UI 中實例化物品項目並設置資訊

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



            // 設置 InventoryItemController 的 item
            InventoryItems[i] = obj.GetComponent<BaseItemController>();
            InventoryItems[i].SetItem(Items[i]);

        }
    }

}
