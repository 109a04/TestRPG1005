using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qTargetTrigger : Interactable
{
    private QuestTarget qTarget;
    private Talkable qTalk;
    private bool isFinish;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        qTarget = this.gameObject.GetComponent<QuestTarget>();
        qTalk = this.gameObject.GetComponent<Talkable>();
        isFinish = false;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (isInRange && Input.GetKeyDown(KeyCode.F) && isFinish == false)
        {
            if(qTalk != null)
            {
                qTalk.StartTalk();//¥æ½Í¥ô°È
            }
            qTarget.questTargetCheck();
            
            Debug.Log("Hiding UI elements");
            nameText.gameObject.SetActive(false);
            hintText.gameObject.SetActive(false);
            pressF.gameObject.SetActive(false);

            isFinish = true;
        }
    }
}
