using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;
    private GameObject enemy; //怪物物件
    public Enemy enemyData; //怪物數值
    private EnemyController stateMachine; //引用狀態機
    private EnemyActionVariables actionVariables; //引用各種變數
    private Vector3 targetPos;
    private bool hasRandomPosition = false; //是否已抽過隨機點

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //找尋玩家物件

        enemy = this.gameObject;
        if (enemy == null)
        {
            Debug.LogWarning("並未設定好適當的怪物物件");
            return;
        }

        stateMachine = GetComponent<EnemyController>();
        actionVariables = GetComponent<EnemyActionVariables>();

        if (stateMachine == null || actionVariables == null)
        {
            enabled = false;
            return;
        }

    }


    //閒置狀態的行為
    public void IdleAction() 
    {
        actionVariables.currentSpeed = actionVariables.walkSpeed;

        // 進入追擊狀態的邏輯，檢查玩家是否進入視野範圍
        if (PlayerInRange(player, actionVariables.visionRadius))
        {
            //設定玩家位置為目標
            targetPos = player.position;

            //改變成追擊模式的速度
            actionVariables.currentSpeed = actionVariables.chaseSpeed;

            //顯示驚嘆號UI
            actionVariables.exclamationUI.SetActive(true);

            //轉向玩家
            Rotate(targetPos);

            //前置作業準備好後進入追擊狀態
            stateMachine.SetState(EnemyController.EnemyState.Chase); 
        }

        //等待一秒後進入遊蕩狀態

        StartCoroutine(DelayAndTransition(1, EnemyController.EnemyState.Wander));
        
        
        
    }

    //遊蕩狀態的行為
    public void WanderAction()
    {
        if (!hasRandomPosition)
        {
            targetPos = GetRandomPos();
            hasRandomPosition = true;
        }


        if (PlayerInRange(player, actionVariables.visionRadius))
        {
            //設定玩家位置為目標
            targetPos = player.position;

            //改變成追擊模式的速度
            actionVariables.currentSpeed = actionVariables.chaseSpeed;

            //顯示驚嘆號UI
            actionVariables.exclamationUI.SetActive(true);

            //轉向玩家
            Rotate(targetPos);

            //前置作業準備好後進入追擊狀態
            stateMachine.SetState(EnemyController.EnemyState.Chase);
        }


        Wander();//遊蕩
        
    }

    //追擊狀態的行為
    public void ChaseAction()
    {

        targetPos = player.position;
        
        

        //若玩家離開追擊範圍則回到閒置模式
        if(!PlayerInRange(player, actionVariables.chaseRadius))
        {
            hasRandomPosition = false; //回去重抽一次隨機點
            //回到閒置狀態
            stateMachine.SetState(EnemyController.EnemyState.Idle); 
        }

        //當玩家更接近敵人達到可攻擊範圍時進入攻擊模式
        else if(PlayerInRange(player, actionVariables.attackRadius))
        {
            actionVariables.currentSpeed *= 0.75f;
            stateMachine.SetState(EnemyController.EnemyState.Attack);
        }

        //等待0.75秒後開始追擊
        StartCoroutine(DelayAndChase(0.75f));

    }

    //攻擊狀態的行為
    public void AttackAction()
    {

        if (actionVariables.canAttack)
        {
            //攻擊間隔為一秒，也可以改
            StartCoroutine(DelayAndAttack(1));  
        }

        //當玩家離開攻擊範圍回到追擊狀態
        if(!PlayerInRange(player, actionVariables.attackRadius))
        {
            actionVariables.currentSpeed = actionVariables.chaseSpeed;
            stateMachine.SetState(EnemyController.EnemyState.Chase);
        }

        //之後會有當玩家死亡時回到閒置狀態
    }

    //移動到指定地點
    private void Move(Vector3 vector)
    {
        //將y值固定在敵人原本的y值上
        vector.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, vector, actionVariables.currentSpeed * Time.deltaTime);
    }

    //面向目的地
    private void Rotate(Vector3 vector)
    {
        //將y值固定在敵人原本的y值上
        vector.y = transform.position.y;
        transform.LookAt(vector);
    }

    //專門處理移動的變數
    private void MoveToTarget(Vector3 target)
    {
        // 移動向目標位置
        Move(target);

        // 使怪物面向目標點
        Rotate(target);
    }

    private void Wander()
    {
        //前往遊蕩目的點
        MoveToTarget(targetPos);

        // 判斷是否接近目標位置，若已到達目的地則跳回閒置狀態
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            hasRandomPosition = false;
            stateMachine.SetState(EnemyController.EnemyState.Idle);
        }
    }

    //獲取隨機點
    private Vector3 GetRandomPos() 
    {
        // 在怪物固定範圍內隨機選擇一個點
        Vector2 randomDirection = Random.insideUnitCircle.normalized * actionVariables.wanderRadius;
        Vector3 randomPosition = new Vector3(randomDirection.x, 0, randomDirection.y) + transform.position;

        // 限制隨機位置在怪物最大移動範圍內
        randomPosition.x = Mathf.Clamp(randomPosition.x, transform.position.x - actionVariables.wanderRadius, transform.position.x + actionVariables.wanderRadius);
        randomPosition.z = Mathf.Clamp(randomPosition.z, transform.position.z - actionVariables.wanderRadius, transform.position.z + actionVariables.wanderRadius);

        return randomPosition;
    }

    //指定等待多少秒後切換狀態（小數點）
    private IEnumerator DelayAndTransition(float delaySeconds, EnemyController.EnemyState nextState)
    {
        yield return new WaitForSeconds(delaySeconds); 
        stateMachine.SetState(nextState);
    }


    private IEnumerator DelayAndChase(float delaySeconds)
    {

        yield return new WaitForSeconds(delaySeconds);

        //隱藏驚嘆號UI
        actionVariables.exclamationUI.SetActive(false);

        MoveToTarget(targetPos);
    }
    
    //攻擊玩家的函式
    public void Attack()
    {
        targetPos = player.position;
        Rotate(targetPos);
        Player.Instance.IncreaseHealth(-enemyData.attack); //傷害玩家
    }

    //規定每幾秒攻擊一次玩家
    private IEnumerator DelayAndAttack(float delaySeconds)
    {
        actionVariables.canAttack = false;
        Attack();
        yield return new WaitForSeconds(delaySeconds);
        
        actionVariables.canAttack = true;
    }

    //確認是否在範圍內
    public bool PlayerInRange(Transform player, float range)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= range;
    }
}
