using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMotion : MonoBehaviour
{
    private Animator animator;
    private EnemyMovement enemyMovement;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        
    }

    
    // Update is called once per frame
    void Update()
    {
        switch (enemyMovement.GetCurrentState())
        {
            case 0:
                animator.SetBool("Wander", false);
                break;
            case 1:
                animator.SetBool("Wander", true);
                break; 
            case 2:
                animator.SetTrigger("Find");
                break;
            case 3:
                break;
        }
        
        
    }
}
