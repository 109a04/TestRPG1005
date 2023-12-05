using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMotion : MonoBehaviour
{
    public static NPCMotion Instance;
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
        
    }
}
