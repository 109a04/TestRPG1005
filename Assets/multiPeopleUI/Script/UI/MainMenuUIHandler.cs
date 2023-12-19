using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUIHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OnFindGameClicked();
    }

    public void OnFindGameClicked()
    {
        NetworkRunnerHandler networkRunnerHandler = FindObjectOfType<NetworkRunnerHandler>();

        networkRunnerHandler.OnJoinLobby();
        FindObjectOfType<SessionListUIHandler>(true).OnLookingForGameSessions();
    }

    public void OnStartNewSessionClicked()
    {
        NetworkRunnerHandler networkRunnerHandler = FindObjectOfType<NetworkRunnerHandler>();
        if(networkRunnerHandler == null)
        {
            Debug.Log("NetworkRunnerHandler ¦b·d");
        }
        string CostomSessionName = playerAttributeManager.Instance.pname;
        Debug.Log($"£«¦W¦r¬O{playerAttributeManager.Instance.pname}");
        networkRunnerHandler.CreateGame(CostomSessionName, "multiPeople");

    }
}
