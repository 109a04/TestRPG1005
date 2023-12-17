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
    public Transform respawnPoint; //重生點
    public GameObject playerModel; //玩家模型
    public float deathDelay = 2.0f; // 死亡後延遲復活的時間

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
        //當玩家按下Esc，跳出暫停遊戲的視窗
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscUI.SetActive(true);
        }

        //當玩家生命值歸零
        if (playerAttributeManager.Instance.hp <= 0)
        {
            isDead = true;
        }

        if (isDead)
        {
            Time.timeScale = 0f;
            DisplayDeathUI();
            Debug.Log("死掉");
        }
        else
        {
            Debug.Log("活著");
        }

        /*
        if (playerModel.transform.position == respawnPoint.position)
        {
            Debug.Log("回到重生點");
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

    //遊戲重開
    public void RestartGame()
    {
        MoveToRespawnPoint();
        Invoke(nameof(ResetGameState), 2);

        //ResetGameState();
        isDead = false;


        
    }
    

    private void ResetGameState()
    {
        Debug.Log("重新開始");

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
