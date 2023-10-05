using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcAttriUI  : MonoBehaviour
{
    public Text availablePointsText;

    //��l���W��
    public Text HPText;
    public Text MPText;
    public Text attackText;
    public Text speedText;

    //�W���
    public Text cHPText;
    public Text cMPText;
    public Text cattackText;
    public Text cspeedText;


    //�W����s
    public Button allocateHPButton;
    public Button allocateMPButton;
    public Button allocateAttackButton;
    public Button allocateSpeedButton;

    public Button minusHPButton;
    public Button minusMPButton;
    public Button minusAttackButton;
    public Button minusSpeedButton;

    //���m
    public Button resetPointsButton;
    public Button checkPointsButton;

    //�qnpcAttribute����W����
    public npcAttribute npcAttr;


    private void Start()
    {
    }

    private void Update()
    {
        // ��s UI ������ܪ��ݩ��I�ƾ�
        HPText.text = npcAttr.ohp.ToString();
        MPText.text = npcAttr.omp.ToString();
        attackText.text = npcAttr.oattack.ToString();
        speedText.text = npcAttr.ospeed.ToString();

        cHPText.text = npcAttr.chp.ToString();
        cMPText.text = npcAttr.cmp.ToString();
        cattackText.text = npcAttr.cattack.ToString();
        cspeedText.text = npcAttr.cspeed.ToString();

        //�i���I
        availablePointsText.text = "�i���I : " + npcAttr.avaPoint.ToString();
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
