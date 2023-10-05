using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Quest Item")]
[Serializable]

public class QuestItem : Item
{
    public override bool CanSell()
    {
        AbleToSell = false;
        return AbleToSell;
    }

    public override ItemType GetItemType()
    {
        return ItemType.Quest;
    }
}
