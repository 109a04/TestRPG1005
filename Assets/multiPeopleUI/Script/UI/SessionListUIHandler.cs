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
    MainMenuUIHandler mainMenuUIHandler;

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
        Debug.Log("刪掉了 verticalLayoutGroup 的子物件");
        // 隱藏 statusText
        statusText.gameObject.SetActive(false);
    }

    public void AddToList(SessionInfo sessionInfo)
    {
        //Add a new item to the list
        SessionInfoUIItem addedSessionInfoListUIItem = Instantiate(sessionItemListPrefab, verticalLayoutGroup.transform).GetComponent<SessionInfoUIItem>();
        Debug.Log("新增了一個物體到房間列表裡面了");
        addedSessionInfoListUIItem.SetInformation(sessionInfo);

        //Hook up events
        addedSessionInfoListUIItem.OnJoinSession += AddedSessionInfoListUIItem_OnJoinSession;

    }

    private void AddedSessionInfoListUIItem_OnJoinSession(SessionInfo sessionInfo)
    {
        NetworkRunnerHandler networkRunnerHandler = FindObjectOfType<NetworkRunnerHandler>();

        networkRunnerHandler.JoinGame(sessionInfo);
    }

    public void OnNoSessionsFound()
    {
        ClearList();

        statusText.text = "目前沒有房間";
        Debug.Log("目前沒有房間");
        statusText.gameObject.SetActive(true);
    }

    public void OnLookingForGameSessions()
    {
        ClearList();

        statusText.text = "正在搜尋房間...";
        Debug.Log("正在搜尋房間...");
        statusText.gameObject.SetActive(true);
    }

}
