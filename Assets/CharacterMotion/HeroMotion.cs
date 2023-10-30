using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制角色模型動畫
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
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")) //當按下上下左右，播放行走動畫
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

        //播放跳躍動畫
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("JumpTrigger");

        }
    }
}
