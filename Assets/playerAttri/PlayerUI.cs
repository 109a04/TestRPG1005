using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    //�i���ݩ��I
    public Text availablePointsText;

    //�m�W/����/�g��/�W��
    public Text nameText;
    public Text levelText;
    public Text expText;
    public Text up_expText;
    
    //�����԰��ݩ�
    public Text HPText;
    public Text MPText;
    public Text attackText;
    public Text speedText;

    //��l�ݩ�
    public Text oHPText;
    public Text oMPText;
    public Text oattackText;
    public Text ospeedText;

    //�Z��/����
    public Text weaponText;
    public Text elementText;

    //�ܩ�
    public Text firemText;
    public Text watermText;
    public Text grassmText;
    public Text groundmText;

    private void Start()
    {
    }

    private void Update()
    {
        // ��s UI ������ܪ��ݩ��I�ƾ�
        nameText.text = "�m�W�G" + playerAttributeManager.Instance.pname.ToString();
        levelText.text = "���šG" + playerAttributeManager.Instance.level.ToString();
        levelText.text = "���šG" + playerAttributeManager.Instance.level.ToString();
        expText.text = "�g��G" + playerAttributeManager.Instance.exp.ToString();
        up_expText.text = "�g��W���G" + playerAttributeManager.Instance.up_exp.ToString();

        HPText.text = "��q�G" + Player.Instance.GetCurrentHealth()+ "/" + playerAttributeManager.Instance.hp.ToString();
        MPText.text = "�]�O�G" + playerAttributeManager.Instance.mp.ToString();
        attackText.text = "�����O�G" + playerAttributeManager.Instance.attack.ToString();
        speedText.text = "�t�סG" + playerAttributeManager.Instance.speed.ToString();

        oHPText.text = "�ͩR�G" + playerAttributeManager.Instance.origin_hp.ToString();
        oMPText.text = "�]�k�G" + playerAttributeManager.Instance.origin_mp.ToString();
        oattackText.text = "�O�q�G" + playerAttributeManager.Instance.origin_attack.ToString();
        ospeedText.text = "�ӱ��G" + playerAttributeManager.Instance.origin_speed.ToString();

        weaponText.text = "�Z���G" + playerAttributeManager.Instance.weapon.ToString();
        elementText.text = "�ݩʡG" + playerAttributeManager.Instance.element.ToString();

        firemText.text = "���ܡG" + playerAttributeManager.Instance.fire_mr.ToString();
        watermText.text = "���ܡG" + playerAttributeManager.Instance.water_mr.ToString();
        grassmText.text = "��ܡG" + playerAttributeManager.Instance.grass_mr.ToString();
        groundmText.text = "�g�ܡG" + playerAttributeManager.Instance.ground_mr.ToString();
        availablePointsText.text = "�i���I�G" + playerAttributeManager.Instance.point.ToString();
    }

}
