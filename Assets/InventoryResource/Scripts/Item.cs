using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{ 
    public int id; //物品編號
    public string itemName; //物品名稱
    public Sprite itemIcon; //物品圖示
    public int    itemPrice; //物品價格
    
    [TextArea]
    public string itemDescript; //物品描述
    public abstract ItemType GetItemType(); //回傳道具種類
    

    public enum ItemType
    {
        Consumable, //消耗品
        Weapon,     //武器類
        Quest       //任務道具
    }
}