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
    public List<Item> Goods = new List<Item>(); //�s�ӫ~���
    public BaseItemController[] StoreItems;     //����ಾ
    public event Action OpenInventoryEvent; // �I�]UI�}�Ҩƥ�
    public event Action CloseInventoryEvent; //����

    public Transform StoreParent; //�ө���l��������
    public GameObject goodsPrefab; //�ӫ~��prefab

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

    public void ToggleStoreUI(bool active) //UI�}��
    {
        StoreUI.SetActive(active);
        if (active)
        {
            OpenInventoryEvent?.Invoke(); // Ĳ�o�I�]UI�}�Ҩƥ�
        }
        else
        {
            CloseInventoryEvent?.Invoke();
        }
    }

    public void UpdateList()
    {
        //�M���즳����l
        foreach (Transform child in StoreParent)
        {
            Destroy(child.gameObject);
        }

        //��l��StoreItems
        StoreItems = new BaseItemController[Goods.Count];

        //��Ҥưӫ~���ػP��T
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
