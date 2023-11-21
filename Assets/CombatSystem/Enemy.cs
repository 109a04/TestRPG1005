using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 用來設計不同怪物數值的類
/// 用法：在下方資源區的空白處右鍵 > Create > Enemy > New Enemy Data
/// </summary>

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/New Enemy Data")]

public class Enemy : ScriptableObject
{
    public int enemyID;      //怪物編號
    public string enemyName; //敵人名稱
    public EnemyElement enemyElement; //敵人屬性

    public int level; //等級
    public int maxHealth; //血量
    public int attack; //攻擊力
    public int rewardExp;

    public enum EnemyElement
    {
        None,   //無
        Water,  //水
        Fire,   //火
        Grass,  //草
        Earth   //土
    }
}
