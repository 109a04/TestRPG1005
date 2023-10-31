using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ҫ��ʵe
/// </summary>

public class HeroMotion : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")) //�����U�W�U���k�A����樫�ʵe
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        //������D�ʵe
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("JumpTrigger");

        }
    }
}