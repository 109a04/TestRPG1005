using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Enemy;

//控制玩家數值相關UI
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    //給Slider做動態運算的最大值
    protected int maxHealth;
    protected int maxMp;
    protected int maxExp;

    //當前玩家數值
    protected int currentHealth;
    protected int currentMp;
    protected int currentExp;
    protected int playerElement;

    public Slider HealthSlider;
    public Slider MpSlider;
    public Slider ExpSlider;
    public Text HealthText;
    public Text MpText;
    public Text ExpText;

    //屬性大師
    public npcAttribute masterNPC;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //把PlayerAttribute中的數值引用過來，並初始化當前數值
        HealthText = HealthSlider.transform.Find("HP").GetComponent<Text>();
        MpText = MpSlider.transform.Find("MP").GetComponent <Text>();
        ExpText = ExpSlider.transform.Find("EXP").GetComponent<Text>();
        SetStatus();
        SetInitStats();
        SetElement((int)playerAttributeManager.Instance.element);

        //正確初始化玩家顯示狀態

        SetUI();
        
    }

    public void SetStatus()
    {
        maxHealth = playerAttributeManager.Instance.hp;
        maxMp = playerAttributeManager.Instance.mp;
        maxExp = playerAttributeManager.Instance.up_exp;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentMp > maxMp)
        {
            currentMp = maxMp;
        }
    }

    public void SetElement(int element)
    {
        playerElement = element;
    }

    public void SetInitStats()
    {
        currentHealth = maxHealth;
        currentMp = maxMp;
        currentExp = playerAttributeManager.Instance.exp;
    }

    public void SetUI()
    {
        UpdateSliderRange();
        SetHealthUI();
        SetMPUI();
        SetEXPUI();
    }


    public void SetHealthUI()
    {
        HealthSlider.value = currentHealth;
        HealthText.text = $"HP: {currentHealth}/{maxHealth}";
    }

    public void SetMPUI()
    {
        MpSlider.value = currentMp;
        MpText.text = $"MP: {currentMp}/{maxMp}";
    }

    public void SetEXPUI()
    {
        ExpSlider.value = currentExp;
        ExpText.text = $"EXP: {currentExp}/{maxExp}";
    }

    //控制血量條與其它進度條動態的函數
    public void UpdateSliderRange()
    {
        HealthSlider.minValue = 0;
        MpSlider.minValue = 0;
        ExpSlider.minValue = 0;
        HealthSlider.maxValue = maxHealth;
        MpSlider.maxValue = maxMp;
        ExpSlider.maxValue = maxExp;
    }


    /// <summary>
    /// 使用消耗品相關的函數，以後可能可以搬運到其他腳本，或留在這裡哈哈(哈哈)
    /// 1017把被怪物打的生命相關函數單獨拿出來寫了
    /// </summary>
    

    //被怪物打，依照屬性判斷真實造成的傷害
    public void Beaten(float damage, int element) 
    {
        /// 元素相剋表
        /// 水 = 1, 火 = 2, 草 = 3, 土 = 4
        ///
        if(element != 0)
        {
            //若怪物屬性減去玩家屬性剛好是-1，意即怪物剋制玩家屬性，攻擊力加成1.25倍（然後取整數）
            if(element - playerElement == -1)
            {
                damage *= 1.25f;
            }
            //若怪物屬性減去玩家屬性剛好是1，則怪物被玩家剋制，攻擊力轉為0.75倍
            else if(element - playerElement == 1)
            {
                damage *= 0.75f;
            }

            //土水的另外算（1跟4）
            if(element == 1 && playerElement == 4) //怪物水玩家土
            {
                damage *= 0.75f;
            }
            if(element == 4 && playerElement == 1) //怪物土玩家水
            {
                damage *= 1.25f;
            }
        }
        if(currentHealth - damage > 0)
        {
            currentHealth -= (int) damage;
        }
        else if(currentHealth - damage <= 0)
        {
            currentHealth = 0;
        }
        SetHealthUI();
    }

    public void IncreaseHealth(int value)
    {
        //若當前數值加成後超出最大範圍，則只回復到最大值
        if((currentHealth + value) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if ((currentHealth + value) <= 0)//當玩家可能被扣血到負數，強制為0
        {
            currentHealth = 0;
            
        }
        else //其餘情況正常
        {
            currentHealth += value;
        }

        /*玉珊測試，hp的上限就是生命點數*50，如果到了上限再吃補品並沒有加成效果
        playerAttributeManager.Instance.hp = Mathf.Min(playerAttributeManager.Instance.hp + value, playerAttributeManager.Instance.origin_hp * 50);
        currentHealth = playerAttributeManager.Instance.hp;//讓角色主畫面的血條也受到限制
        */

        SetHealthUI();

        if (value < 0)
        {
            ChatManager.Instance.SystemMessage($"體力減少<color=#F5EC3D>{Mathf.Abs(value)}。</color>\n");
        }
        else
        {
            ChatManager.Instance.SystemMessage($"體力增加<color=#F5EC3D>{value}。</color>\n");
        }
    }

    public void IncreaseMp(int value)
    {
        if ((currentMp + value) > maxMp)
        {
            currentMp = maxMp;
        }
        else
        {
            currentMp += value;
        }

        /*玉珊測試，mp上限是法力點*10
        playerAttributeManager.Instance.mp = Mathf.Min(playerAttributeManager.Instance.mp + value, playerAttributeManager.Instance.origin_mp * 10);
        Mp = playerAttributeManager.Instance.mp;//讓角色主畫面的法條受到限制
        */

        SetMPUI();

        if (value < 0)
        {
            ChatManager.Instance.SystemMessage($"魔力減少<color=#F5EC3D>{Mathf.Abs(value)}。</color>\n");
        }
        else
        {
            ChatManager.Instance.SystemMessage($"魔力增加<color=#F5EC3D>{value}。</color>\n");
        }

    }
    public void IncreaseExp(int value)
    {
        if((currentExp + value) >= maxExp) //照理來說要升級，但目前還不知道升級後要做什麼
        {
            ///  LevelUp(); 假裝這是升級函式
            ///  函式裡面可能有playerAttributeManager.Instance.level += 1; 
            ///  currentExp = 0;
            ///  之類的東西
            playerAttributeManager.Instance.exp += value;
            int levelsToUp = playerAttributeManager.Instance.exp / playerAttributeManager.Instance.up_exp;
            playerAttributeManager.Instance.level += levelsToUp;
            playerAttributeManager.Instance.exp = playerAttributeManager.Instance.exp - (levelsToUp * playerAttributeManager.Instance.up_exp);
            playerAttributeManager.Instance.point = playerAttributeManager.Instance.point + (levelsToUp * 5);
            masterNPC.avaPoint = playerAttributeManager.Instance.point;
            currentExp = playerAttributeManager.Instance.exp;
            ChatManager.Instance.SystemMessage($"<color=#F5EC3D>你升至 {playerAttributeManager.Instance.level} 級!</color>\n");
        }
        else
        {
            playerAttributeManager.Instance.exp += value;
            currentExp += value;
        }


        /*玉珊測試，讓角色欄的經驗欄也一起改變
        playerAttributeManager.Instance.exp = playerAttributeManager.Instance.exp + value;
        */

        SetEXPUI();

        if (value < 0)
        {
            ChatManager.Instance.SystemMessage($"經驗減少<color=#F5EC3D>{Mathf.Abs(value)}。</color>\n");
        }
        else
        {
            ChatManager.Instance.SystemMessage($"經驗增加<color=#F5EC3D>{value}。</color>\n");
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SyncCurrentExp() //同步當前經驗值
    {
        currentExp = playerAttributeManager.Instance.exp;
    }
}
