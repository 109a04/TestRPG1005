using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ReturnToMyGame : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickCallBack);
        // 這個是還沒切換到多人模式，在多人組隊的畫面切回來用的那個按鈕
    }

    public void OnClickCallBack()
    {
        // 註解的這些都是，原本想要傳送回指定的地點，但是好像會垂直降落死掉
        // 最後發現了很簡單的方法（）完全不需要傳送位置
        //Vector3 move;
        //move = new Vector3(18.0f, 10.0f, -7.5f);
        //Debug.Log("現在傳過來的位置是 " + oriPlace);
        //GameObject catNow = GameObject.Find("Cat");
        //catNow.GetComponent<Transform>().position = move; 
        //SceneManager.LoadScene("SampleScene");
        //catNow.GetComponent<Transform>().position = move;
        //Debug.Log("移動完後的位置是 " + catNow.GetComponent<Transform>().position);
        SceneManager.UnloadSceneAsync("teamUI");
        GameObject cat = GameObject.Find("Cat");
        cat.GetComponentInChildren<Camera>().enabled = true;
    }
}
