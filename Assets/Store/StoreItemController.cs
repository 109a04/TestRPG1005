using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemController : BaseItemController
{
    public Text itemPriceText;
    public GameObject QuantityPanel;
    public Text QuantityText;
    public Text ItemNameText;
    public Text CanNotAffordText;
    private bool CanNotAfford = false;
    private static GameObject activeQuantityUI; // 保存當前已經彈出的數量選擇界面
    private int currentQuantity = 1; //初始數量

    protected override void Start()
    {
        base.Start();

        QuantityPanel.SetActive(false);
        CanNotAfford = false;
        itemPriceText = DescriptionPanel.transform.Find("ItemPrice").GetComponent<Text>(); //追加以下文字
        QuantityText = QuantityPanel.transform.Find("QuantityText").GetComponent<Text>();
        ItemNameText = QuantityPanel.transform.Find("ItemName").GetComponent<Text>();
        CanNotAffordText = QuantityPanel.transform.Find("CanNotAfford").GetComponent<Text>();
    }

    protected override void Update()
    {
        base.Update(); //前面用BaseItemController的東西
        if (isMouseOverItem)
        {
            DescriptionPanel.transform.position = new Vector3(500, 280, 0);
            itemPriceText.text = $"價格: {thisItem.itemPrice}";
        }
        ToggleHint();
    }

    public void SelectItem()
    {
        currentQuantity = 1;
        QuantityText.text = $"{currentQuantity}";
        ItemNameText.text = $"{thisItem.itemName}: ";

        // 如果當前有已經彈出的數量選擇界面，先隱藏它
        if (activeQuantityUI != null && activeQuantityUI.activeSelf)
        {
            activeQuantityUI.SetActive(false);
        }

        // 將當前的數量選擇界面設為已彈出
        activeQuantityUI = QuantityPanel;

        QuantityPanel.transform.position = new Vector3(1000, 525, 0);
        QuantityPanel.SetActive(true);
    }

    public void IncrementQuantity()
    {
        currentQuantity++; // 增加數量
        UpdateQuantityUI(); // 更新數量顯示
    }

    public void DecrementQuantity()
    {
        if (currentQuantity > 1) // 確保數量不小於 1
        {
            currentQuantity--; // 減少數量
            UpdateQuantityUI(); // 更新數量顯示
        }
    }

    private void UpdateQuantityUI()
    {
        // 更新數量的 UI 顯示
        QuantityText.text = QuantityText.text = $"{currentQuantity}";
    }

    public void BuyItem() //按下確認鍵購買
    {
        if (InventoryManager.Instance.Items.Count < InventoryManager.Instance.maxCapacity) //背包滿了不行買
        {
            int totalPrice = thisItem.itemPrice * currentQuantity; // 計算總價格
            if (playerAttributeManager.Instance.money >= totalPrice) //有錢的情況
            {
                CanNotAfford = false;
                playerAttributeManager.Instance.money -= totalPrice; //付錢


                //若為消耗品類，檢查背包中是否已存在相同物品
                if (thisItem.GetItemType() == Item.ItemType.Consumable)
                {
                    ConsumableItem itemToBuy = thisItem as ConsumableItem;
                    ConsumableItem existingItem = InventoryManager.Instance.Items.Find(item => item.id == thisItem.id) as ConsumableItem;

                    if (existingItem != null)
                    {
                        // 如果存在，僅增加物品數量
                        existingItem.itemCounts[thisItem.itemName] += currentQuantity;
                    }
                    else
                    {
                        // 如果不存在，創建新的 Item 

                        ConsumableItem purchasedItem = new ConsumableItem //然後通通複製過去
                        {
                            id = itemToBuy.id,
                            itemName = itemToBuy.itemName,
                            itemIcon = itemToBuy.itemIcon,
                            itemPrice = thisItem.itemPrice,
                            itemValue = itemToBuy.itemValue,
                            itemDescript = itemToBuy.itemDescript,
                            consumableType = itemToBuy.consumableType,
                            itemCounts = new Dictionary<string, int> { { itemToBuy.itemName, currentQuantity } }
                        };

                        // 添加到玩家的背包
                        InventoryManager.Instance.AddItem(purchasedItem);

                    }
                }
                //若為武器類道具
                else if (thisItem.GetItemType() == Item.ItemType.Weapon) //武器
                {
                    WeaponItem itemToBuy = (WeaponItem)thisItem;
                    for (int i = 0; i < currentQuantity; i++)
                    {
                        WeaponItem purchasedItem = ScriptableObject.CreateInstance<WeaponItem>(); // 使用ScriptableObject.CreateInstance來實例化
                        purchasedItem.id = itemToBuy.id;
                        purchasedItem.itemName = itemToBuy.itemName;
                        purchasedItem.itemIcon = itemToBuy.itemIcon;
                        purchasedItem.itemPrice = itemToBuy.itemPrice;
                        purchasedItem.itemDescript = itemToBuy.itemDescript;
                        purchasedItem.weaponType = itemToBuy.weaponType;
                        purchasedItem.elementType = itemToBuy.elementType;
                        InventoryManager.Instance.AddItem(purchasedItem); //僅增加，不疊加數量
                    }

                }

                InventoryManager.Instance.UpdateList();//更新背包
                currentQuantity = 1;
                UpdateQuantityUI();
            }

            else
            {  
                CanNotAfford = true;
            }
        }
        else
        {
            ChatManager.Instance.SystemMessage($"<color=#CC0000>背包已滿!</color>\n");
            Debug.Log("背包已滿!");
        }
    }


    public void ToggleHint()
    {
        CanNotAffordText.gameObject.SetActive(CanNotAfford);
    }
}
