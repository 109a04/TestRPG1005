using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public CanvasGroup chatPanel; //面板
    public Transform chatContent; // 儲存系統提示的容器
    public GameObject MessagePrefab; // 系統提示的預製件
    public InputField inputField; //文字輸入框
    private ScrollRect scrollRect; //垂直滾動條
    private float storedPosition;   //儲存滾動條位置
    private bool toggleChatPanel;   //開關面板
    public static ChatManager Instance;

    private float displayTime = 3f; //顯示時間
    private float timer;            //計時器
    private float fadeSpeed = 2.0f; //淡出速度
    private bool isSendText;
    public bool isInputActive;
    

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
        toggleChatPanel = false;
        chatPanel.alpha = 0;
        isSendText = false;
        isInputActive = false;
    }

     //玩家輸入文字。
    public void SendInput(string message) 
    {
        if (!string.IsNullOrEmpty(message))
        {
            // 儲存滾動條的位置
            storedPosition = scrollRect.verticalNormalizedPosition;

            // 創建新的 Text 元素
            GameObject systemMessage = Instantiate(MessagePrefab, chatContent);
            Text messageText = systemMessage.GetComponent<Text>();
            messageText.text = $"<color=#FFFFFF>{playerAttributeManager.Instance.pname}：{message}</color>\n"; //白色

            inputField.text = ""; //清空

            StartCoroutine("SetScrollPosition");
            isSendText = true;
        }
    }

    public void SystemMessage(string message)
    {
        toggleChatPanel = true;
        // 儲存滾動條的位置
        storedPosition = scrollRect.verticalNormalizedPosition;

        // 創建新的 Text 元素
        GameObject systemMessage = Instantiate(MessagePrefab, chatContent);

        // 設置 Text 元素的內容
        Text messageText = systemMessage.GetComponent<Text>();
        messageText.text = "<color=#FFFFFF>" + message + "</color>\n";

        // 等待一幀後設置滾動條位置
        StartCoroutine("SetScrollPosition");
        timer = displayTime; //重置倒數計時
    }

    IEnumerator SetScrollPosition()
    {
        
        yield return new WaitForEndOfFrame();
        scrollRect.verticalNormalizedPosition -= 2.5f; //往下挪一點

        // 將滾動條設置回儲存的位置
        scrollRect.verticalNormalizedPosition = storedPosition;
    }

    private void Update()
    {
        timer -= Time.deltaTime; //否則繼續倒數

        if (timer <= 0f) //當倒數結束則淡出聊天框
        {
            toggleChatPanel = false;
        }

        if (Input.GetKeyUp(KeyCode.Return)) //按下Enter時
        {
            timer = 3f;
            if (isSendText) //若有文字要輸出
            {
                Invoke(nameof(ClearText), 0.15f);
            }
            else
            {
                toggleChatPanel = !toggleChatPanel;
                if (toggleChatPanel)
                {
                    inputField.ActivateInputField();
                }
            }
        }

        if (toggleChatPanel == true)
        {
            chatPanel.alpha = 1;
            EnableInput();
        }
        else
        {
            FadeOutTable();
        }

        if (chatPanel.alpha == 0)
        {
            DisableInput();
        }

        if (inputField.isFocused) //如果玩家正在輸入文字，把減去的時間抵銷掉
        {
            timer += Time.deltaTime;
        }
    }



    void ClearText()
    {
        isSendText = false;
    }

    public void FadeOutTable()
    {
        chatPanel.alpha =  Mathf.Lerp(chatPanel.alpha, 0f, fadeSpeed * Time.deltaTime);
    }

    void DisableInput()
    {
        if (inputField != null)
        {
            // 禁用 InputField
            inputField.interactable = false;
            inputField.DeactivateInputField();
        }
    }

    void EnableInput()
    {
        if (inputField != null)
        {
            // 啟用 InputField
            inputField.interactable = true;
        }
    }
}
