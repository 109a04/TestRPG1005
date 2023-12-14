using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public static NetworkPlayer Local { get; set; }
    // 方便去查找誰才是 local player
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Spawned()
    {
        // 在生成之後跑這段
        if (Object.HasInputAuthority)
        {
            // 只有有權限的才是 local player
            // 如果不檢查的話就會像之前那樣在其中一個視窗移動，所有的玩家都會被移動
            Local = this;
            Debug.Log("生成了 local player");

        }
        else
        {
            Camera localCamera = GetComponentInChildren<Camera>();
            localCamera.enabled = false;

            AudioListener audioListener = GetComponentInChildren<AudioListener>();
            audioListener.enabled = false;

            Debug.Log("生成了遠端的 player");
        }

        // 給不同的玩家改名
        transform.name = $"Player_{Object.Id}";
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (player == Object.InputAuthority)
        {
            Runner.Despawn(Object);
        }
            
    }


}
