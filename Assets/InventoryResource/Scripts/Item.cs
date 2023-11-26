using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{ 
    public int id; //���~�s��
    public string itemName; //���~�W��
    public Sprite itemIcon; //���~�ϥ�
    public int    itemPrice; //���~����
    
    [TextArea]
    public string itemDescript; //���~�y�z
    public abstract ItemType GetItemType(); //�^�ǹD�����
    

    public enum ItemType
    {
        Consumable, //���ӫ~
        Weapon,     //�Z����
        Quest       //���ȹD��
    }
}