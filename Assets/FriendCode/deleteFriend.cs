using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class deleteFriend : MonoBehaviour
{
    public InputField friend;
    public Text request;
    public string sender;
    public Text resultText;

    public void DeleteFriend()
    {
        string friendName = friend.text;
        StartCoroutine(SendDeleteRequest(friendName));
    }

    IEnumerator SendDeleteRequest(string friendName)
    {
        sender = playerNameManager.Instance.playerName;
        WWWForm form = new WWWForm();
        form.AddField("sender", sender);
        form.AddField("friendName", friendName);

        //發送刪除好友
        using UnityWebRequest www = UnityWebRequest.Post("http://140.136.151.69/friend/deleteFriend.php", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("刪除好友失敗 " + www.error);
            resultText.text = "請求失敗";
        }
        else
        {
            string response = www.downloadHandler.text;
            Debug.Log(response);
            resultText.text = www.downloadHandler.text;
        }

        friend.text = null;
    }



}
