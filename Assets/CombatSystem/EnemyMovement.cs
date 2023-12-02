using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent; //怪物導航
    private Transform player;
    private GameObject enemy; //怪物物件
    public Enemy enemyData; //怪物數值
    private EnemyController stateMachine; //引用狀態機
    private EnemyActionVariables actionVariables; //引用各種變數
    private Vector3 targetPos;
    private bool hasRandomPosition = false; //是否已抽過隨機點
    private int currentHP; //當前血量

    //測試打怪任務用
    public delegate void monsterDestroyed();
    public static event monsterDestroyed monsterQuest;
    //

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = GetComponent<EnemyController>();
        actionVariables = GetComponent<EnemyActionVariables>();

        player = GameObject.FindGameObjectWithTag("Player").transform; //找尋玩家物件

        enemy = this.gameObject;
        actionVariables.StatusUI.SetActive(false); //預設隱藏
        currentHP = enemyData.maxHealth; //初始化血量
        
        //初始化怪物血條與名稱等級
        actionVariables.hpSlider.minValue = 0;
        actionVariables.hpSlider.maxValue = enemyData.maxHealth;
        actionVariables.hpSlider.value = currentHP;
        actionVariables.enemyName.text = enemyData.enemyName;
        actionVariables.enemyLevel.text = $"Lv. " + enemyData.level.ToString();
        actionVariables.isBeaten = false;

        if (enemy == null)
        {
            Debug.LogWarning("並未設定好適當的怪物物件");
            return;
        }

        

        if (stateMachine == null || actionVariables == null)
        {
            enabled = false;
            return;
        }

    }

    //在追擊模式之前的準備與進入追擊模式
    private void EnterChaseMode()
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

    //閒置狀態的行為
    public void IdleAction() 
    {
        
        actionVariables.currentSpeed = actionVariables.walkSpeed;

        // 進入追擊狀態的邏輯，檢查玩家是否進入視野範圍
        if (PlayerInRange(player, actionVariables.visionRadius))
        {
            EnterChaseMode();
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
            EnterChaseMode();
        }


        Wander();//遊蕩
        
    }

    //追擊狀態的行為
    public void ChaseAction()
    {
        if (playerAttributeManager.Instance.hp <= 0) stateMachine.SetState(EnemyController.EnemyState.Idle);
        actionVariables.StatusUI.SetActive(true);
        targetPos = player.position;
        
        if (actionVariables.isBeaten != true) //怪沒有被打時才會回到閒置模式，不然就會追到天涯海角直到咬到玩家
        {
            //若玩家離開追擊範圍則回到閒置模式
            if (!PlayerInRange(player, actionVariables.chaseRadius))
            {
                hasRandomPosition = false; //回去重抽一次隨機點
                                           //回到閒置狀態

                StartCoroutine(HideStatusUI());
                stateMachine.SetState(EnemyController.EnemyState.Idle);
            }
        }
        

        

        //當玩家更接近敵人達到可攻擊範圍時進入攻擊模式
        if(PlayerInRange(player, actionVariables.attackRadius))
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
        if (playerAttributeManager.Instance.hp <= 0) stateMachine.SetState(EnemyController.EnemyState.Idle);
        actionVariables.isBeaten = false;
        actionVariables.StatusUI.SetActive(true);
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
        if (GameManager.Instance.GetIsDead())
        {
            Invoke(nameof(HideStatusUI), 1);
            //StartCoroutine(HideStatusUI());
            stateMachine.SetState(EnemyController.EnemyState.Idle);
        }
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
        SetDestination(target);

        // 使怪物面向目標點
        Rotate(target);
    }

    void SetDestination(Vector3 destination)
    {
        // 設定 NavMesh Agent 的目標點
        if (navMeshAgent != null && navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.destination = destination;
        }
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

    private IEnumerator HideStatusUI()
    {
        yield return new WaitForSeconds(1f);

        actionVariables.StatusUI.SetActive(false);
    }
    
    //攻擊玩家的函式
    public void Attack()
    {
        targetPos = player.position;
        Rotate(targetPos);
        Player.Instance.Beaten(enemyData.attack, (int)enemyData.enemyElement);
    }

    //規定每幾秒攻擊一次玩家
    private IEnumerator DelayAndAttack(float delaySeconds)
    {
        actionVariables.canAttack = false;
        Attack();
        yield return new WaitForSeconds(delaySeconds);
        
        actionVariables.canAttack = true;
    }

    //怪物遭到玩家攻擊的函數
    public void TakeDamage(float damage, int weaponElement)
    {
        //當怪物被攻擊時，不論玩家有無在攻擊範圍內都會進入追擊模式
        actionVariables.isBeaten = true;
        EnterChaseMode();

        
        if (weaponElement != 0) //若武器屬性不為無
        {
            /// 元素相剋表
            /// 水 = 1, 火 = 2, 草 = 3, 土 = 4
            ///
            if (weaponElement - (int)enemyData.enemyElement == -1) //武器屬性剋制怪物屬性
            {
                damage *= 1.25f;
            }
            else if (weaponElement - (int)enemyData.enemyElement == 1) //武器屬性被怪物屬性剋制
            {
                damage *= 0.75f;
            } 

            if(weaponElement == 1 && (int)enemyData.enemyElement == 4) //武器水怪物土
            {
                damage *= 0.75f;
            }

            if (weaponElement == 4 && (int)enemyData.enemyElement == 1) //武器土怪物水
            {
                damage *= 1.25f;
            } 
        }


        //檢查是否打死
        if ((currentHP - damage) > 0)
        {
            currentHP -= (int)damage;
            actionVariables.hpSlider.value = currentHP;
        }
        else
        {
            currentHP = 0;
            actionVariables.hpSlider.value = 0;
            IsKilled();

        }

    }

    //假如怪物被擊敗，銷毀遊戲物件
    public void IsKilled()
    {
        if(currentHP == 0)
        {
            actionVariables.currentSpeed = 0;
            playerAttributeManager.Instance.exp += enemyData.rewardExp; //獲得經驗
            Player.Instance.SyncCurrentExp();
            Player.Instance.SetEXPUI();
            ChatManager.Instance.SystemMessage($"獲得經驗<color=#F5EC3D>{enemyData.rewardExp}</color>。");
            actionVariables.exclamationUI.SetActive(false); //隱藏UI
            actionVariables.StatusUI.SetActive(false); 
            Destroy(gameObject); //銷毀物件
            monsterQuest();//打怪任務測試
        }
    }

    //確認是否在範圍內
    public bool PlayerInRange(Transform player, float range)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= range;
    }

    private void Update()
    {
        MouseOverEnemy();
    }

    private void MouseOverEnemy() //滑鼠在怪物上方時顯示UI
    {
        //把滑鼠在的地方記成射線
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo)) //如果射線射到怪物，則顯示UI
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > playerAttributeManager.Instance.atkRange) return;
            if (hitInfo.collider.gameObject == gameObject)
            {
                actionVariables.StatusUI.SetActive(true);
            }
            else
            {
                //假如滑鼠離開怪物上方而玩家也不在追擊範圍內時，隱藏UI
                if(stateMachine.currentState != EnemyController.EnemyState.Chase || stateMachine.currentState != EnemyController.EnemyState.Attack)
                {
                    actionVariables.StatusUI.SetActive(false);
                }
                
            }
        }
    }
}
