using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public Transform chatContent; // 儲存系統提示的容器
    public GameObject MessagePrefab; // 系統提示的預製件
    public InputField inputField; //文字輸入框
    private ScrollRect scrollRect; //垂直滾動條
    private float storedPosition;   //儲存滾動條位置

    public static ChatManager Instance;

    

    private void Awake()
    {
        Instance = this;
        scrollRect = chatContent.GetComponentInParent<ScrollRect>();
        storedPosition = 0f; //初始化
    }

    private void Start()
    {
        // 將 OnEndEdit 事件與 SendInput 函數關聯，當按下Enter，Listener就會叫SendInput傳文字進去
        inputField.onEndEdit.AddListener(SendInput);
    }

     //玩家輸入文字。
    public void SendInput(string message) //靠，結果這台電腦不用掛這串程式碼到InputField裡面就可以輸出文字了，為什麼
    {
        if (!string.IsNullOrEmpty(message))
        {
            // 儲存滾動條的位置
            storedPosition = scrollRect.verticalNormalizedPosition;

            // 創建新的 Text 元素
            GameObject systemMessage = Instantiate(MessagePrefab, chatContent);
            Text messageText = systemMessage.GetComponent<Text>();
            messageText.text = $"<color=#FFFFFF>{Player.Instance.playerName}：{message}</color>\n"; //白色

            inputField.text = ""; //清空

            StartCoroutine("SetScrollPosition");

        }
    }

    public void SystemMessage(string message)
    {
        // 儲存滾動條的位置
        storedPosition = scrollRect.verticalNormalizedPosition;

        // 創建新的 Text 元素
        GameObject systemMessage = Instantiate(MessagePrefab, chatContent);

        // 設置 Text 元素的內容
        Text messageText = systemMessage.GetComponent<Text>();
        messageText.text = "<color=#FFFFFF>" + message + "</color>\n";

        // 等待一幀後設置滾動條位置
        StartCoroutine("SetScrollPosition");
    }

    IEnumerator SetScrollPosition()
    {
        
        yield return new WaitForEndOfFrame();
        scrollRect.verticalNormalizedPosition -= 2.5f; //往下挪一點

        // 將滾動條設置回儲存的位置
        scrollRect.verticalNormalizedPosition = storedPosition;
    }
}
