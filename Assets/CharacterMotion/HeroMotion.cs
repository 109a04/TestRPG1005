using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 控制角色模型動畫
/// </summary>

public class HeroMotion : MonoBehaviour
{
    public static HeroMotion Instance;
    public Animator animator;

    private void Awake()
    {
        Instance = this;
    }

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
            if (ChatManager.Instance.inputField.isFocused) return;
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

        if(playerAttributeManager.Instance.hp == 0)
        {
            animator.SetTrigger("Die");
        }
    }
}
