using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transToMulti : Interactable
{
    // Start is called before the first frame update

    public Vector3 oriPlace;
    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (isTriggered == true)
        {
            GameObject cat = GameObject.Find("Cat");
            cat.GetComponentInChildren<Camera>().enabled = false; 
            // 先把這個相機關掉，不然會有兩個卡在一起 
            SceneManager.LoadSceneAsync("teamUI", LoadSceneMode.Additive);
            // 在不關掉一個 scene 的情況下開另一個 scene
            isTriggered = !isTriggered; // 這裡不加這個的話，會一直不斷的新增一個 teamUI
            Debug.Log("are you all right?");

            //oriPlace = cat.GetComponent<Transform>().position;
            //Debug.Log("現在的位置是 " + oriPlace);
            //SceneManager.UnloadSceneAsync("SampleScene");
            //SceneManager.LoadScene("teamUI");
            // 原本想用直接切換的方法，但是這樣回到 sample 的時候貓總是掉下去死掉，後來用這個方法就不會了
            // 雖然覺得後面切換多人再切回來還是會有這個摔死的情況()不管了等之後遇到了再修
        }
    }
}
