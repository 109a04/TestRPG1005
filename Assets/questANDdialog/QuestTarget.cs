using UnityEngine;

//放在和任務有關的目標物品上
public class QuestTarget : MonoBehaviour
{
    public int questID;
    public int targetID;
    public Quest quest;

    public enum QuestType { Gathering, Talk, Monster };
    public QuestType questType;//收集類的任務，收集完後會消失

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
                    //QuestManager.instance.questList[i].ownAmount++;
                    //非打怪任務直接加
                    if (QuestManager.instance.questList[i].questType == Quest.QuestType.Gathering || QuestManager.instance.questList[i].questType == Quest.QuestType.Talk)
                    {
                        QuestManager.instance.questList[i].ownAmount++;
                    }

                    //打怪任務則怪物消滅後才加
                    if (QuestManager.instance.questList[i].questType == Quest.QuestType.Monster)
                    {
                        Debug.Log("碰到任務怪物拉");
                    }

                    //打怪任務則怪物消滅後才加
                    if (QuestManager.instance.questList[i].questType == Quest.QuestType.Monster && gameObject == null)
                    {
                        QuestManager.instance.questList[i].ownAmount++;
                        Debug.Log("解決任務怪物拉");
                    }

                    if (QuestManager.instance.questList[i].questType == Quest.QuestType.Gathering)
                    {
                        Destroy(gameObject);
                    }
                    
                    CheckQuestIsComplete();
                }
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
                }
            }
        }
    }
}
