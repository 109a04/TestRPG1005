using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class startNewRoom : MonoBehaviour
{
    public GameObject statusPanel;
    public GameObject sessionListPanel;
    //MainMenuUIHandler mainMenuUIHandler;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickCallBack);
        // �o�ӬO�٨S������h�H�Ҧ��A�b�h�H�ն����e�����^�ӥΪ����ӫ��s
        // �o�O������ѡA�ڥ����O���^�h�����s�A�o�ӬO�s�Щж������s
    }

    public void OnClickCallBack()
    {
        SceneManager.UnloadSceneAsync("teamUI");
        
        //SceneManager.LoadSceneAsync("AnimationTest");
        
        NetworkRunnerHandler networkRunnerHandler = FindObjectOfType<NetworkRunnerHandler>();

        /*if(mainMenuUIHandler == null)
        {
            Debug.Log("mainMenuUIHandler �b�d");
        }*/
        OnStartNewSessionClicked();
        statusPanel.gameObject.SetActive(true);
        sessionListPanel.gameObject.SetActive(false);

        //SceneManager.LoadScene("multiPeople");
        GameObject cat = GameObject.Find("CatPlayer");
        Destroy(cat);
    }

    public void OnStartNewSessionClicked()
    {
        NetworkRunnerHandler networkRunnerHandler = FindObjectOfType<NetworkRunnerHandler>();
        if (networkRunnerHandler == null)
        {
            Debug.Log("NetworkRunnerHandler �b�d");
        }
        string CostomSessionName = playerAttributeManager.Instance.pname;
        Debug.Log($"���W�r�O{playerAttributeManager.Instance.pname}");
        networkRunnerHandler.CreateGame(CostomSessionName, "multiPeople");

    }
}
