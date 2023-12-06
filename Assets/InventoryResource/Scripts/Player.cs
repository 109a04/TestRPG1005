using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Enemy;

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
    protected int playerElement;

    public Slider HealthSlider;
    public Slider MpSlider;
    public Slider ExpSlider;
    public Text HealthText;
    public Text MpText;
    public Text ExpText;

    //�ݩʤj�v
    public npcAttribute masterNPC;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //��PlayerAttribute�����ƭȤޥιL�ӡA�ê�l�Ʒ�e�ƭ�
        HealthText = HealthSlider.transform.Find("HP").GetComponent<Text>();
        MpText = MpSlider.transform.Find("MP").GetComponent <Text>();
        ExpText = ExpSlider.transform.Find("EXP").GetComponent<Text>();
        SetStatus();
        SetInitStats();
        SetElement((int)playerAttributeManager.Instance.element);

        //���T��l�ƪ��a��ܪ��A

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
    /// 1017��Q�Ǫ������ͩR������Ƴ�W���X�Ӽg�F
    /// </summary>
    

    //�Q�Ǫ����A�̷��ݩʧP�_�u��y�����ˮ`
    public void Beaten(float damage, int element) 
    {
        /// �����۫g��
        /// �� = 1, �� = 2, �� = 3, �g = 4
        ///
        if(element != 0)
        {
            //�Y�Ǫ��ݩʴ�h���a�ݩʭ�n�O-1�A�N�Y�Ǫ��g��a�ݩʡA�����O�[��1.25���]�M�����ơ^
            if(element - playerElement == -1)
            {
                damage *= 1.25f;
            }
            //�Y�Ǫ��ݩʴ�h���a�ݩʭ�n�O1�A�h�Ǫ��Q���a�g��A�����O�ର0.75��
            else if(element - playerElement == 1)
            {
                damage *= 0.75f;
            }

            //�g�����t�~��]1��4�^
            if(element == 1 && playerElement == 4) //�Ǫ������a�g
            {
                damage *= 0.75f;
            }
            if(element == 4 && playerElement == 1) //�Ǫ��g���a��
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
        //�Y��e�ƭȥ[����W�X�̤j�d��A�h�u�^�_��̤j��
        if((currentHealth + value) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if ((currentHealth + value) <= 0)//���a�i��Q�����t�ơA�j�0
        {
            currentHealth = 0;
            
        }
        else //��l���p���`
        {
            currentHealth += value;
        }

        /*�ɬ����աAhp���W���N�O�ͩR�I��*50�A�p�G��F�W���A�Y�ɫ~�èS���[���ĪG
        playerAttributeManager.Instance.hp = Mathf.Min(playerAttributeManager.Instance.hp + value, playerAttributeManager.Instance.origin_hp * 50);
        currentHealth = playerAttributeManager.Instance.hp;//������D�e��������]���쭭��
        */

        SetHealthUI();

        if (value < 0)
        {
            ChatManager.Instance.SystemMessage($"��O���<color=#F5EC3D>{Mathf.Abs(value)}�C</color>\n");
        }
        else
        {
            ChatManager.Instance.SystemMessage($"��O�W�[<color=#F5EC3D>{value}�C</color>\n");
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
            ChatManager.Instance.SystemMessage($"�]�O���<color=#F5EC3D>{Mathf.Abs(value)}�C</color>\n");
        }
        else
        {
            ChatManager.Instance.SystemMessage($"�]�O�W�[<color=#F5EC3D>{value}�C</color>\n");
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
            playerAttributeManager.Instance.exp += value;
            int levelsToUp = playerAttributeManager.Instance.exp / playerAttributeManager.Instance.up_exp;
            playerAttributeManager.Instance.level += levelsToUp;
            playerAttributeManager.Instance.exp = playerAttributeManager.Instance.exp - (levelsToUp * playerAttributeManager.Instance.up_exp);
            playerAttributeManager.Instance.point = playerAttributeManager.Instance.point + (levelsToUp * 5);
            masterNPC.avaPoint = playerAttributeManager.Instance.point;
            currentExp = playerAttributeManager.Instance.exp;
            ChatManager.Instance.SystemMessage($"<color=#F5EC3D>�A�ɦ� {playerAttributeManager.Instance.level} ��!</color>\n");
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
            ChatManager.Instance.SystemMessage($"�g����<color=#F5EC3D>{Mathf.Abs(value)}�C</color>\n");
        }
        else
        {
            ChatManager.Instance.SystemMessage($"�g��W�[<color=#F5EC3D>{value}�C</color>\n");
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SyncCurrentExp() //�P�B��e�g���
    {
        currentExp = playerAttributeManager.Instance.exp;
    }
}
