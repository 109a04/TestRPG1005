using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qTargetTrigger : Interactable
{
    private QuestTarget qTarget;
    private Talkable qTalk;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        qTarget = this.gameObject.GetComponent<QuestTarget>();
        qTalk = this.gameObject.GetComponent<Talkable>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (isInRange && Input.GetKeyDown(KeyCode.F))
        {
            if(qTalk != null)
            {
                qTalk.StartTalk();
            }
            qTarget.questTargetCheck();
        }
    }
}
