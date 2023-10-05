using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{

    public GameObject StoreUI;
    public GameObject InventoryUI;
    public Text moneyText;
    public List<Item> Goods = new List<Item>(); //存商品資料
    public BaseItemController[] StoreItems;     //資料轉移
    public event Action OpenInventoryEvent; // 背包UI開啟事件
    public event Action CloseInventoryEvent; //關閉

    public Transform StoreParent; //商店格子的父物件
    public GameObject goodsPrefab; //商品的prefab

    private void Start()
    {
        StoreUI.SetActive(false);
        UpdateList();
    }

    public void Update()
    {
        GetMoney();
    }
    public void GetMoney()
    {
        moneyText.text = $"{Player.Instance.money}";
    }

    public void ToggleStoreUI(bool active) //UI開關
    {
        StoreUI.SetActive(active);
        if (active)
        {
            OpenInventoryEvent?.Invoke(); // 觸發背包UI開啟事件
        }
        else
        {
            CloseInventoryEvent?.Invoke();
        }
    }

    public void UpdateList()
    {
        //清除原有的格子
        foreach (Transform child in StoreParent)
        {
            Destroy(child.gameObject);
        }

        //初始化StoreItems
        StoreItems = new BaseItemController[Goods.Count];

        //實例化商品項目與資訊
        for (int i = 0; i < Goods.Count; i++)
        {
            GameObject obj = Instantiate(goodsPrefab, StoreParent);
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemIcon.sprite = Goods[i].itemIcon;
            StoreItems[i] = obj.GetComponent<BaseItemController>();
            StoreItems[i].SetItem(Goods[i]);
        }
    }
}
