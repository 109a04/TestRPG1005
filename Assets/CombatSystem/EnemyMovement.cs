using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent; //�Ǫ��ɯ�
    private Transform player;
    private GameObject enemy; //�Ǫ�����
    public Enemy enemyData; //�Ǫ��ƭ�
    private EnemyController stateMachine; //�ޥΪ��A��
    private EnemyActionVariables actionVariables; //�ޥΦU���ܼ�
    private Vector3 targetPos;
    private bool hasRandomPosition = false; //�O�_�w��L�H���I
    private int currentHP; //��e��q

    //���ե��ǥ��ȥ�
    public delegate void monsterDestroyed();
    public static event monsterDestroyed monsterQuest;
    //

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = GetComponent<EnemyController>();
        actionVariables = GetComponent<EnemyActionVariables>();

        player = GameObject.FindGameObjectWithTag("Player").transform; //��M���a����

        enemy = this.gameObject;
        actionVariables.StatusUI.SetActive(false); //�w�]����
        currentHP = enemyData.maxHealth; //��l�Ʀ�q
        
        //��l�ƩǪ�����P�W�ٵ���
        actionVariables.hpSlider.minValue = 0;
        actionVariables.hpSlider.maxValue = enemyData.maxHealth;
        actionVariables.hpSlider.value = currentHP;
        actionVariables.enemyName.text = enemyData.enemyName;
        actionVariables.enemyLevel.text = $"Lv. " + enemyData.level.ToString();
        actionVariables.isBeaten = false;

        if (enemy == null)
        {
            Debug.LogWarning("�å��]�w�n�A���Ǫ�����");
            return;
        }

        

        if (stateMachine == null || actionVariables == null)
        {
            enabled = false;
            return;
        }

    }

    //�b�l���Ҧ����e���ǳƻP�i�J�l���Ҧ�
    private void EnterChaseMode()
    {
        //�]�w���a��m���ؼ�
        targetPos = player.position;

        //���ܦ��l���Ҧ����t��
        actionVariables.currentSpeed = actionVariables.chaseSpeed;

        //�����ĸ�UI
        actionVariables.exclamationUI.SetActive(true);

        //��V���a
        Rotate(targetPos);

        //�e�m�@�~�ǳƦn��i�J�l�����A
        stateMachine.SetState(EnemyController.EnemyState.Chase);
    }

    //���m���A���欰
    public void IdleAction() 
    {
        
        actionVariables.currentSpeed = actionVariables.walkSpeed;

        // �i�J�l�����A���޿�A�ˬd���a�O�_�i�J�����d��
        if (PlayerInRange(player, actionVariables.visionRadius))
        {
            EnterChaseMode();
        }

        //���ݤ@���i�J�C�����A

        StartCoroutine(DelayAndTransition(1, EnemyController.EnemyState.Wander));
        
        
        
    }

    //�C�����A���欰
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


        Wander();//�C��
        
    }

    //�l�����A���欰
    public void ChaseAction()
    {
        if (playerAttributeManager.Instance.hp <= 0) stateMachine.SetState(EnemyController.EnemyState.Idle);
        actionVariables.StatusUI.SetActive(true);
        targetPos = player.position;
        
        if (actionVariables.isBeaten != true) //�ǨS���Q���ɤ~�|�^�춢�m�Ҧ��A���M�N�|�l��ѲP��������r�쪱�a
        {
            //�Y���a���}�l���d��h�^�춢�m�Ҧ�
            if (!PlayerInRange(player, actionVariables.chaseRadius))
            {
                hasRandomPosition = false; //�^�h����@���H���I
                                           //�^�춢�m���A

                StartCoroutine(HideStatusUI());
                stateMachine.SetState(EnemyController.EnemyState.Idle);
            }
        }
        

        

        //���a�󱵪�ĤH�F��i�����d��ɶi�J�����Ҧ�
        if(PlayerInRange(player, actionVariables.attackRadius))
        {
            actionVariables.currentSpeed *= 0.75f;
            stateMachine.SetState(EnemyController.EnemyState.Attack);
        }

        //����0.75���}�l�l��
        StartCoroutine(DelayAndChase(0.75f));

    }

    //�������A���欰
    public void AttackAction()
    {
        if (playerAttributeManager.Instance.hp <= 0) stateMachine.SetState(EnemyController.EnemyState.Idle);
        actionVariables.isBeaten = false;
        actionVariables.StatusUI.SetActive(true);
        if (actionVariables.canAttack)
        {
            //�������j���@��A�]�i�H��
            StartCoroutine(DelayAndAttack(1));  
        }

        //���a���}�����d��^��l�����A
        if(!PlayerInRange(player, actionVariables.attackRadius))
        {
            actionVariables.currentSpeed = actionVariables.chaseSpeed;
            stateMachine.SetState(EnemyController.EnemyState.Chase);
        }

        //����|�����a���`�ɦ^�춢�m���A
        if (GameManager.Instance.GetIsDead())
        {
            Invoke(nameof(HideStatusUI), 1);
            //StartCoroutine(HideStatusUI());
            stateMachine.SetState(EnemyController.EnemyState.Idle);
        }
    }

    //���ʨ���w�a�I
    private void Move(Vector3 vector)
    {
        //�Ny�ȩT�w�b�ĤH�쥻��y�ȤW
        vector.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, vector, actionVariables.currentSpeed * Time.deltaTime);
    }

    //���V�ت��a
    private void Rotate(Vector3 vector)
    {
        //�Ny�ȩT�w�b�ĤH�쥻��y�ȤW
        vector.y = transform.position.y;
        transform.LookAt(vector);
    }

    //�M���B�z���ʪ��ܼ�
    private void MoveToTarget(Vector3 target)
    {
        // ���ʦV�ؼЦ�m
        SetDestination(target);

        // �ϩǪ����V�ؼ��I
        Rotate(target);
    }

    void SetDestination(Vector3 destination)
    {
        // �]�w NavMesh Agent ���ؼ��I
        if (navMeshAgent != null && navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.destination = destination;
        }
    }

    private void Wander()
    {
        //�e���C���ت��I
        MoveToTarget(targetPos);

        // �P�_�O�_����ؼЦ�m�A�Y�w��F�ت��a�h���^���m���A
        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            hasRandomPosition = false;
            stateMachine.SetState(EnemyController.EnemyState.Idle);
        }
    }

    //����H���I
    private Vector3 GetRandomPos() 
    {
        // �b�Ǫ��T�w�d���H����ܤ@���I
        Vector2 randomDirection = Random.insideUnitCircle.normalized * actionVariables.wanderRadius;
        Vector3 randomPosition = new Vector3(randomDirection.x, 0, randomDirection.y) + transform.position;

        // �����H����m�b�Ǫ��̤j���ʽd��
        randomPosition.x = Mathf.Clamp(randomPosition.x, transform.position.x - actionVariables.wanderRadius, transform.position.x + actionVariables.wanderRadius);
        randomPosition.z = Mathf.Clamp(randomPosition.z, transform.position.z - actionVariables.wanderRadius, transform.position.z + actionVariables.wanderRadius);

        return randomPosition;
    }

    //���w���ݦh�֬��������A�]�p���I�^
    private IEnumerator DelayAndTransition(float delaySeconds, EnemyController.EnemyState nextState)
    {
        yield return new WaitForSeconds(delaySeconds); 
        stateMachine.SetState(nextState);
    }


    private IEnumerator DelayAndChase(float delaySeconds)
    {

        yield return new WaitForSeconds(delaySeconds);

        //������ĸ�UI
        actionVariables.exclamationUI.SetActive(false);

        MoveToTarget(targetPos);
    }

    private IEnumerator HideStatusUI()
    {
        yield return new WaitForSeconds(1f);

        actionVariables.StatusUI.SetActive(false);
    }
    
    //�������a���禡
    public void Attack()
    {
        targetPos = player.position;
        Rotate(targetPos);
        Player.Instance.Beaten(enemyData.attack, (int)enemyData.enemyElement);
    }

    //�W�w�C�X������@�����a
    private IEnumerator DelayAndAttack(float delaySeconds)
    {
        actionVariables.canAttack = false;
        Attack();
        yield return new WaitForSeconds(delaySeconds);
        
        actionVariables.canAttack = true;
    }

    //�Ǫ��D�쪱�a���������
    public void TakeDamage(float damage, int weaponElement)
    {
        //��Ǫ��Q�����ɡA���ת��a���L�b�����d�򤺳��|�i�J�l���Ҧ�
        actionVariables.isBeaten = true;
        EnterChaseMode();

        
        if (weaponElement != 0) //�Y�Z���ݩʤ����L
        {
            /// �����۫g��
            /// �� = 1, �� = 2, �� = 3, �g = 4
            ///
            if (weaponElement - (int)enemyData.enemyElement == -1) //�Z���ݩʫg��Ǫ��ݩ�
            {
                damage *= 1.25f;
            }
            else if (weaponElement - (int)enemyData.enemyElement == 1) //�Z���ݩʳQ�Ǫ��ݩʫg��
            {
                damage *= 0.75f;
            } 

            if(weaponElement == 1 && (int)enemyData.enemyElement == 4) //�Z�����Ǫ��g
            {
                damage *= 0.75f;
            }

            if (weaponElement == 4 && (int)enemyData.enemyElement == 1) //�Z���g�Ǫ���
            {
                damage *= 1.25f;
            } 
        }


        //�ˬd�O�_����
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

    //���p�Ǫ��Q���ѡA�P���C������
    public void IsKilled()
    {
        if(currentHP == 0)
        {
            actionVariables.currentSpeed = 0;
            playerAttributeManager.Instance.exp += enemyData.rewardExp; //��o�g��
            Player.Instance.SyncCurrentExp();
            Player.Instance.SetEXPUI();
            ChatManager.Instance.SystemMessage($"��o�g��<color=#F5EC3D>{enemyData.rewardExp}</color>�C");
            actionVariables.exclamationUI.SetActive(false); //����UI
            actionVariables.StatusUI.SetActive(false); 
            Destroy(gameObject); //�P������
            monsterQuest();//���ǥ��ȴ���
        }
    }

    //�T�{�O�_�b�d��
    public bool PlayerInRange(Transform player, float range)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= range;
    }

    private void Update()
    {
        MouseOverEnemy();
    }

    private void MouseOverEnemy() //�ƹ��b�Ǫ��W������UI
    {
        //��ƹ��b���a��O���g�u
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo)) //�p�G�g�u�g��Ǫ��A�h���UI
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > playerAttributeManager.Instance.atkRange) return;
            if (hitInfo.collider.gameObject == gameObject)
            {
                actionVariables.StatusUI.SetActive(true);
            }
            else
            {
                //���p�ƹ����}�Ǫ��W��Ӫ��a�]���b�l���d�򤺮ɡA����UI
                if(stateMachine.currentState != EnemyController.EnemyState.Chase || stateMachine.currentState != EnemyController.EnemyState.Attack)
                {
                    actionVariables.StatusUI.SetActive(false);
                }
                
            }
        }
    }
}
