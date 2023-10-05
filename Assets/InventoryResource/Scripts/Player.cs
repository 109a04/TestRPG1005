using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//����a�ƭȬ���UI
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    //��Slider���ʺA�B�⪺�̤j��
    protected int maxHealth;
    protected int maxMp;
    protected int maxExp;

    //��e���a�ƭ�
    protected int currentHealth;
    protected int currentMp;
    protected int currentExp;

    public Slider HealthSlider;
    public Slider MpSlider;
    public Slider ExpSlider;
    public Text HealthText;
    public Text MpText;
    public Text ExpText;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //��PlayerAttribute�����ƭȤޥιL�ӡA�ê�l�Ʒ�e�ƭ�
        SetStatus();
        currentHealth = maxHealth;
        currentMp = maxMp;
        currentExp = playerAttributeManager.Instance.exp;


        //���T��l�ƪ��a��ܪ��A

        SetUI();
        
    }

    public void SetStatus()
    {
        maxHealth = playerAttributeManager.Instance.hp;
        maxMp = playerAttributeManager.Instance.mp;
        maxExp = playerAttributeManager.Instance.up_exp;
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
        HealthText.text = $"HP: {currentHealth}";
    }

    public void SetMPUI()
    {
        MpSlider.value = currentMp;
        MpText.text = $"MP: {currentMp}";
    }

    public void SetEXPUI()
    {
        ExpSlider.value = currentExp;
        ExpText.text = $"EXP: {currentExp}";
    }

    //�����q���P�䥦�i�ױ��ʺA�����
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
    /// �ϥή��ӫ~��������ơA�H��i��i�H�h�B���L�}���A�ίd�b�o�̫���(����)
    /// </summary>

    public void IncreaseHealth(int value)
    {
        //�Y��e�ƭȥ[����W�X�̤j�d��A�h�u�^�_��̤j��
        if((currentHealth + value) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += value;
        }

        if((currentHealth + value) <= 0) //���a�i��Q�����t�ơA�j�0
        {
            currentHealth = 0;
        }

        /*�ɬ����աAhp���W���N�O�ͩR�I��*50�A�p�G��F�W���A�Y�ɫ~�èS���[���ĪG
        playerAttributeManager.Instance.hp = Mathf.Min(playerAttributeManager.Instance.hp + value, playerAttributeManager.Instance.origin_hp * 50);
        currentHealth = playerAttributeManager.Instance.hp;//������D�e��������]���쭭��
        */

        SetHealthUI();

        if (value < 0)
        {
            ChatManager.Instance.SystemMessage($"<color=#F5EC3D>��O���{Mathf.Abs(value)}�C</color>\n");
        }
        else
        {
            ChatManager.Instance.SystemMessage($"<color=#F5EC3D>��O�W�[{value}�C</color>\n");
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

        /*�ɬ����աAmp�W���O�k�O�I*10
        playerAttributeManager.Instance.mp = Mathf.Min(playerAttributeManager.Instance.mp + value, playerAttributeManager.Instance.origin_mp * 10);
        Mp = playerAttributeManager.Instance.mp;//������D�e�����k�����쭭��
        */

        SetMPUI();

        if (value < 0)
        {
            ChatManager.Instance.SystemMessage($"<color=#F5EC3D>�]�O���{Mathf.Abs(value)}�C</color>\n");
        }
        else
        {
            ChatManager.Instance.SystemMessage($"<color=#F5EC3D>�]�O�W�[{value}�C</color>\n");
        }

    }
    public void IncreaseExp(int value)
    {
        if((currentExp + value) >= maxExp) //�Ӳz�ӻ��n�ɯšA���ثe�٤����D�ɯū�n������
        {
            ///  LevelUp(); ���˳o�O�ɯŨ禡
            ///  �禡�̭��i�঳playerAttributeManager.Instance.level += 1; 
            ///  currentExp = 0;
            ///  �������F��
        }
        else
        {
            playerAttributeManager.Instance.exp += value;
            currentExp += value;
        }


        /*�ɬ����աA�������檺�g����]�@�_����
        playerAttributeManager.Instance.exp = playerAttributeManager.Instance.exp + value;
        */

        SetEXPUI();

        if (value < 0)
        {
            ChatManager.Instance.SystemMessage($"<color=#F5EC3D>�g����{Mathf.Abs(value)}�C</color>\n");
        }
        else
        {
            ChatManager.Instance.SystemMessage($"<color=#F5EC3D>�g��W�[{value}�C</color>\n");
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
