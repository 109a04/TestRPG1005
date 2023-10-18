using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 存各種怪物移動相關的變數，我是想說移動速度攻擊範圍的都統一啦，除非之後我們還有美國時間一個一個設定
/// </summary>


public class EnemyActionVariables : MonoBehaviour
{
    

    //速度相關
    internal float currentSpeed;
    internal float walkSpeed = 5.0f; //常態移動速度
    internal float chaseSpeed = 8.0f; //追擊狀態移動速度

    //遊蕩範圍
    internal float wanderRadius = 10.0f;

    //視野範圍相關
    internal float visionRadius = 12.0f; //視野範圍
    internal float chaseRadius = 15.0f;  //追擊範圍，比視野範圍稍大
    internal float attackRadius = 7.0f; //攻擊範圍，比上面兩者小

    //UI相關
    protected Camera mainCamera; //主相機
    internal Transform enemyTransfrom;
    internal GameObject exclamationUI; //驚嘆號UI
    protected Vector3 offset = new Vector3(0f, 2.5f, 0f); // UI元素在頭頂上的偏移值

    public GameObject StatusUI; //整塊怪物狀態欄
    public Slider hpSlider;     //血條
    public Text enemyName;      //名稱文字
    public Text enemyLevel;     //等級文字

    //控制攻擊間隔的東西
    internal bool canAttack;



    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        enemyTransfrom = GetComponent<Transform>();
        currentSpeed = walkSpeed;
        exclamationUI = GameObject.Find("Canvas/Exclamation");
        enemyName = StatusUI.transform.Find("Name").GetComponent<Text>();
        enemyLevel = StatusUI.transform.Find("Level").GetComponent<Text>();
        hpSlider = StatusUI.GetComponent<Slider>();

        
        
        if(exclamationUI == null)
        {
            Debug.LogError("Exclamation UI not found!");
        }
        else
        {   //隱藏UI
            exclamationUI.SetActive(false);
        }
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 enemyScrPos = mainCamera.WorldToScreenPoint(enemyTransfrom.position + offset);
        exclamationUI.transform.position = enemyScrPos;
    }
}
