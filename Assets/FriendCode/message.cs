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
    public List<Text> requestName;//�Ψ���ֱܽH�ܽе��A
    public List<Text> requestText;//�Ψ���ֱܽH�ܽе��A
    public GameObject panel;

    public void showMessage()
    {
        StartCoroutine(receiveMessage());
    }

    public void ClearPreviousContent()
    {

        //�M�������
        foreach (Text requestN in requestName)
        {
            requestN.text = "";
        }

        //�M�������
        foreach (Text request in requestText)
        {
            request.text = "";
        }

        panel.SetActive(false);
    }

    IEnumerator receiveMessage()
    {
        panel.SetActive(true);


        //���N���a�W�rPOST��php
        WWWForm form = new WWWForm();
        form.AddField("receiver", receiver);

        //POST��php��A�qphp�D�����a�n��
        using UnityWebRequest www = UnityWebRequest.Post("http://140.136.151.69/friend/receiveMessage.php", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //�ѪR���쪺json�榡�W�r�}�C
            string jsonData = www.downloadHandler.text;
            if (jsonData == "�H�c����")
            {
                Debug.Log(jsonData);
            }
            else
            {
                jsonData = "{\"Items\":" + jsonData + "}";//��json�榡���״_��i�H��JsonHelper���榡
                friendMessage[] requestYes = JsonHelper.FromJson<friendMessage>(jsonData);

                //��H�n���ܽЪ����a�W�r�s��invitationData�}�C
                for (int i = 0; i < requestYes.Length; i++)
                {
                    string fName = requestYes[i].friendName;
                    string messT = requestYes[i].message;
                    Text requestN = requestName[i];
                    Text request = requestText[i];



                    //���Ӫ��a�H���ܽ�
                    requestN.text = "�H�H���a: " + fName;
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
