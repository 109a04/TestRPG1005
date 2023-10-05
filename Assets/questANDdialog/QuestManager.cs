using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public List<Quest> questList = new List<Quest>();//玩家的任務列表

    public Text[] quest = new Text[5];
    public Text[] status = new Text[5];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void updateList()
    {
        //移除狀態為complete的任務
        questList.RemoveAll(quest => quest.questStatus == Quest.QuestStatus.Completed);

        //只顯示accepted的任務
        for (int i = 0; i < quest.Length; i++)
        {
            if (i < questList.Count)
            {
                quest[i].text = questList[i].questName;
                status[i].text = questList[i].questStatus.ToString();
            }
            else
            {
                //沒任務則清空文本顯示
                quest[i].text = "";
                status[i].text = "";
            }
        }
    }
}
