using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startNewRoom : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickCallBack);
        // 這個是還沒切換到多人模式，在多人組隊的畫面切回來用的那個按鈕
    }

    public void OnClickCallBack()
    {
        SceneManager.UnloadSceneAsync("teamUI");
        SceneManager.LoadScene("multiPeople");
        SceneManager.LoadSceneAsync("AnimationTest");
        GameObject cat = GameObject.Find("CatPlayer");
        Destroy(cat);
    }
}
