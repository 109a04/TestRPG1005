using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;


[Serializable]
public class friend
{
    public string friendName;
    public string friendID;
}


public class friendList : MonoBehaviour
{
    public string sender;
    public List<Text> requestText;//�Ψ���ֱܽH�ܽе��A
    public GameObject panel;

    public void showList()
    {
        StartCoroutine(FriendList());
    }

    public void ClearPreviousContent()
    {
        //�M�������
        foreach (Text request in requestText)
        {
            request.text = "";
        }

        panel.SetActive(false);
    }

    IEnumerator FriendList()
    {
        panel.SetActive(true);
        sender = playerNameManager.Instance.playerName;

        //���N���a�W�rPOST��php
        WWWForm form = new WWWForm();
        form.AddField("sender", sender);

        //POST��php��A�qphp�D�����a�n��
        using UnityWebRequest www = UnityWebRequest.Post("http://140.136.151.69/friend/friendAll.php", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //�ѪR���쪺json�榡�W�r�}�C
            string jsonData = www.downloadHandler.text;
            if (jsonData == "�n�ͦC����")
            {
                Debug.Log(jsonData);
            }
            else
            {
                jsonData = "{\"Items\":" + jsonData + "}";//��json�榡���״_��i�H��JsonHelper���榡
                friend[] requestYes = JsonHelper.FromJson<friend>(jsonData);

                //��H�n���ܽЪ����a�W�r�s��invitationData�}�C
                for (int i = 0; i < requestYes.Length; i++)
                {
                    string fName = requestYes[i].friendName;
                    string fID = requestYes[i].friendID;
                    Text request = requestText[i];



                    //���Ӫ��a�H���ܽ�
                    request.text = "���a " + fName+" ID�� "+fID;
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
