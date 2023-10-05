using UnityEngine;
using UnityEngine.UI;

public class playerData : MonoBehaviour
{
    public Text playerNameText;//顯示我們要的跨scene東東
    public Text playerIDText;//顯示我們要的跨scene東東

    //playerNameManager.Instance就是一個含有playerName和playerID的物件
    //playerNameManager.Instance.playerName和playerNameManager.Instance.playerID的type都是string
    private void Start()
    {
        if(playerNameManager.Instance != null)//切換後物件存在可繼續做
        {
            playerNameText.text = playerNameManager.Instance.playerName;
            playerIDText.text = playerNameManager.Instance.playerID;
        }
        else
        {
            Debug.Log("no playerNameManager.Instance");//切換後物件不存在則回報
        }
        
    }
}







