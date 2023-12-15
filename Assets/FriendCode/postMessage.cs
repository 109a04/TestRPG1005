using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI物件需要增加此行
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using System;
using UnityEngine.SceneManagement;


public class postMessage : MonoBehaviour
{
    public InputField receiverName;//收件人
    public InputField message;//信件內容
    public string senderName;//玩家為寄送人
    public Text sendText;//寄送提示

    //public GameObject panel;

    [Serializable]//玩家寄信內容
    public class messager
    {
        public string sendName;
        public string name;
        public string messT;
    }

    public messager sendMessage;

    public void Post()
    {
        //panel.SetActive(true);
        string reName = receiverName.text;
        string mess = message.text;

        sendMessage.name = reName;
        sendMessage.messT = mess;
        sendMessage.sendName = playerAttributeManager.Instance.pname;

        // 將物件轉換為JSON字符串
        string jsonPost = JsonUtility.ToJson(sendMessage);

        if (jsonPost != null && sendMessage.messT != "")
        {
            // 發送POST請求
            StartCoroutine(SendMessageT(jsonPost));
            
        }
        //panel.SetActive(false);
    }

    IEnumerator SendMessageT(string jsonData)
    {
        // 建立一個表單
        WWWForm form = new WWWForm();
        form.AddField("json", jsonData);

        using (UnityWebRequest www = UnityWebRequest.Post("http://140.136.151.69/friend/postMessage.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                sendText.text = responseText;
            }
        }
        receiverName.text = null;
        message.text = null;
    }
}