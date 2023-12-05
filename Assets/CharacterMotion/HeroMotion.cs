using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ҫ��ʵe
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
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")) //����U�W�U���k�A����樫�ʵe
        {
            if (ChatManager.Instance.inputField.isFocused) return;
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

        if(playerAttributeManager.Instance.hp == 0)
        {
            animator.SetTrigger("Die");
        }
    }
}
