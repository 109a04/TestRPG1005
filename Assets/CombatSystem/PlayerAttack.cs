using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 偵測怪物是否進入玩家攻擊範圍的腳本
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
        
        if(Input.GetMouseButtonDown(0)) // 當玩家按下滑鼠左鍵
        {   
            //檢查滑鼠射線位置
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray, out hitInfo))
            {
                //檢查點擊位置是否有怪物
                if (hitInfo.collider.CompareTag("Enemy"))
                {
                    
                    GameObject enemyTransform = hitInfo.collider.gameObject;
                    
                    if (enemyTransform != null)
                    {
                        EnemyMovement enemyMovement = enemyTransform.GetComponent<EnemyMovement>();
                        
                        float distance = Vector3.Distance(transform.position, enemyTransform.transform.position);
                        if(distance <= atkRange && !GameManager.Instance.IsDead()) //當怪物在可攻擊範圍內，且玩家非死亡狀態
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
