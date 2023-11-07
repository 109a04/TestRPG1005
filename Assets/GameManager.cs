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
        next = false;
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

    //遊戲重開，因為還沒想好存檔的要怎麼做，之後有需要存檔點的話再改（好）
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
        //血量回復後，生成到指定地點
        ResetGameState();
        DeathUI.SetActive(isDead);
        next = true;
        
    }
    

    private void ResetGameState()
    {
        Debug.Log("重新開始");
        
        
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
        Debug.Log("回到重生點");
        playerModel.transform.position = respawnPoint.position;

    }

}
