using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerAttributeManager : MonoBehaviour
{
    public static playerAttributeManager Instance { get; private set; }


    //角色基本資料
    public string pname { get; set; }//名字
    public int money { get; set; }//等級
    public int level { get; set; }//等級
    public int exp { get; set; }//經驗
    public int up_exp { get; set; }//每等級的經驗上限
    public int point { get; set; }//可用屬性點

    //原始戰鬥相關屬性
    public int origin_hp { get; set; }//原始血量
    public int origin_mp { get; set; }//原始魔力值
    public int origin_attack { get; set; }//原始攻擊力
    public int origin_speed { get; set; }//原始移動速度

    //經過加成後實際運用在戰鬥上的屬性
    public int hp { get; set; }//血量
    public int mp { get; set; }//魔力值
    public int attack { get; set; }//攻擊力
    public int speed { get; set; }//移動速度
    public int weapon { get; set; }//裝備，0=赤手空拳，1=劍，2=法器，3=重裝，4=弓箭
    public int element { get; set; }//0=無屬性，1=火，2=水，3=草，4=土

    //各元素屬性抗性
    public int fire_mr { get; set; }//火抗
    public int water_mr { get; set; }//水抗
    public int grass_mr { get; set; }//草抗
    public int ground_mr { get; set; }//土抗

    //給playerAttributes類的東東初始值或初始設定
    public playerAttributeManager()
    {
        pname = "test";
        money = 1000;
        level = 1;
        exp = 0;
        up_exp = 0;
        point = 10;

        origin_hp = 1;
        origin_mp = 3;
        origin_attack = 5;
        origin_speed = 9;

        hp = origin_hp * 50;
        mp = origin_mp * 10;
        attack = origin_attack * 2;
        speed = origin_speed * 2;

        weapon = 0;
        element = 0;

        fire_mr = 10;
        water_mr = 10;
        grass_mr = 10;
        ground_mr = 10;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}