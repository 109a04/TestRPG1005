using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Consumable Item")]
[Serializable]

public class ConsumableItem : Item
{
    public Dictionary<string, int> itemCounts = new Dictionary<string, int>(); //儲存消耗品的數量
    public int itemValue; //物品數值，吃下去可以恢復多少生命值之類的
    public ConsumableType consumableType;

    public override ItemType GetItemType()
    {
        return ItemType.Consumable;
    }

    public override bool CanSell()
    {
        AbleToSell = true;
        return AbleToSell;
    }

    public enum ConsumableType 
    {   
        Health,
        Magic,
        Exp,
        //還有一些加成類的還沒加入之後可以隨便增減(?
    }


}
