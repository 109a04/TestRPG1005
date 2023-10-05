using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public enum QuestType { Gathering, Talk, Reach };
    public enum QuestStatus { Waitting, Accepted, Completed };

    public string questName;
    public int questID;//�ΨӼW�[�쪱�a���w�������Ȫ�
    public QuestType questType;
    public QuestStatus questStatus;

    public int expReward;
    public int goldReward;

    [Header("���ȧ�������")]
    public int requireAmount;//�ݭn�X��
    public int ownAmount;//�{�b���X��
    public int targetID;//�ƻ�F��
}
