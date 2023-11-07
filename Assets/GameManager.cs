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

        //��q�^�_��A�ͦ�����w�a�I
        StartCoroutine(RestartPositonAfterDelay(1));
        ResetGameState();
        DeathUI.SetActive(isDead);

    }
    //�n���S��k���} ���ܨt�Ϊ��Y�F�観���Y���ڬݤ���

    private void ResetGameState()
    {
        Debug.Log("���s�}�l");
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
        yield return new WaitForSeconds(delay); // ���ݫ��w�����
        playerModel.transform.position = new Vector3(18, 2, -7); //�N���a���ʨ쭫���I
    }
}
