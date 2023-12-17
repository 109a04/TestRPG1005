using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject EscUI;
    public GameObject DeathUI;
    private bool isDead;
    private bool backToPoint;
    public Transform respawnPoint; //�����I
    public GameObject playerModel; //���a�ҫ�
    public float deathDelay = 2.0f; // ���`�᩵��_�����ɶ�

    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EscUI.SetActive(false);
        DeathUI.SetActive(false);
        isDead = false;
        backToPoint = true;

    }

    // Update is called once per frame
    void Update()
    {
        //���a���UEsc�A���X�Ȱ��C��������
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscUI.SetActive(true);
        }

        //���a�ͩR���k�s
        if (playerAttributeManager.Instance.hp <= 0)
        {
            isDead = true;
        }

        if (isDead)
        {
            Time.timeScale = 0f;
            DisplayDeathUI();
            Debug.Log("����");
        }
        else
        {
            Debug.Log("����");
        }

        /*
        if (playerModel.transform.position == respawnPoint.position)
        {
            Debug.Log("�^�쭫���I");
            backToPoint = true;
        }
        else
        {
            backToPoint = false;
        }
        */


    }

    public void PressEscYes()
    {
        Application.Quit();
    }

    public void PressEscNo()
    {
        EscUI.SetActive(false);
    }

    public void DisplayDeathUI()
    {
        DeathUI.SetActive(isDead);
    }

    //�C�����}
    public void RestartGame()
    {
        MoveToRespawnPoint();
        Invoke(nameof(ResetGameState), 2);

        //ResetGameState();
        isDead = false;


        
    }
    

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

    public void SetIsDead()
    {
        isDead = true;
    }

    private void MoveToRespawnPoint()
    {
        playerModel.transform.position = Utils.GetRandomSpawnPoint();
    }

}
