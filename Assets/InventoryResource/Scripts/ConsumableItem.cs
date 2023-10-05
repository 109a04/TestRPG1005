using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Consumable Item")]
[Serializable]

public class ConsumableItem : Item
{
    public Dictionary<string, int> itemCounts = new Dictionary<string, int>(); //�x�s���ӫ~���ƶq
    public int itemValue; //���~�ƭȡA�Y�U�h�i�H��_�h�֥ͩR�Ȥ�����
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
        //�٦��@�ǥ[�������٨S�[�J����i�H�H�K�W��(?
    }


}
