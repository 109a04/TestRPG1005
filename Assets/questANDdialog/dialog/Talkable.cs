using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talkable : MonoBehaviour
{
    public string[] lines;
    private bool playerInRange;
    private bool diaFinish = false;
    private QuestTarget questTScript;

    void Start()
    {
        //抓取是對話任務的任務目標的target腳本
        questTScript = GetComponent<QuestTarget>();
    }

    /*
    private void OnCollisionEnter(Collision other)
    {
        //玩家接觸npc
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("撞到npc拉");

            //如果是對話任務的target
            if(questTScript != null && diaFinish == false)
            {
                for (int i = 0; i < QuestManager.instance.questList.Count; i++)
                {
                    if (questTScript.targetID == QuestManager.instance.questList[i].targetID)
                    {
                        dialogManager.instance.ShowDialogue(lines);
                        diaFinish = true;
                        break;
                        
                    }

                }
            }
            else//單純對話腳本
            {
                dialogManager.instance.ShowDialogue(lines);
                diaFinish = true;
            }
        }   
    }

    
    private void OnCollisionExit(Collision other)
    {
        //玩家離開npc
        if (other.gameObject.CompareTag("Player"))
        {
            //playerInRange = false;
            Debug.Log("離開npc拉");
        }
    }
    */

    //如果是對話任務的target
    public void StartTalk()
    {
        if (questTScript != null && diaFinish == false)
        {
            for (int i = 0; i < QuestManager.instance.questList.Count; i++)
            {
                if (questTScript.targetID == QuestManager.instance.questList[i].targetID)
                {
                    dialogManager.instance.ShowDialogue(lines);
                    diaFinish = true;
                    break;

                }

            }
        }
        else//單純對話腳本
        {
            dialogManager.instance.ShowDialogue(lines);
            diaFinish = true;
        }
    }
}
