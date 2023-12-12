using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyCamera : MonoBehaviour
{
    // Start is called before the first frame update
    // 動態的生成一個相機，因為場景中的 main camera 是大家共用的
    // 連線的時候大家類似於進入到同一個 scene 裡面，這樣的話 camera 會出現競爭的狀態
    // 會導致只有一個場景有畫面，其他場景的畫面是空白的
    // 但又不確定會有多少個玩家出現在場景裡面，所以要動態的生成相機

    public GameObject copyCamera;//要被複製的物件
    public GameObject superGameObject;//要被放置在哪個物件底下

    private GameObject childGameObject;//被複製出來的物件
    void Start()
    {
        //copyGameObject = GameObject.Find("MainCamera");
        childGameObject = Instantiate(copyCamera);//複製copyGameObject物件(連同該物件身上的腳本一起複製)
        childGameObject.transform.parent = superGameObject.transform;//放到superGameObject物件內
        childGameObject.transform.localPosition = new Vector3(3, 3, -3);//複製出來的物件放置的座標為superGameObject物件內的原點
        
        //childGameObject.AddComponent<NullScript>(); //動態增加名為"NullScript"的腳本到此物件身上
        //下面這一行的功能為將複製出來的子物件命名為CopyObject
        childGameObject.name = "MainCamera"; 
    }
}
