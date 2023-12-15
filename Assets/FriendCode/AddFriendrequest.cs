using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class AddFriendrequest : MonoBehaviour
{
    //輸入接收玩家的玩家id
    public InputField receiverInput;
    public Text resultText;

    public void SendFriendRequest()
    {
        string sender = playerNameManager.Instance.playerName;
        //string sender = "D";
        string receiver = receiverInput.text;

        StartCoroutine(AddFriendRequest(sender, receiver));
        
    }

    IEnumerator AddFriendRequest(string sender, string receiver)
    {
        WWWForm form = new WWWForm();
        form.AddField("sender", sender);
        form.AddField("receiver", receiver);

        using (UnityWebRequest www = UnityWebRequest.Post("http://140.136.151.69/friend/friendRequest.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log(www.error);
                resultText.text = "請求失敗";
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                resultText.text = www.downloadHandler.text;
            }
        }
        receiverInput.text = null;
    }
}
