using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public string playerName;

    public int Health = 100;//��Mp�MHealteh�MExp���]��public
    public int Mp = 50;
    public int Exp = 0;
    public int money = 1200; //���a�]�O

    public Text HealthText;
    public Text MpText;
    public Text ExpText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //�ɬ����աA���D�e���������=��q
        Health = playerAttributeManager.Instance.hp;
        Mp = playerAttributeManager.Instance.mp;
        Exp = playerAttributeManager.Instance.exp;
        //

        //���T��ܪ��a���A
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
    /// �ϥή��ӫ~��������ơA�H��i��i�H�h�B���L�}���A�ίd�b�o�̫���(����)
    /// </summary>

    public void IncreaseHealth(int value)
    {
        Health += value;

        //�ɬ����աAhp���W���N�O�ͩR�I��*50�A�p�G��F�W���A�Y�ɫ~�èS���[���ĪG
        playerAttributeManager.Instance.hp = Mathf.Min(playerAttributeManager.Instance.hp + value, playerAttributeManager.Instance.origin_hp * 50);
        Health = playerAttributeManager.Instance.hp;//������D�e��������]���쭭��
        //

        HealthText.text = $"HP: {Health}";
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
        Mp += value;

        //�ɬ����աAmp�W���O�k�O�I*10
        playerAttributeManager.Instance.mp = Mathf.Min(playerAttributeManager.Instance.mp + value, playerAttributeManager.Instance.origin_mp * 10);
        Mp = playerAttributeManager.Instance.mp;//������D�e�����k�����쭭��
        //

        MpText.text = $"MP: {Mp}";
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
        Exp += value;

        //�ɬ����աA�������檺�g����]�@�_����
        playerAttributeManager.Instance.exp = playerAttributeManager.Instance.exp + value;
        //

        ExpText.text = $"EXP: {Exp}";
        if (value < 0)
        {
            ChatManager.Instance.SystemMessage($"<color=#F5EC3D>�g����{Mathf.Abs(value)}�C</color>\n");
        }
        else
        {
            ChatManager.Instance.SystemMessage($"<color=#F5EC3D>�g��W�[{value}�C</color>\n");
        }
    }
}
