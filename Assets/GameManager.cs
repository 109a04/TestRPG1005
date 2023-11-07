using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject EscUI;
    public GameObject DeathUI;
    private bool isDead;
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

        //血量回復後，生成到指定地點
        StartCoroutine(RestartPositonAfterDelay(1));
        ResetGameState();
        DeathUI.SetActive(isDead);

    }
    //好像沒辦法重開 跟對話系統的某東西有關係但我看不懂

    private void ResetGameState()
    {
        Debug.Log("重新開始");
        isDead = false;
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

    private IEnumerator RestartPositonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 等待指定的秒數
        playerModel.transform.position = new Vector3(18, 2, -7); //將玩家移動到重生點
    }
}
