using System.Collections.Generic;
using UnityEngine;

//这个脚本添加在Player游戏对象上
public class PlayerQ : MonoBehaviour
{
    public static PlayerQ instance;

    public List<int> questCompleteList = new List<int>();//玩家的已完成任務列表

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

}
