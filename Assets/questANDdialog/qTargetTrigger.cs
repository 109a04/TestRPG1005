using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qTargetTrigger : InteractV2
{
    private QuestTarget qTarget;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        qTarget = this.gameObject.GetComponent<QuestTarget>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (isInRange && Input.GetKeyDown(KeyCode.F))
        {
            qTarget.questTargetCheck();
        }
    }
}
