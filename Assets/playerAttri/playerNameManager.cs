using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerNameManager : MonoBehaviour
{
    public static playerNameManager Instance { get; private set; }

    //要保存的玩家名字
    public string playerName { get; set; }
    public string playerID { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}