using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public enum QuestType { Gathering, Talk, Reach };
    public enum QuestStatus { Waitting, Accepted, Completed };

    public string questName;
    public int questID;//用來增加到玩家的已完成任務表
    public QuestType questType;
    public QuestStatus questStatus;

    public int expReward;
    public int goldReward;

    [Header("任務完成條件")]
    public int requireAmount;//需要幾個
    public int ownAmount;//現在有幾個
    public int targetID;//甚麼東西
}
