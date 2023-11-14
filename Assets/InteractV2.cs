using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �C���i���ʪ���椬UI�G���A�����D�|���|�n�@�I
/// ����k�����b����W���@��Trigger�~��B�@�A�����Collider���覡
/// </summary>

public class InteractV2 : MonoBehaviour
{
    public string objName; //����Ψ��⪺�W�r
    public string prompt;  //���ʴ��� ex.���, �B��...

    public Text objNameText; //�W�٤�r
    public Text promptText; //���ʴ��ܤ�r
    public Image pressF;   //����F���ϥ�

    public Transform objTransform; //���󪺦�m
    private Vector3 objPos;
    private Camera mainCamera; //�D�@�ɬ۾�

    //UI���׽վ�
    private Vector3 objNameOffset = new Vector3(0f, 2f, 0f); 
    private Vector3 pressFOffset = new Vector3(0f, 80f, 0f);
    private Vector3 promptOffset = new Vector3(0f, 125f, 0f);

    public void show() //���UI
    {
        objNameText.gameObject.SetActive(true);
        promptText.gameObject.SetActive(true);
        pressF.gameObject.SetActive(true);
    }
    
    public void hide() //����UI
    {
        objNameText.gameObject.SetActive(false);
        promptText.gameObject.SetActive(false);
        pressF.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) //�I��Trigger��Ĳ�o�A�L���I�s
    {
        show();
    }

    private void OnTriggerExit(Collider other) //���}Ĳ�o�d�������
    {
        hide();
    }


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        objNameText.text = $"<color=#FFD700>{objName}</color>\n";
        promptText.text = prompt;
        hide(); //��l���A������
    }

    // Update is called once per frame
    void Update()
    {
        //����ù���m
        objPos = mainCamera.WorldToScreenPoint(objTransform.position + objNameOffset);
        objNameText.transform.position = objPos;
        pressF.transform.position = objPos + pressFOffset;
        promptText.transform.position = objPos + promptOffset;
    }
}
