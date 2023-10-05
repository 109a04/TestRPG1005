using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    private Transform player;
    private GameObject enemy; //�Ǫ�����
    public Enemy enemyData; //�Ǫ��ƭ�
    private EnemyController stateMachine; //�ޥΪ��A��
    private EnemyActionVariables actionVariables; //�ޥΦU���ܼ�
    private Vector3 targetPos;
    private bool hasRandomPosition = false; //�O�_�w��L�H���I

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //��M���a����

        enemy = this.gameObject;
        if (enemy == null)
        {
            Debug.LogWarning("�å��]�w�n�A���Ǫ�����");
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


    //���m���A���欰
    public void IdleAction() 
    {
        actionVariables.currentSpeed = actionVariables.walkSpeed;

        // �i�J�l�����A���޿�A�ˬd���a�O�_�i�J�����d��
        if (PlayerInRange(player, actionVariables.visionRadius))
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


        Wander();//�C��
        
    }

    //�l�����A���欰
    public void ChaseAction()
    {

        targetPos = player.position;
        
        

        //�Y���a���}�l���d��h�^�춢�m�Ҧ�
        if(!PlayerInRange(player, actionVariables.chaseRadius))
        {
            hasRandomPosition = false; //�^�h����@���H���I
            //�^�춢�m���A
            stateMachine.SetState(EnemyController.EnemyState.Idle); 
        }

        //���a�󱵪�ĤH�F��i�����d��ɶi�J�����Ҧ�
        else if(PlayerInRange(player, actionVariables.attackRadius))
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
        Move(target);

        // �ϩǪ����V�ؼ��I
        Rotate(target);
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
    
    //�������a���禡
    public void Attack()
    {
        targetPos = player.position;
        Rotate(targetPos);
        Player.Instance.IncreaseHealth(-enemyData.attack); //�ˮ`���a
    }

    //�W�w�C�X������@�����a
    private IEnumerator DelayAndAttack(float delaySeconds)
    {
        actionVariables.canAttack = false;
        Attack();
        yield return new WaitForSeconds(delaySeconds);
        
        actionVariables.canAttack = true;
    }

    //�T�{�O�_�b�d��
    public bool PlayerInRange(Transform player, float range)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= range;
    }
}
