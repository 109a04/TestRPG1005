using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject EscUI;    //結束遊戲的視窗
    public GameObject DeathUI;  //玩家死亡的視窗
    private bool isDead;        //死亡判斷
    public Transform respawnPoint; //重生點
    public GameObject playerModel; //玩家模型

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
        //當玩家按下Esc，跳出暫停遊戲的視窗
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

    //確認離開
    public void PressEscYes()
    {
        Application.Quit();
    }

    //取消
    public void PressEscNo()
    {
        EscUI.SetActive(false);
    }

    //新細明體
    public void DisplayDeathUI()
    {
        DeathUI.SetActive(isDead);
    }

    //遊戲重開
    public void RestartGame()
    {
        ResetGameState();
    }
    

    //重置玩家狀態
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

    //別的腳本讓玩家死亡的方式，反正只能死所以就只設true
    //這個腳本裡才能設false
    public void SetIsDead()
    {
        isDead = true;
    }


}
