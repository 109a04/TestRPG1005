using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public string objName; //����W��
    public string prompt; //���ʴ���
    public Transform interactTransform; //���ʦ�m�A��Q�n���ʪ�����Ԩ�o��
    public Text nameText; //��r�Զi��
    public Text hintText; //���ܤ�r
    public Image pressF;  //���s�Ϥ�
    protected Vector3 offset = new Vector3(0f, 2f, 0f); // UI�����b�Y���W��������
    protected Vector3 moreHigh = new Vector3(0f, 80f, 0f); //��
    protected Vector3 moreMoreHigh = new Vector3(0f, 125f, 0f); //���
    protected Camera mainCamera;


    protected bool isInRange;
    protected bool isTriggered;

    // Start is called before the first frame update
    public virtual void Start()
    {
        mainCamera = Camera.main;
        isTriggered = false; //��l��
        isInRange = false;
        nameText.gameObject.SetActive(false); //���åH�UUI����
        hintText.gameObject.SetActive(false);
        pressF.gameObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            nameText.gameObject.SetActive(true);
            hintText.gameObject.SetActive(true);
            pressF.gameObject.SetActive(true);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            nameText.gameObject.SetActive(false);
            hintText.gameObject.SetActive(false);
            pressF.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    public virtual void Update()
    {
        // �N���󪺥@�ɪŶ���m�ഫ���̹��Ŷ���m
        Vector3 npcScrPos = mainCamera.WorldToScreenPoint(interactTransform.position + offset);
        nameText.text = $"<color=#FFD700>{objName}</color>\n";
        hintText.text = prompt;

        // �NUI��������m�]�m���ù��Ŷ���m
        nameText.transform.position = npcScrPos;
        pressF.transform.position = npcScrPos + moreHigh;
        hintText.transform.position = npcScrPos + moreMoreHigh;

        //�P�_�O�_�}�l����
        if (isInRange == true && Input.GetKeyDown(KeyCode.F))
        {
            isTriggered = !isTriggered;
        }
    }
}
