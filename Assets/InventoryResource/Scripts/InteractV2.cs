using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 遊戲可互動物件交互UI二版，不知道會不會好一點
/// 在Assets的首頁應該有一個"InteractPrefab"的東西，把它拉到想要互動的物件上就可以了
/// 此方法必須在物件上掛一個Trigger才能運作，不能用Collider的方式
/// </summary>



public class InteractV2 : MonoBehaviour
{
    public string objName; //物件或角色的名字
    public string prompt;  //互動提示 ex.交談, 拾取...

    private Text objNameText; //名稱文字
    private Text promptText; //互動提示文字
    private Image pressF;   //那個F的圖示

    private Transform objTransform; //物件的位置
    
    protected Camera mainCamera; //主世界相機
    protected Vector3 objPos;       //物件世界座標

    //UI高度調整
    protected Vector3 objNameOffset = new Vector3(0f, 2f, 0f);
    protected Vector3 pressFOffset = new Vector3(0f, 80f, 0f);
    protected Vector3 promptOffset = new Vector3(0f, 125f, 0f);

    protected bool isInRange;


    // Start is called before the first frame update
    public virtual void Start()
    {
        mainCamera = Camera.main;
        objTransform = GetComponent<Transform>();
        objNameText = this.transform.Find("InteractPrefab/ObjName").GetComponent<Text>();
        promptText = this.transform.Find("InteractPrefab/Prompt").GetComponent<Text>();
        pressF = this.transform.Find("InteractPrefab/PressF").GetComponent<Image>();
        objNameText.text = $"<color=#FFD700>{objName}</color>\n";   
        promptText.text = prompt;
        isInRange = false;
        hide(); //初始狀態為隱藏
    }

    // Update is called once per frame
    public virtual void Update()
    {
        //對齊螢幕位置
        objPos = mainCamera.WorldToScreenPoint(objTransform.position + objNameOffset);
        objNameText.transform.position = objPos;
        pressF.transform.position = objPos + pressFOffset;
        promptText.transform.position = objPos + promptOffset;


    }


    public void show() //顯示UI
    {
        objNameText.gameObject.SetActive(true);
        promptText.gameObject.SetActive(true);
        pressF.gameObject.SetActive(true);
        
    }
    
    public void hide() //隱藏UI
    {
        objNameText.gameObject.SetActive(false);
        promptText.gameObject.SetActive(false);
        pressF.gameObject.SetActive(false);
        
    }

    private void OnTriggerEnter(Collider other) //碰到Trigger時觸發，無須呼叫
    {
        show();
        isInRange = true;
    }

    private void OnTriggerExit(Collider other) //離開觸發範圍後隱藏
    {
        hide();
        isInRange = false;
    }



    
}
