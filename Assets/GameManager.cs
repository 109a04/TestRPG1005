using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject EscUI;    //�����C��������
    public GameObject DeathUI;  //���a���`������
    private bool isDead;        //���`�P�_
    public Transform respawnPoint; //�����I
    public GameObject playerModel; //���a�ҫ�

    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EscUI.SetActive(false);
        DeathUI.SetActive(false);
        isDead = false;


    }

    // Update is called once per frame
    void Update()
    {
        //���a���UEsc�A���X�Ȱ��C��������
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscUI.SetActive(true);
        }

        if(Player.Instance.GetCurrentHealth() <= 0)
        {
            SetIsDead();
        }

        DeathUI.SetActive(isDead);
        if (isDead)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    //�T�{���}
    public void PressEscYes()
    {
        Application.Quit();
    }

    //����
    public void PressEscNo()
    {
        EscUI.SetActive(false);
    }

    //�s�ө���
    public void DisplayDeathUI()
    {
        DeathUI.SetActive(isDead);
    }

    //�C�����}
    public void RestartGame()
    {
        ResetGameState();
    }
    

    //���m���a���A
    private void ResetGameState()
    {
        Debug.Log("���s�}�l");

        isDead = false;
        playerAttributeManager.Instance.hp = 50 + (int)(playerAttributeManager.Instance.origin_hp * 2.5);
        Player.Instance.SetStatus();
        Player.Instance.SetInitStats();
        Player.Instance.SetUI();
        Time.timeScale = 1.0f;
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    //�O���}�������a���`���覡�A�ϥ��u�঺�ҥH�N�u�]true
    //�o�Ӹ}���̤~��]false
    public void SetIsDead()
    {
        isDead = true;
    }


}
