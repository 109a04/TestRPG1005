using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Weapon")]
[Serializable]

public class WeaponItem : Item
{
    public WeaponType weaponType;
    public ElementType elementType;
    public GameObject weaponModel;

    public override ItemType GetItemType()
    {
        return ItemType.Weapon;
    }


    public enum WeaponType //四種武器
    {
        None,      //沒拿武器
        Sword,     //劍
        MagicWand, //魔杖
        Heavy,     //重型武器
        Bow        //弓
    }

    public enum ElementType
    {
        None,  //無屬性
        Water, //水
        Fire,  //火
        Grass, //草
        Earth  //土
    }
}
