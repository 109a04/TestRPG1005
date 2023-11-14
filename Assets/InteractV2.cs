using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 遊戲可互動物件交互UI二版，不知道會不會好一點
/// 此方法必須在物件上掛一個Trigger才能運作，不能用Collider的方式
/// </summary>

public class InteractV2 : MonoBehaviour
{
    public string objName; //物件或角色的名字
    public string prompt;  //互動提示 ex.交談, 拾取...

    public Text objNameText; //名稱文字
    public Text promptText; //互動提示文字
    public Image pressF;   //那個F的圖示

    public Transform objTransform; //物件的位置
    private Vector3 objPos;
    private Camera mainCamera; //主世界相機

    //UI高度調整
    private Vector3 objNameOffset = new Vector3(0f, 2f, 0f); 
    private Vector3 pressFOffset = new Vector3(0f, 80f, 0f);
    private Vector3 promptOffset = new Vector3(0f, 125f, 0f);

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
    }

    private void OnTriggerExit(Collider other) //離開觸發範圍後隱藏
    {
        hide();
    }


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        objNameText.text = $"<color=#FFD700>{objName}</color>\n";
        promptText.text = prompt;
        hide(); //初始狀態為隱藏
    }

    // Update is called once per frame
    void Update()
    {
        //對齊螢幕位置
        objPos = mainCamera.WorldToScreenPoint(objTransform.position + objNameOffset);
        objNameText.transform.position = objPos;
        pressF.transform.position = objPos + pressFOffset;
        promptText.transform.position = objPos + promptOffset;
    }
}
