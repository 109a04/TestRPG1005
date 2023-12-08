using UnityEngine;

public class Questable : MonoBehaviour
{
    public Quest quest;//可給玩家的任務
    public QuestTarget questTarget;//任務目標
    public bool playerInRange;
    public bool isFinish = false;

    public GameObject monsterPrefab;
    public Vector3 spawnPoint;

    //npc對話
    public string npcName;//npc名字
    public string[] giveMission = new string[1];//給任務的對話
    public string[] noComplete = new string[2];//還沒完成
    public string[] complete = new string[2];//恭喜完成
    public string[] repeat = new string[2];//不能重複做任務


    
    private void Start()
    {
        giveMission[0] = npcName;
        noComplete[0] = npcName;
        noComplete[1] = "你還沒達成條件呢，繼續加油吧！";
        complete[0] = npcName;
        complete[1] = "謝謝你啦，給你金幣 " + quest.goldReward.ToString() + " 和經驗 " + quest.expReward.ToString() + "。";
        repeat[0] = npcName;
        repeat[1] = "不能重複做任務喔！";
    }

    /*
    private void OnCollisionEnter(Collision other)
    {
        //玩家碰到npc
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("撞");
            DelegateQuest();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        //玩家離開npc
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("離");
        }
    }
    */

    public void DelegateQuest()
    {
        if (/*playerInRange && */dialogManager.instance.dialogueBox.activeInHierarchy == false)
        {
            if (quest.questStatus != Quest.QuestStatus.Completed)//任務還沒完成
            {
                if (quest.questStatus == Quest.QuestStatus.Waitting)//還沒接任務
                {
                    dialogManager.instance.ShowDialogue(giveMission);
                    quest.questStatus = Quest.QuestStatus.Accepted;//任務狀態改為accepted
                    QuestManager.instance.questList.Add(quest);//將任務改為接受後，加入玩家的任務列表

                    if(quest.questType == Quest.QuestType.Monster)
                    {
                        Debug.Log("打怪任務");
                        EnemySpawn.Instance.SpawnMonster(monsterPrefab, spawnPoint);
                    }
                }
                else//沒做完任務
                {
                    dialogManager.instance.ShowDialogue(noComplete);
                }

            }
            else//已完成任務會顯示
            {
                if (!isFinish)
                {
                    dialogManager.instance.ShowDialogue(complete);
                    OfferRewards();
                    QuestManager.instance.questList.RemoveAll(quest => quest.questStatus == Quest.QuestStatus.Completed);
                    isFinish = true;
                }
                else
                {
                    dialogManager.instance.ShowDialogue(repeat);
                }
            }
            QuestManager.instance.updateList();
        }
        
    }

    public void OfferRewards()//提供獎勵，加經驗和金幣
    {
        //加上UI同步顯示
        Player.Instance.IncreaseExp(quest.expReward);
        //playerAttributeManager.Instance.exp += quest.expReward;
        playerAttributeManager.Instance.money += quest.goldReward;
        //PlayerLevelSystem.Instance.CheckExperience();
    }
}
