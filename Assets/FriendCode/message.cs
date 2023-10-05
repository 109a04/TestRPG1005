using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

[Serializable]
public class friendMessage
{
    public string friendName;
    public string message;
}

public class message : MonoBehaviour
{
    public string receiver;
    public List<Text> requestName;//用來顯示誰寄邀請給你
    public List<Text> requestText;//用來顯示誰寄邀請給你
    public GameObject panel;

    public void showMessage()
    {
        StartCoroutine(receiveMessage());
    }

    public void ClearPreviousContent()
    {

        //清除舊顯示
        foreach (Text requestN in requestName)
        {
            requestN.text = "";
        }

        //清除舊顯示
        foreach (Text request in requestText)
        {
            request.text = "";
        }

        panel.SetActive(false);
    }

    IEnumerator receiveMessage()
    {
        panel.SetActive(true);


        //先將玩家名字POST到php
        WWWForm form = new WWWForm();
        form.AddField("receiver", receiver);

        //POST到php後，從php求取玩家好友
        using UnityWebRequest www = UnityWebRequest.Post("http://140.136.151.69/friend/receiveMessage.php", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //解析收到的json格式名字陣列
            string jsonData = www.downloadHandler.text;
            if (jsonData == "信箱為空")
            {
                Debug.Log(jsonData);
            }
            else
            {
                jsonData = "{\"Items\":" + jsonData + "}";//把json格式先修復到可以用JsonHelper的格式
                friendMessage[] requestYes = JsonHelper.FromJson<friendMessage>(jsonData);

                //把寄好友邀請的玩家名字存到invitationData陣列
                for (int i = 0; i < requestYes.Length; i++)
                {
                    string fName = requestYes[i].friendName;
                    string messT = requestYes[i].message;
                    Text requestN = requestName[i];
                    Text request = requestText[i];



                    //哪個玩家寄的邀請
                    requestN.text = "寄信玩家: " + fName;
                    request.text = messT;
                }
            }
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
