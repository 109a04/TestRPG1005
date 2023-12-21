using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;
using TMPro;

public class SessionListUIHandler : MonoBehaviour
{
    public TextMeshProUGUI statusText;
    public GameObject sessionItemListPrefab;
    public VerticalLayoutGroup verticalLayoutGroup;

    private void Awake()
    {
        ClearList();
        //mainMenuUIHandler.OnFindGameClicked();
    }

    public void ClearList()
    {
        // Delete all children of the vertical layout group
        foreach(Transform child in verticalLayoutGroup.transform)
        {
            Destroy(child.gameObject);
        }
        Debug.Log("�R���F verticalLayoutGroup ���l����");
        // ���� statusText
        statusText.gameObject.SetActive(false);
    }

    public void AddToList(SessionInfo sessionInfo)
    {
        //Add a new item to the list
        SessionInfoUIItem addedSessionInfoListUIItem = Instantiate(sessionItemListPrefab, verticalLayoutGroup.transform).GetComponent<SessionInfoUIItem>();
        Debug.Log("�s�W�F�@�Ӫ����ж��C��̭��F");
        addedSessionInfoListUIItem.SetInformation(sessionInfo);

        //Hook up events
        // �s�u�ƥ�
        addedSessionInfoListUIItem.OnJoinSession += AddedSessionInfoListUIItem_OnJoinSession;

    }

    private void AddedSessionInfoListUIItem_OnJoinSession(SessionInfo sessionInfo)
    {
        NetworkRunnerHandler networkRunnerHandler = FindObjectOfType<NetworkRunnerHandler>();
        if(networkRunnerHandler == null)
        {
            Debug.Log("SessionListUIHandler �̭����[�J�ƥ�b�d");
        }
        Debug.Log($"SessionListUIHandler �̭������s�� sessionInfo �O {sessionInfo}");
        networkRunnerHandler.JoinGame(sessionInfo);
        MainMenuUIHandler mainMenuUIHandler = FindObjectOfType<MainMenuUIHandler>();
        mainMenuUIHandler.OnJoiningServer();
    }

    public void OnNoSessionsFound()
    {
        ClearList();

        statusText.text = "�ثe�S���ж�";
        Debug.Log("�ثe�S���ж�");
        statusText.gameObject.SetActive(true);
    }

    public void OnLookingForGameSessions()
    {
        ClearList();

        statusText.text = "���b�j�M�ж�...";
        Debug.Log("���b�j�M�ж�...");
        statusText.gameObject.SetActive(true);
    }

}
