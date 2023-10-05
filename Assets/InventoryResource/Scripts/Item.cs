using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Item : ScriptableObject //  23/08/21更換成抽象類別
{
    /// <summary>
    /// 每個物品共通有的東西
    /// </summary>

    public int id; //物品編號
    public string itemName; //物品名稱
    public Sprite itemIcon; //物品圖示
    public int itemPrice; //給商店用的商品售價，之後可能會有把不要的東西賣出去的功能
    public bool AbleToSell; //能否交易（反正只能跟NPC交易哈哈）
    [TextArea]
    public string itemDescript; //物品描述
    public abstract ItemType GetItemType();
    public abstract bool CanSell();

    public enum ItemType
    {
        Consumable,
        Weapon,
        Quest
    }



}