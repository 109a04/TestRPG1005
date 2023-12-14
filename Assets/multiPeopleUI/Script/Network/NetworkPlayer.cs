using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public static NetworkPlayer Local { get; set; }
    // ��K�h�d��֤~�O local player
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Spawned()
    {
        // �b�ͦ�����]�o�q
        if (Object.HasInputAuthority)
        {
            // �u�����v�����~�O local player
            // �p�G���ˬd���ܴN�|�����e���˦b�䤤�@�ӵ������ʡA�Ҧ������a���|�Q����
            Local = this;
            Debug.Log("�ͦ��F local player");

        }
        else
        {
            Camera localCamera = GetComponentInChildren<Camera>();
            localCamera.enabled = false;

            AudioListener audioListener = GetComponentInChildren<AudioListener>();
            audioListener.enabled = false;

            Debug.Log("�ͦ��F���ݪ� player");
        }

        // �����P�����a��W
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
