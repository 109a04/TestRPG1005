using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// �����Ǫ��O�_�i�J���a�����d�򪺸}��
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    protected int atkRange;
    protected int damage;
    protected int weapon;
    

    // Start is called before the first frame update
    void Start()
    {
        atkRange = playerAttributeManager.Instance.atkRange;
        damage = playerAttributeManager.Instance.attack;
        weapon = playerAttributeManager.Instance.weapon;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetMouseButtonDown(0)) // ���a���U�ƹ�����
        {   
            //�ˬd�ƹ��g�u��m
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo))
            {
                //�ˬd�I����m�O�_���Ǫ�
                if (hitInfo.collider.CompareTag("Enemy"))
                {
                    
                    GameObject enemyTransform = hitInfo.collider.gameObject;
                    
                    if (enemyTransform != null)
                    {
                        EnemyMovement enemyMovement = enemyTransform.GetComponent<EnemyMovement>();
                        
                        float distance = Vector3.Distance(transform.position, enemyTransform.transform.position);
                        if(distance <= atkRange && !GameManager.Instance.IsDead()) //��Ǫ��b�i�����d�򤺡A�B���a�D���`���A
                        {
                            HeroMotion.Instance.animator.SetTrigger("AttackTrigger");
                            enemyMovement.TakeDamage(damage, weapon);
                        }
                    }
                    else
                    {
                        Debug.LogWarning("EnemyMovement component not found on the object.");
                    }
                }
                
            }
        }
    }
}
