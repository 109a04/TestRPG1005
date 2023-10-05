using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Item : ScriptableObject //  23/08/21�󴫦���H���O
{
    /// <summary>
    /// �C�Ӫ��~�@�q�����F��
    /// </summary>

    public int id; //���~�s��
    public string itemName; //���~�W��
    public Sprite itemIcon; //���~�ϥ�
    public int itemPrice; //���ө��Ϊ��ӫ~����A����i��|���⤣�n���F���X�h���\��
    public bool AbleToSell; //��_����]�ϥ��u���NPC��������^
    [TextArea]
    public string itemDescript; //���~�y�z
    public abstract ItemType GetItemType();
    public abstract bool CanSell();

    public enum ItemType
    {
        Consumable,
        Weapon,
        Quest
    }



}