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
    private static GameObject activeQuantityUI; // �O�s��e�w�g�u�X���ƶq��ܬɭ�
    private int currentQuantity = 1; //��l�ƶq

    protected override void Start()
    {
        base.Start();

        QuantityPanel.SetActive(false);
        CanNotAfford = false;
        itemPriceText = DescriptionPanel.transform.Find("ItemPrice").GetComponent<Text>(); //�l�[�H�U��r
        QuantityText = QuantityPanel.transform.Find("QuantityText").GetComponent<Text>();
        ItemNameText = QuantityPanel.transform.Find("ItemName").GetComponent<Text>();
        CanNotAffordText = QuantityPanel.transform.Find("CanNotAfford").GetComponent<Text>();
    }

    protected override void Update()
    {
        base.Update(); //�e����BaseItemController���F��
        if (isMouseOverItem)
        {
            DescriptionPanel.transform.position = new Vector3(500, 280, 0);
            itemPriceText.text = $"����: {thisItem.itemPrice}";
        }
        ToggleHint();
    }

    public void SelectItem()
    {
        currentQuantity = 1;
        QuantityText.text = $"{currentQuantity}";
        ItemNameText.text = $"{thisItem.itemName}: ";

        // �p�G��e���w�g�u�X���ƶq��ܬɭ��A�����å�
        if (activeQuantityUI != null && activeQuantityUI.activeSelf)
        {
            activeQuantityUI.SetActive(false);
        }

        // �N��e���ƶq��ܬɭ��]���w�u�X
        activeQuantityUI = QuantityPanel;

        QuantityPanel.transform.position = new Vector3(1000, 525, 0);
        QuantityPanel.SetActive(true);
    }

    public void IncrementQuantity()
    {
        currentQuantity++; // �W�[�ƶq
        UpdateQuantityUI(); // ��s�ƶq���
    }

    public void DecrementQuantity()
    {
        if (currentQuantity > 1) // �T�O�ƶq���p�� 1
        {
            currentQuantity--; // ��ּƶq
            UpdateQuantityUI(); // ��s�ƶq���
        }
    }

    private void UpdateQuantityUI()
    {
        // ��s�ƶq�� UI ���
        QuantityText.text = QuantityText.text = $"{currentQuantity}";
    }

    public void BuyItem() //���U�T�{���ʶR
    {
        if (InventoryManager.Instance.Items.Count < InventoryManager.Instance.maxCapacity) //�I�]���F����R
        {
            int totalPrice = thisItem.itemPrice * currentQuantity; // �p���`����
            if (playerAttributeManager.Instance.money >= totalPrice) //���������p
            {
                CanNotAfford = false;
                playerAttributeManager.Instance.money -= totalPrice; //�I��


                //�Y�����ӫ~���A�ˬd�I�]���O�_�w�s�b�ۦP���~
                if (thisItem.GetItemType() == Item.ItemType.Consumable)
                {
                    ConsumableItem itemToBuy = thisItem as ConsumableItem;
                    ConsumableItem existingItem = InventoryManager.Instance.Items.Find(item => item.id == thisItem.id) as ConsumableItem;

                    if (existingItem != null)
                    {
                        // �p�G�s�b�A�ȼW�[���~�ƶq
                        existingItem.itemCounts[thisItem.itemName] += currentQuantity;
                    }
                    else
                    {
                        // �p�G���s�b�A�Ыطs�� Item 

                        ConsumableItem purchasedItem = new ConsumableItem //�M��q�q�ƻs�L�h
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

                        // �K�[�쪱�a���I�]
                        InventoryManager.Instance.AddItem(purchasedItem);

                    }
                }
                //�Y���Z�����D��
                else if (thisItem.GetItemType() == Item.ItemType.Weapon) //�Z��
                {
                    WeaponItem itemToBuy = (WeaponItem)thisItem;
                    for (int i = 0; i < currentQuantity; i++)
                    {
                        WeaponItem purchasedItem = ScriptableObject.CreateInstance<WeaponItem>(); // �ϥ�ScriptableObject.CreateInstance�ӹ�Ҥ�
                        purchasedItem.id = itemToBuy.id;
                        purchasedItem.itemName = itemToBuy.itemName;
                        purchasedItem.itemIcon = itemToBuy.itemIcon;
                        purchasedItem.itemPrice = itemToBuy.itemPrice;
                        purchasedItem.itemDescript = itemToBuy.itemDescript;
                        purchasedItem.weaponType = itemToBuy.weaponType;
                        purchasedItem.elementType = itemToBuy.elementType;
                        InventoryManager.Instance.AddItem(purchasedItem); //�ȼW�[�A���|�[�ƶq
                    }

                }

                InventoryManager.Instance.UpdateList();//��s�I�]
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
            ChatManager.Instance.SystemMessage($"<color=#CC0000>�I�]�w��!</color>\n");
            Debug.Log("�I�]�w��!");
        }
    }


    public void ToggleHint()
    {
        CanNotAffordText.gameObject.SetActive(CanNotAfford);
    }
}
