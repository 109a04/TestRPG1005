using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �Ψӳ]�p���P�Ǫ��ƭȪ���
/// �Ϊk�G�b�U��귽�Ϫ��ťճB�k�� > Create > Enemy > New Enemy Data
/// </summary>

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/New Enemy Data")]

public class Enemy : ScriptableObject
{
    public int enemyID;      //�Ǫ��s��
    public string enemyName; //�ĤH�W��
    public EnemyElement enemyElement; //�ĤH�ݩ�

    public int level; //����
    public int maxHealth; //��q
    public int attack; //�����O
    public int rewardExp;

    public enum EnemyElement
    {
        None,   //�L
        Water,  //��
        Fire,   //��
        Grass,  //��
        Earth   //�g
    }
}
