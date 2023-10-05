using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcAttribute : MonoBehaviour
{
    
    public int ohp { get; set; }//原始血量
    public int omp { get; set; }//原始魔力值
    public int oattack { get; set; }//原始攻擊力
    public int ospeed { get; set; }//原始移動速度
    
    public int chp { get; set; }//改後血量
    public int cmp { get; set; }//改後魔力值
    public int cattack { get; set; }//改後攻擊力
    public int cspeed { get; set; }//改後移動速度

    public int avaPoint { get; set; }//可用點，可能會重置


    private void Start()
    {
        //npc介面未增減點前會用到
        ohp = playerAttributeManager.Instance.origin_hp;
        omp = playerAttributeManager.Instance.origin_mp;
        oattack = playerAttributeManager.Instance.origin_attack;
        ospeed = playerAttributeManager.Instance.origin_speed;

        chp = playerAttributeManager.Instance.origin_hp;
        cmp = playerAttributeManager.Instance.origin_mp;
        cattack = playerAttributeManager.Instance.origin_attack;
        cspeed = playerAttributeManager.Instance.origin_speed;
        avaPoint = playerAttributeManager.Instance.point;
    }
    


    //用來增加血量的屬性點
    public void hpAdd(int points)
    {
        chp += points;
        avaPoint -= points;
    }

    //用來增加魔力的屬性點
    public void mpAdd(int points)
    {
        cmp += points;
        avaPoint -= points;
    }

    //用來增加攻擊力的屬性點
    public void attackAdd(int points)
    {
        cattack += points;
        avaPoint -= points;
    }

    //用來增加速度的屬性點
    public void speedAdd(int points)
    {
        cspeed += points;
        avaPoint -= points;
    }

    //用來重置屬性點
    public void resetPoint()
    {
        chp = playerAttributeManager.Instance.origin_hp;
        cmp = playerAttributeManager.Instance.origin_mp;
        cattack = playerAttributeManager.Instance.origin_attack;
        cspeed = playerAttributeManager.Instance.origin_speed;
        avaPoint = playerAttributeManager.Instance.point;
    }

    public void checkPoint()
    {
        //讓點數完全確定
        ohp = chp;
        omp = cmp;
        oattack = cattack;
        ospeed = cspeed;

        //點數增減後結果
        playerAttributeManager.Instance.origin_hp = chp;
        playerAttributeManager.Instance.origin_mp = cmp;
        playerAttributeManager.Instance.origin_attack = cattack;
        playerAttributeManager.Instance.origin_speed = cspeed;
        playerAttributeManager.Instance.point = avaPoint;

        //換算後數值影響
        playerAttributeManager.Instance.hp = playerAttributeManager.Instance.origin_hp * 50;
        playerAttributeManager.Instance.mp = playerAttributeManager.Instance.origin_mp * 10;
        playerAttributeManager.Instance.attack = playerAttributeManager.Instance.origin_attack * 2;
        playerAttributeManager.Instance.speed = playerAttributeManager.Instance.origin_speed * 2;

        //顯示到遊戲主畫面的血條/法條/經驗條
        Player.Instance.Health = playerAttributeManager.Instance.hp;
        Player.Instance.Mp = playerAttributeManager.Instance.mp;
        Player.Instance.GetHealth();
        Player.Instance.GetMP();
        
    }
}