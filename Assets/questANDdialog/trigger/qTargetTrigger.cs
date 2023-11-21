using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qTargetTrigger : Interactable
{
    private QuestTarget qTarget;
    private Talkable qTalk;
    private bool checkTalk = false;//任務物品觸發限制

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        qTarget = this.gameObject.GetComponent<QuestTarget>();
        qTalk = this.gameObject.GetComponent<Talkable>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (checkTalk)//任務物品只能觸發1次
        {
            nameText.gameObject.SetActive(false);
            hintText.gameObject.SetActive(false);
            pressF.gameObject.SetActive(false);
            return;
        }

        bool checkF = false;//未接任務不顯示UI

        for (int i = 0; i < QuestManager.instance.questList.Count; i++)
        {
            if (qTarget.targetID == QuestManager.instance.questList[i].targetID)
            {
                checkF = true;
                
                if (isInRange)
                {
                    nameText.gameObject.SetActive(true);
                    hintText.gameObject.SetActive(true);
                    pressF.gameObject.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        if (qTalk != null)
                        {
                            qTalk.StartTalk();//交談任務
                        }
                        qTarget.questTargetCheck();
                        checkTalk = true;//限制只能觸發1次
                    }
                }
            }
        }

        if (!checkF)
        {
            nameText.gameObject.SetActive(false);
            hintText.gameObject.SetActive(false);
            pressF.gameObject.SetActive(false);
        }
    }
}
