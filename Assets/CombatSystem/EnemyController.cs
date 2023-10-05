using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// ���A���������A��I�s�����ʧ@���a��
/// </summary>

public class EnemyController : MonoBehaviour
{
    private EnemyMovement enemyMovement; //�ޥΰ���ʧ@���}��

    public enum EnemyState //�|�ت��A
    {
        Idle,
        Wander,
        Chase,
        Attack
    }

    public EnemyState currentState;//�s��e���A

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();

        if (enemyMovement == null) //�S���N������
        {
            enabled = false;
            return;
        }

        //��l�Ƭ����m���A
        SetState(EnemyState.Idle); 

    }

    private void Update()
    {
        switch (currentState) //�̷�e���A���X�����ʧ@
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


    public EnemyState GetState() //���o��e���A
    {
        return currentState;
    }

    public void SetState(EnemyState newState) //�ΨӴ����A
    {
        currentState = newState;
    }
}
