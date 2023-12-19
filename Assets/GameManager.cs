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
    private bool next;
    public Transform respawnPoint; //�����I
    public GameObject playerModel; //���a�ҫ�
    public float deathDelay = 2.0f; // ���`�᩵��_�����ɶ�

    /* �o�̬O�h�H��
    byte[] connectionToken;

    public Vector2 cameraViewRotation = Vector2.zero;
    public string playerNickName = "";
     ��o�̵���*/

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
        next = false;

        /* �q�o�̶}�l�A�h�H�n�Ϊ��A�ڤ]���T�w�O�F�����ϥ��N�O�n��
        //Check if token is valid, if not get a new one
        if (connectionToken == null)
        {
            connectionToken = ConnectionTokenUtils.NewToken();
            Debug.Log($"Player connection token {ConnectionTokenUtils.HashToken(connectionToken)}");
        }
         ��o�̵���*/
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
        if (Player.Instance.GetCurrentHealth() <= 0)
        {
            isDead = true;
        }

        if (isDead)
        {
            Time.timeScale = 0f;
            DisplayDeathUI();
        }

        if (!backToPoint)
        {
            MoveToRespawnPoint();
        }


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

    //�C�����}�A�]���٨S�Q�n�s�ɪ��n��򰵡A���ᦳ�ݭn�s���I���ܦA��]�n�^
    public void RestartGame()
    {
        if (next)
        {
            backToPoint = true;
            isDead = false;
            next = false;
        }
        if (playerModel.transform.position != respawnPoint.position)
        {
            backToPoint = false;
        }
        //��q�^�_��A�ͦ�����w�a�I
        ResetGameState();
        DeathUI.SetActive(isDead);
        next = true;
        
    }
    

    private void ResetGameState()
    {
        Debug.Log("���s�}�l");
        
        
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
        Debug.Log("�^�쭫���I");
        playerModel.transform.position = respawnPoint.position;

    }

    /* �h�H�n�Ϊ��A�ڤ]���T�w�O�F�����ϥ��N�O�n��
    public void SetConnectionToken(byte[] connectionToken)
    {
        this.connectionToken = connectionToken;
    }

    public byte[] GetConnectionToken()
    {
        return connectionToken;
    }
     ��o�̵���*/
}
