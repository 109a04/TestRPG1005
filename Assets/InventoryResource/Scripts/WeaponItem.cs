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

    public enum WeaponType //四種武器
    {
        Sword,     //劍
        MagicWand, //魔杖
        Heavy,     //重型武器
        Bow        //弓
    }

    public enum ElementType
    {
        Fire,  //火
        Water, //水
        Grass, //草
        Earth  //土
    }
}
