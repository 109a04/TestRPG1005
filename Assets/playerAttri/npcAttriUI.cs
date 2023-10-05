using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcAttriUI  : MonoBehaviour
{
    public Text availablePointsText;

    //原始未增減
    public Text HPText;
    public Text MPText;
    public Text attackText;
    public Text speedText;

    //增減後
    public Text cHPText;
    public Text cMPText;
    public Text cattackText;
    public Text cspeedText;


    //增減按鈕
    public Button allocateHPButton;
    public Button allocateMPButton;
    public Button allocateAttackButton;
    public Button allocateSpeedButton;

    public Button minusHPButton;
    public Button minusMPButton;
    public Button minusAttackButton;
    public Button minusSpeedButton;

    //重置
    public Button resetPointsButton;
    public Button checkPointsButton;

    //從npcAttribute物件上取值
    public npcAttribute npcAttr;


    private void Start()
    {
    }

    private void Update()
    {
        // 更新 UI 介面顯示的屬性點數據
        HPText.text = npcAttr.ohp.ToString();
        MPText.text = npcAttr.omp.ToString();
        attackText.text = npcAttr.oattack.ToString();
        speedText.text = npcAttr.ospeed.ToString();

        cHPText.text = npcAttr.chp.ToString();
        cMPText.text = npcAttr.cmp.ToString();
        cattackText.text = npcAttr.cattack.ToString();
        cspeedText.text = npcAttr.cspeed.ToString();

        //可用點
        availablePointsText.text = "可用點 : " + npcAttr.avaPoint.ToString();
    }

    public void allocateHP()
    {
        if (npcAttr.avaPoint > 0)
        {
            npcAttr.hpAdd(1);
        }
    }

    public void allocateMP()
    {
        if (npcAttr.avaPoint > 0)
        {
            npcAttr.mpAdd(1);
        }
    }

    public void allocateAttack()
    {
        if (npcAttr.avaPoint > 0)
        {
            npcAttr.attackAdd(1);
        }
    }

    public void allocateSpeed()
    {
        if (npcAttr.avaPoint > 0)
        {
            npcAttr.speedAdd(1);
        }
    }

    public void minusHP()
    {
        if (npcAttr.chp > 0)
        {
            npcAttr.hpAdd(-1);
        }
    }

    public void minusMP()
    {
        if (npcAttr.cmp > 0)
        {
            npcAttr.mpAdd(-1);
        }
    }

    public void minusAttack()
    {
        if (npcAttr.cattack > 0)
        {
            npcAttr.attackAdd(-1);
        }
    }

    public void minusSpeed()
    {
        if (npcAttr.cspeed > 0)
        {
            npcAttr.speedAdd(-1);
        }
    }
    public void resetAttributePoints()
    {
        npcAttr.resetPoint();
    }

    public void checkAttributePoints()
    {
        npcAttr.checkPoint();
    }
}
