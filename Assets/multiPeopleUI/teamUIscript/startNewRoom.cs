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
        // �o�ӬO�٨S������h�H�Ҧ��A�b�h�H�ն����e�����^�ӥΪ����ӫ��s
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
