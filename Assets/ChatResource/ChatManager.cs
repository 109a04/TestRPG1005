using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public CanvasGroup chatPanel; //���O
    public Transform chatContent; // �x�s�t�δ��ܪ��e��
    public GameObject MessagePrefab; // �t�δ��ܪ��w�s��
    public InputField inputField; //��r��J��
    private ScrollRect scrollRect; //�����u�ʱ�
    private float storedPosition;   //�x�s�u�ʱ���m
    private bool toggleChatPanel;   //�}�����O
    public static ChatManager Instance;

    private float displayTime = 3f; //��ܮɶ�
    private float timer;            //�p�ɾ�
    private float fadeSpeed = 2.0f; //�H�X�t��
    private bool isSendText;
    public bool isInputActive;
    

    private void Awake()
    {
        Instance = this;
        scrollRect = chatContent.GetComponentInParent<ScrollRect>();
        storedPosition = 0f; //��l��
    }



    private void Start()
    {
        // �N OnEndEdit �ƥ�P SendInput ������p�A����UEnter�AListener�N�|�sSendInput�Ǥ�r�i�h
        inputField.onEndEdit.AddListener(SendInput);
        toggleChatPanel = false;
        chatPanel.alpha = 0;
        isSendText = false;
        isInputActive = false;
    }

     //���a��J��r�C
    public void SendInput(string message) 
    {
        if (!string.IsNullOrEmpty(message))
        {
            // �x�s�u�ʱ�����m
            storedPosition = scrollRect.verticalNormalizedPosition;

            // �Ыطs�� Text ����
            GameObject systemMessage = Instantiate(MessagePrefab, chatContent);
            Text messageText = systemMessage.GetComponent<Text>();
            messageText.text = $"<color=#FFFFFF>{playerAttributeManager.Instance.pname}�G{message}</color>\n"; //�զ�

            inputField.text = ""; //�M��

            StartCoroutine("SetScrollPosition");
            isSendText = true;
        }
    }

    public void SystemMessage(string message)
    {
        toggleChatPanel = true;
        // �x�s�u�ʱ�����m
        storedPosition = scrollRect.verticalNormalizedPosition;

        // �Ыطs�� Text ����
        GameObject systemMessage = Instantiate(MessagePrefab, chatContent);

        // �]�m Text ���������e
        Text messageText = systemMessage.GetComponent<Text>();
        messageText.text = "<color=#FFFFFF>" + message + "</color>\n";

        // ���ݤ@�V��]�m�u�ʱ���m
        StartCoroutine("SetScrollPosition");
        timer = displayTime; //���m�˼ƭp��
    }

    IEnumerator SetScrollPosition()
    {
        
        yield return new WaitForEndOfFrame();
        scrollRect.verticalNormalizedPosition -= 2.5f; //���U���@�I

        // �N�u�ʱ��]�m�^�x�s����m
        scrollRect.verticalNormalizedPosition = storedPosition;
    }

    private void Update()
    {
        timer -= Time.deltaTime; //�_�h�~��˼�

        if (timer <= 0f) //��˼Ƶ����h�H�X��Ѯ�
        {
            toggleChatPanel = false;
        }

        if (Input.GetKeyUp(KeyCode.Return)) //���UEnter��
        {
            timer = 3f;
            if (isSendText) //�Y����r�n��X
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

        if (inputField.isFocused) //�p�G���a���b��J��r�A���h���ɶ���P��
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
            // �T�� InputField
            inputField.interactable = false;
            inputField.DeactivateInputField();
        }
    }

    void EnableInput()
    {
        if (inputField != null)
        {
            // �ҥ� InputField
            inputField.interactable = true;
        }
    }
}
