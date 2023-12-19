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
    public float wanderRadius = 10.0f;

    //視野範圍相關
    public float visionRadius = 12.0f; //視野範圍
    public float chaseRadius = 15.0f;  //追擊範圍，比視野範圍稍大
    public float attackRadius = 7.0f; //攻擊範圍，比上面兩者小


    //控制攻擊間隔的東西
    internal bool canAttack;
    internal bool isBeaten; //被打了



    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = walkSpeed;
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
