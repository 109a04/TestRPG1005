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


    public enum WeaponType //�|�تZ��
    {
        None,      //�S���Z��
        Sword,     //�C
        MagicWand, //�]��
        Heavy,     //�����Z��
        Bow        //�}
    }

    public enum ElementType
    {
        None,  //�L�ݩ�
        Water, //��
        Fire,  //��
        Grass, //��
        Earth  //�g
    }
}
