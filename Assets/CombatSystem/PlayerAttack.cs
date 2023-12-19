using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 玩家攻擊怪物的腳本
/// </summary>

public class PlayerAttack : MonoBehaviour
{
    protected int atkRange; //攻擊範圍（跟持有武器有關係）
    protected int damage;   //攻擊力
    protected int weaponElement;   //武器屬性

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

    public void SetPlayerAtkParameter() //設置好玩家當前的攻擊相關參數
    {
        atkRange = playerAttributeManager.Instance.atkRange;
        damage = playerAttributeManager.Instance.attack;
        weaponElement = playerAttributeManager.Instance.element;
    } 

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GetIsDead()) { return; } //玩家死了直接不幹

        if (Input.GetMouseButtonDown(0)) // 當玩家按下滑鼠左鍵
        {   
            //檢查滑鼠射線位置
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (!Physics.Raycast(ray, out hitInfo)) { return; }         //射線沒射到東西離開     
            if (!hitInfo.collider.CompareTag("Enemy")) { return; }      //檢查點擊位置是否有怪物，沒有直接離開
                
            GameObject enemyTransform = hitInfo.collider.gameObject;    //將點擊到的物件指派到 enemyTransform
            if (enemyTransform == null) { return; }
            EnemyMovement enemyMovement = enemyTransform.GetComponent<EnemyMovement>();
                        
            float distance = Vector3.Distance(transform.position, enemyTransform.transform.position);
            if (distance > atkRange) return;                            //檢查怪物是否在攻擊範圍內，沒有則回傳

            AtkParticle.Instance.PlayAtkParticle();
            HeroMotion.Instance.animator.SetTrigger("AttackTrigger");   //觸發打擊動作
            enemyMovement.TakeDamage(damage, weaponElement);            //怪物被打擊          
        }
    }
}
