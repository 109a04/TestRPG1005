using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public Transform chatContent; // �x�s�t�δ��ܪ��e��
    public GameObject MessagePrefab; // �t�δ��ܪ��w�s��
    public InputField inputField; //��r��J��
    private ScrollRect scrollRect; //�����u�ʱ�
    private float storedPosition;   //�x�s�u�ʱ���m

    public static ChatManager Instance;

    

    private void Awake()
    {
        Instance = this;
        scrollRect = chatContent.GetComponentInParent<ScrollRect>();
        storedPosition = 0f; //��l��
    }

    private void Start()
    {
        // �N OnEndEdit �ƥ�P SendInput ������p�A����UEnter�AListener�N�|�sSendInput�Ǥ�r�i�h
        //inputField.onEndEdit.AddListener(SendInput);
    }

     //���a��J��r�C
    public void SendInput(string message) //�a�A���G�o�x�q�����α��o��{���X��InputField�̭��N�i�H��X��r�F�A������
    {
        if (!string.IsNullOrEmpty(message))
        {
            // �x�s�u�ʱ�����m
            storedPosition = scrollRect.verticalNormalizedPosition;

            // �Ыطs�� Text ����
            GameObject systemMessage = Instantiate(MessagePrefab, chatContent);
            Text messageText = systemMessage.GetComponent<Text>();
            messageText.text = $"<color=#FFFFFF>{playerAttributeManager.Instance.name}�G{message}</color>\n"; //�զ�

            inputField.text = ""; //�M��

            StartCoroutine("SetScrollPosition");

        }
    }

    public void SystemMessage(string message)
    {
        // �x�s�u�ʱ�����m
        storedPosition = scrollRect.verticalNormalizedPosition;

        // �Ыطs�� Text ����
        GameObject systemMessage = Instantiate(MessagePrefab, chatContent);

        // �]�m Text ���������e
        Text messageText = systemMessage.GetComponent<Text>();
        messageText.text = "<color=#FFFFFF>" + message + "</color>\n";

        // ���ݤ@�V��]�m�u�ʱ���m
        StartCoroutine("SetScrollPosition");
    }

    IEnumerator SetScrollPosition()
    {
        
        yield return new WaitForEndOfFrame();
        scrollRect.verticalNormalizedPosition -= 2.5f; //���U���@�I

        // �N�u�ʱ��]�m�^�x�s����m
        scrollRect.verticalNormalizedPosition = storedPosition;
    }
}
