using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 狀態機切換狀態跟呼叫動應動作的地方
/// </summary>

public class EnemyController : MonoBehaviour
{
    private EnemyMovement enemyMovement; //引用執行動作的腳本

    public enum EnemyState //四種狀態
    {
        Idle,
        Wander,
        Chase,
        Attack
    }

    public EnemyState currentState;//存當前狀態

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();

        if (enemyMovement == null) //沒找到就不給用
        {
            enabled = false;
            return;
        }

        //初始化為閒置狀態
        SetState(EnemyState.Idle); 

    }

    private void Update()
    {
        switch (currentState) //依當前狀態做出相應動作
        {
            case EnemyState.Idle:
                
                enemyMovement.IdleAction();
                break;
            case EnemyState.Wander:
                
                enemyMovement.WanderAction();
                break;
            case EnemyState.Chase:
                
                enemyMovement.ChaseAction();
                break;
            case EnemyState.Attack:
                
                enemyMovement.AttackAction();
                break;
        }
    }


    public EnemyState GetState() //取得當前狀態
    {
        return currentState;
    }

    public void SetState(EnemyState newState) //用來換狀態
    {
        currentState = newState;
    }
}
