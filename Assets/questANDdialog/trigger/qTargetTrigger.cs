using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qTargetTrigger : Interactable
{
    private QuestTarget qTarget;
    private Talkable qTalk;
    private bool checkTalk = false;//���Ȫ��~Ĳ�o����

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

        if (checkTalk)//���Ȫ��~�u��Ĳ�o1��
        {
            nameText.gameObject.SetActive(false);
            hintText.gameObject.SetActive(false);
            pressF.gameObject.SetActive(false);
            return;
        }

        bool checkF = false;//�������Ȥ����UI

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
                            qTalk.StartTalk();//��ͥ���
                        }
                        qTarget.questTargetCheck();
                        checkTalk = true;//����u��Ĳ�o1��
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
