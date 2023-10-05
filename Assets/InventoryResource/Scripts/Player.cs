using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public string playerName;

    public int Health = 100;//把Mp和Healteh和Exp都設成public
    public int Mp = 50;
    public int Exp = 0;
    public int money = 1200; //玩家財力

    public Text HealthText;
    public Text MpText;
    public Text ExpText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //玉珊測試，讓主畫面的血條值=血量
        Health = playerAttributeManager.Instance.hp;
        Mp = playerAttributeManager.Instance.mp;
        Exp = playerAttributeManager.Instance.exp;
        //

        //正確顯示玩家狀態
        GetHealth();
        GetMP();
        GetEXP();
    }


    public void GetHealth()
    {
        HealthText.text = $"HP: {Health}";
    }

    public void GetMP()
    {
        MpText.text = $"MP: {Mp}";
    }

    public void GetEXP()
    {
        ExpText.text = $"EXP: {Exp}";
    }

    /// <summary>
    /// 使用消耗品相關的函數，以後可能可以搬運到其他腳本，或留在這裡哈哈(哈哈)
    /// </summary>

    public void IncreaseHealth(int value)
    {
        Health += value;

        //玉珊測試，hp的上限就是生命點數*50，如果到了上限再吃補品並沒有加成效果
        playerAttributeManager.Instance.hp = Mathf.Min(playerAttributeManager.Instance.hp + value, playerAttributeManager.Instance.origin_hp * 50);
        Health = playerAttributeManager.Instance.hp;//讓角色主畫面的血條也受到限制
        //

        HealthText.text = $"HP: {Health}";
        if (value < 0)
        {
            ChatManager.Instance.SystemMessage($"<color=#F5EC3D>體力減少{Mathf.Abs(value)}。</color>\n");
        }
        else
        {
            ChatManager.Instance.SystemMessage($"<color=#F5EC3D>體力增加{value}。</color>\n");
        }

    }

    public void IncreaseMp(int value)
    {
        Mp += value;

        //玉珊測試，mp上限是法力點*10
        playerAttributeManager.Instance.mp = Mathf.Min(playerAttributeManager.Instance.mp + value, playerAttributeManager.Instance.origin_mp * 10);
        Mp = playerAttributeManager.Instance.mp;//讓角色主畫面的法條受到限制
        //

        MpText.text = $"MP: {Mp}";
        if (value < 0)
        {
            ChatManager.Instance.SystemMessage($"<color=#F5EC3D>魔力減少{Mathf.Abs(value)}。</color>\n");
        }
        else
        {
            ChatManager.Instance.SystemMessage($"<color=#F5EC3D>魔力增加{value}。</color>\n");
        }
    }
    public void IncreaseExp(int value)
    {
        Exp += value;

        //玉珊測試，讓角色欄的經驗欄也一起改變
        playerAttributeManager.Instance.exp = playerAttributeManager.Instance.exp + value;
        //

        ExpText.text = $"EXP: {Exp}";
        if (value < 0)
        {
            ChatManager.Instance.SystemMessage($"<color=#F5EC3D>經驗減少{Mathf.Abs(value)}。</color>\n");
        }
        else
        {
            ChatManager.Instance.SystemMessage($"<color=#F5EC3D>經驗增加{value}。</color>\n");
        }
    }
}
