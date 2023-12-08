using UnityEngine;

//放在和任務有關的目標物品上
public class QuestTarget : MonoBehaviour
{
    public int questID;
    public int targetID;
    public Quest quest;

    public enum QuestType { Gathering, Talk, Monster };
    public QuestType questType;//收集類的任務，收集完後會消失

    private void Awake()
    {
        //先訂閱怪物被消滅事件
        EnemyMovement.monsterQuest += OnMonsterDestroyed;
    }

    private void OnMonsterDestroyed()
    {
        //打怪任務則怪物消滅後才加
        for (int i = 0; i < QuestManager.instance.questList.Count; i++)
        {
            if (targetID == QuestManager.instance.questList[i].targetID)
            {
                Debug.Log(targetID);
                Debug.Log("解決任務怪物拉");
                QuestManager.instance.questList[i].ownAmount++;
                CheckQuestIsComplete();
            }
        }
    }

    /*
    //攜帶這個腳本的是任務相關人和物品
    private void OnCollisionEnter(Collision other)
    {
        //玩家碰到物品
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < QuestManager.instance.questList.Count; i++)
            {
                if (targetID == QuestManager.instance.questList[i].targetID)
                {
                    //非打怪任務的任務目標計數
                    if (QuestManager.instance.questList[i].questType == Quest.QuestType.Gathering || QuestManager.instance.questList[i].questType == Quest.QuestType.Talk)
                    {
                        QuestManager.instance.questList[i].ownAmount++;
                    }

                    //收集任務直接銷毀任務道具
                    if (QuestManager.instance.questList[i].questType == Quest.QuestType.Gathering)
                    {
                        Destroy(gameObject);
                    }
                    CheckQuestIsComplete();
                }
            }
        }
    }
    */

    //攜帶這個腳本的是任務相關人和物品
    public void questTargetCheck()
    {
        for (int i = 0; i < QuestManager.instance.questList.Count; i++)
        {
            if (targetID == QuestManager.instance.questList[i].targetID)
            {
                //非打怪任務的任務目標計數
                if (QuestManager.instance.questList[i].questType == Quest.QuestType.Gathering || QuestManager.instance.questList[i].questType == Quest.QuestType.Talk)
                {
                    QuestManager.instance.questList[i].ownAmount++;
                    Debug.Log("非打怪+1");
                }

                //收集任務直接銷毀任務道具
                if (QuestManager.instance.questList[i].questType == Quest.QuestType.Gathering)
                {
                    Destroy(gameObject);
                }
                CheckQuestIsComplete();
            }
        }
    }


    //用來確認任務有沒有完成
    public void CheckQuestIsComplete()
    {
        for (int i = 0; i < QuestManager.instance.questList.Count; i++)//搜尋整個任務列表
        {
            if (targetID == QuestManager.instance.questList[i].targetID)//找需要蘋果的任務
            {
                if (QuestManager.instance.questList[i].requireAmount == QuestManager.instance.questList[i].ownAmount)
                {//確認任務是否完成
                    QuestManager.instance.questList[i].questStatus = Quest.QuestStatus.Completed;
                    PlayerQ.instance.questCompleteList.Add(questID);
                    QuestManager.instance.updateList();
                    ChatManager.Instance.SystemMessage($"<color=#3fe047>{QuestManager.instance.questList[i].questName}任務已達標</color>\n");
                    ChatManager.Instance.SystemMessage($"<color=#3fe047>可以回去找NPC完成任務拉</color>\n");
                }
                else
                {
                    int leftCount = QuestManager.instance.questList[i].requireAmount - QuestManager.instance.questList[i].ownAmount;
                    //ChatManager.Instance.SystemMessage($"<color=#F5EC3D>{QuestManager.instance.questList[i].questName}任務數量剩下  {leftCount}。</color>\n");
                    ChatManager.Instance.SystemMessage($"<color=#3fe047>{QuestManager.instance.questList[i].questName}</color>\n");
                    ChatManager.Instance.SystemMessage($"<color=#3fe047>任務目標數量還剩 {leftCount}。</color>\n");
                }
            }
        }
    }

    //當怪物被消滅後，呼叫完事件後要取消訂閱
    private void OnDestroy()
    {
        EnemyMovement.monsterQuest -= OnMonsterDestroyed;
    }
}
