using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;


//用來解析從全好友提示表得到誰send好友邀請
/*
[Serializable]
public class requestShow
{
    public string senderName;
}
*/


public class DisplayFriendInvitations : MonoBehaviour
{
    public string receiverPlayer;//存放playerNameManager.Instance.playerName的變數

    public List<Button> acceptButtons;//同意按鈕
    public List<Button> rejectButtons;//拒絕按鈕
    public List<Text> invitationTextElements;//用來顯示誰寄邀請給你
    public GameObject panel;

    public void showRequest()
    {
        panel.SetActive(true);
        StartCoroutine(GetFriendInvitations());
    }


    public void ClearPreviousContent()
    {
        //清除舊顯示和舊事件
        foreach (Text invitationText in invitationTextElements)
        {
            invitationText.text = "";
        }

        foreach (Button acceptButton in acceptButtons)
        {
            acceptButton.interactable = true;
            acceptButton.onClick.RemoveAllListeners();
        }

        foreach (Button rejectButton in rejectButtons)
        {
            rejectButton.interactable = true;
            rejectButton.onClick.RemoveAllListeners();
        }

        panel.SetActive(false);

    }

    IEnumerator GetFriendInvitations()
    {
        //先將玩家名字POST到php
        WWWForm form = new WWWForm();
        form.AddField("receiverPlayer", receiverPlayer);

        //POST到php後，從php求取全玩家好友提示表裡寄交友邀請的資料
        using UnityWebRequest www = UnityWebRequest.Post("http://140.136.151.69/friend/showRequest.php", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //解析收到的json格式名字陣列
            string jsonData = www.downloadHandler.text;
            if (jsonData == "沒有人寄好友邀請")
            {
                Debug.Log(jsonData);
            }
            else
            {
                jsonData = "{\"Items\":" + jsonData + "}";//把json格式先修復到可以用JsonHelper的格式
                string[] requestAll = JsonHelper.FromJson<string>(jsonData);

                //把寄好友邀請的玩家名字存到invitationData陣列
                for (int i = 0; i < requestAll.Length; i++)
                {
                    Debug.Log(requestAll[i]);
                    string senderName = requestAll[i];
                    Text invitationText = invitationTextElements[i];

                    //哪個玩家寄的邀請
                    invitationText.text = "玩家 " + senderName + " 寄好友邀请给你\n";

                    //創造同意和拒絕按鈕
                    Button acceptButton = acceptButtons[i];
                    Button rejectButton = rejectButtons[i];

                    //給同意按鈕添加事件
                    acceptButton.onClick.RemoveAllListeners(); // 移除之前的监听器
                    acceptButton.onClick.AddListener(() => AcceptFriendInvitation(senderName, acceptButton, rejectButton, i));

                    //給創造按鈕添加事件
                    rejectButton.onClick.RemoveAllListeners(); // 移除之前的监听器
                    rejectButton.onClick.AddListener(() => RejectFriendInvitation(senderName, acceptButton, rejectButton, i));
                }
            }
        }
    }

    //同意好友邀請的function
    public void AcceptFriendInvitation(string senderName, Button acceptButton, Button rejectButton, int i)
    {
        StartCoroutine(SendAcceptanceToServer(senderName));
        //按同意後不能按拒絕了
        rejectButton.interactable = false;

    }

    //拒絕好友邀請的function
    public void RejectFriendInvitation(string senderName, Button acceptButton, Button rejectButton, int i)
    {

        StartCoroutine(SendRejectionToServer(senderName));
        //按同意後不能按拒絕了
        acceptButton.interactable = false;

        }

    //把同意好友邀請資料POST到好友邀請提示資料庫
    IEnumerator SendAcceptanceToServer(string senderName)
    {
        //POST玩家和寄邀請者的名字
        WWWForm form = new WWWForm();
        form.AddField("senderName", receiverPlayer);//原本被寄邀請的
        form.AddField("receiverName", senderName);//寄邀請的變成接收同意邀請的人
        form.AddField("action", "accept"); //叫服務器執行操作

        //發送POST
        using UnityWebRequest www = UnityWebRequest.Post("http://140.136.151.69/friend/requestYorN.php", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("傳送同意邀請時出錯：" + www.error);
        }
        else
        {
            Debug.Log("已成功傳送同意邀請。");
            form.AddField("senderName", receiverPlayer);//原本被寄邀請的
            form.AddField("receiverName", senderName);//寄邀請的變成接收同意邀請的人
            using UnityWebRequest wwwAddFriend = UnityWebRequest.Post("http://140.136.151.69/friend/addFriendlist.php", form);
            yield return wwwAddFriend.SendWebRequest();

            if (wwwAddFriend.result == UnityWebRequest.Result.ConnectionError || wwwAddFriend.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("傳送添加好友時出錯：" + wwwAddFriend.error);
            }
            else
            {
                Debug.Log(wwwAddFriend.downloadHandler.text);
            }
        }
    }

    //拒絕好友邀請要執行的function
    IEnumerator SendRejectionToServer(string senderName)
    {
        WWWForm form = new WWWForm();
        form.AddField("senderName", receiverPlayer);
        form.AddField("receiverName", senderName);
        form.AddField("action", "reject");

        using UnityWebRequest www = UnityWebRequest.Post("http://140.136.151.69/friend/requestYorN.php", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("傳送拒絕邀請時出錯：" + www.error);
        }
        else
        {
            Debug.Log("已成功傳送拒絕邀請。");
        }
    }

    
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}
