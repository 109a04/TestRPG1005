using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject EscUI;
    public GameObject DeathUI;

    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        EscUI.SetActive(false);
        DeathUI.SetActive(false);
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

            //顯示死亡訊息
            DisplayDeathUI();

            //暫停遊戲
            Time.timeScale = 0f;

        }
    }

    public void PressYes()
    {
        Application.Quit();
    }

    public void PressNo()
    {
        EscUI.SetActive(false);
    }

    public void DisplayDeathUI()
    {
        DeathUI.SetActive(true);
    }

    /*遊戲重開，因為還沒想好存檔的要怎麼做，之後有需要存檔點的話再改（好）
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResetGameState();

    }好像沒辦法重開  跟對話系統的某東西有關係但我看不懂*/

    private void ResetGameState()
    {
        Time.timeScale = 1.0f;
    }
}
