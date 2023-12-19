using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// ���a�����Ǫ����}��
/// </summary>

public class PlayerAttack : MonoBehaviour
{
    protected int atkRange; //�����d��]������Z�������Y�^
    protected int damage;   //�����O
    protected int weaponElement;   //�Z���ݩ�

    public static PlayerAttack Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetPlayerAtkParameter();
    }

    public void SetPlayerAtkParameter() //�]�m�n���a��e�����������Ѽ�
    {
        atkRange = playerAttributeManager.Instance.atkRange;
        damage = playerAttributeManager.Instance.attack;
        weaponElement = playerAttributeManager.Instance.element;
    } 

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetIsDead()) { return; } //���a���F�������F

        if (Input.GetMouseButtonDown(0)) // ���a���U�ƹ�����
        {   
            //�ˬd�ƹ��g�u��m
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (!Physics.Raycast(ray, out hitInfo)) { return; }         //�g�u�S�g��F�����}     
            if (!hitInfo.collider.CompareTag("Enemy")) { return; }      //�ˬd�I����m�O�_���Ǫ��A�S���������}
                
            GameObject enemyTransform = hitInfo.collider.gameObject;    //�N�I���쪺��������� enemyTransform
            if (enemyTransform == null) { return; }
            EnemyMovement enemyMovement = enemyTransform.GetComponent<EnemyMovement>();
                        
            float distance = Vector3.Distance(transform.position, enemyTransform.transform.position);
            if (distance > atkRange) return;                            //�ˬd�Ǫ��O�_�b�����d�򤺡A�S���h�^��

            AtkParticle.Instance.PlayAtkParticle();
            HeroMotion.Instance.animator.SetTrigger("AttackTrigger");   //Ĳ�o�����ʧ@
            enemyMovement.TakeDamage(damage, weaponElement);            //�Ǫ��Q����          
        }
    }
}
