using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public enum QuestType { Gathering, Talk, Monster };
    public enum QuestStatus { Waitting, Accepted, Completed };

    public string questName;
    public int questID;//�ΨӼW�[�쪱�a�����ȦC��
    public QuestType questType;
    public QuestStatus questStatus;

    public int expReward;
    public int goldReward;

    [Header("���ȧ�������")]
    public int requireAmount;//�ݭn�X��
    public int ownAmount;//�{�b���X��
    public int targetID;//���ӥ��ȥؼЪ��~
}
