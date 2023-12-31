using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    //可用屬性點
    public Text availablePointsText;

    //姓名/等級/經驗/上限
    public Text nameText;
    public Text playerID;
    public Text levelText;
    public Text expText;
    //public Text up_expText;
    
    //換算後戰鬥屬性
    public Text HPText;
    public Text MPText;
    public Text attackText;
    public Text speedText;

    //原始屬性
    public Text oHPText;
    public Text oMPText;
    public Text oattackText;
    public Text ospeedText;

    //武器/元素
    public Text weaponText;
    public Text elementText;

    //抗性
    public Text firemText;
    public Text watermText;
    public Text grassmText;
    public Text groundmText;

    private void Start()
    {
    }

    private void Update()
    {
        // 更新 UI 介面顯示的屬性點數據
        nameText.text = "名稱：" + playerAttributeManager.Instance.pname.ToString();
        levelText.text = "等級：" + playerAttributeManager.Instance.level.ToString();
        playerID.text = "ID：" + playerAttributeManager.Instance.playerID;
        //levelText.text = "等級：" + playerAttributeManager.Instance.level.ToString();
        expText.text = "經驗：" + playerAttributeManager.Instance.exp.ToString() + "/" + playerAttributeManager.Instance.up_exp.ToString();
        //up_expText.text = "經驗上限：" + playerAttributeManager.Instance.up_exp.ToString();

        HPText.text = "血量：" + Player.Instance.GetCurrentHealth()+ "/" + playerAttributeManager.Instance.hp.ToString();
        MPText.text = "魔力：" + playerAttributeManager.Instance.mp.ToString();
        attackText.text = "攻擊力：" + playerAttributeManager.Instance.attack.ToString();
        speedText.text = "速度：" + playerAttributeManager.Instance.speed.ToString();

        oHPText.text = "生命：" + playerAttributeManager.Instance.origin_hp.ToString();
        oMPText.text = "魔法：" + playerAttributeManager.Instance.origin_mp.ToString();
        oattackText.text = "力量：" + playerAttributeManager.Instance.origin_attack.ToString();
        ospeedText.text = "敏捷：" + playerAttributeManager.Instance.origin_speed.ToString();

        switch (playerAttributeManager.Instance.weapon)
        {
            case 0:
                weaponText.text = "武器： 無";
                break;
            case 1:
                weaponText.text = "武器： 劍";
                break;
            case 2:
                weaponText.text = "武器： 魔杖";
                break; 
            case 3:
                weaponText.text = "武器： 重裝";
                break;
            case 4:
                weaponText.text = "武器： 弓箭";
                break;
        }

        switch (playerAttributeManager.Instance.element)
        {
            case 0:
                elementText.text = "屬性： 無";
                break;
            case 1:
                elementText.text = "屬性： 水";
                break;
            case 2:
                elementText.text = "屬性： 火";
                break;
            case 3:
                elementText.text = "屬性： 草";
                break;
            case 4:
                elementText.text = "屬性： 土";
                break;
        }


        firemText.text = "火抗：" + playerAttributeManager.Instance.fire_mr.ToString();
        watermText.text = "水抗：" + playerAttributeManager.Instance.water_mr.ToString();
        grassmText.text = "草抗：" + playerAttributeManager.Instance.grass_mr.ToString();
        groundmText.text = "土抗：" + playerAttributeManager.Instance.ground_mr.ToString();
        availablePointsText.text = "可用點：" + playerAttributeManager.Instance.point.ToString();
    }

}
