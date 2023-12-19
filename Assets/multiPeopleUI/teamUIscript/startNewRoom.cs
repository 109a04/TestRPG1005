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
        // 這個是還沒切換到多人模式，在多人組隊的畫面切回來用的那個按鈕
        // 這是什麼註解，根本不是切回去的按鈕，這個是新創房間的按鈕
    }

    public void OnClickCallBack()
    {
        SceneManager.UnloadSceneAsync("teamUI");
        
        //SceneManager.LoadSceneAsync("AnimationTest");
        
        NetworkRunnerHandler networkRunnerHandler = FindObjectOfType<NetworkRunnerHandler>();

        /*if(mainMenuUIHandler == null)
        {
            Debug.Log("mainMenuUIHandler 在搞");
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
            Debug.Log("NetworkRunnerHandler 在搞");
        }
        string CostomSessionName = playerAttributeManager.Instance.pname;
        Debug.Log($"ㄚ名字是{playerAttributeManager.Instance.pname}");
        networkRunnerHandler.CreateGame(CostomSessionName, "multiPeople");

    }
}
