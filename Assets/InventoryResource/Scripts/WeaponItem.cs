using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Weapon")]
[Serializable]

public class WeaponItem : Item
{
    public WeaponType weaponType;
    public ElementType elementType;

    public override ItemType GetItemType()
    {
        return ItemType.Weapon;
    }

    public override bool CanSell()
    {
        AbleToSell = true;
        return AbleToSell;
    }

    public enum WeaponType //�|�تZ��
    {
        Sword,     //�C
        MagicWand, //�]��
        Heavy,     //�����Z��
        Bow        //�}
    }

    public enum ElementType
    {
        Fire,  //��
        Water, //��
        Grass, //��
        Earth  //�g
    }
}
