using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public TextMeshProUGUI playerNickNameTM;
    //public string playerNickName;
    //public playerNameManager playerNameManager;
    //public playerAttributeManager PlayerAttributeManager;
    public static NetworkPlayer Local { get; set; }
    // ��K�h�d��֤~�O local player
    // Start is called before the first frame update

    [Networked(OnChanged = nameof(OnNickNameChanged))]
    public NetworkString<_16> nickName { get; set; }

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
            //Enable 1 audio listner
            //AudioListener audioListener = GetComponentInChildren<AudioListener>(true);
            //audioListener.enabled = true;
            //RPC_SetNickName(playerAttributeManager.Instance.pname);
            RPC_SetNickName(PlayerPrefs.GetString("PlayerNickname"));
            //Debug.Log($"�W�r�O {playerAttributeManager.Instance.pname}");
            Debug.Log($"NetworkPlayer-Spawned �W�r�O {PlayerPrefs.GetString("PlayerNickname")}");
        }
        else
        {
            Camera localCamera = GetComponentInChildren<Camera>();
            localCamera.enabled = false;

            AudioListener audioListener = GetComponentInChildren<AudioListener>();
            audioListener.enabled = false;

            Debug.Log("�ͦ��F���ݪ� player");
        }
        //Set the Player as a player object
        Runner.SetPlayerObject(Object.InputAuthority, Object);

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

    static void OnNickNameChanged(Changed<NetworkPlayer> changed)
    {
        Debug.Log($"{Time.time} OnHPChanged value {changed.Behaviour.nickName}");

        changed.Behaviour.OnNickNameChanged();
    }
    private void OnNickNameChanged()
    {
        Debug.Log($"Nick name changed for player to {nickName} for player {gameObject.name}");

        playerNickNameTM.text = nickName.ToString();
        //playerNickName.text = nickName.ToString();
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]

    public void RPC_SetNickName(string nickName, RpcInfo info = default)
    {
        Debug.Log($"[RPC] SetNickName {nickName}");
        this.nickName = nickName;
        // client send message to host and tell what name they are

    }
}
