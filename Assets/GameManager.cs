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
        //���a���UEsc�A���X�Ȱ��C��������
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscUI.SetActive(true);
        }

        //���a�ͩR���k�s
        if (Player.Instance.GetCurrentHealth() <= 0)
        {

            //��ܦ��`�T��
            DisplayDeathUI();

            //�Ȱ��C��
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

    /*�C�����}�A�]���٨S�Q�n�s�ɪ��n��򰵡A���ᦳ�ݭn�s���I���ܦA��]�n�^
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResetGameState();

    }�n���S��k���}  ���ܨt�Ϊ��Y�F�観���Y���ڬݤ���*/

    private void ResetGameState()
    {
        Time.timeScale = 1.0f;
    }
}
