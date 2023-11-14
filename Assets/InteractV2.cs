using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �C���i���ʪ���椬UI�G���A�����D�|���|�n�@�I
/// �bAssets���������Ӧ��@��"InteractPrefab"���F��A�⥦�Ԩ�Q�n���ʪ�����W�N�i�H�F
/// ����k�����b����W���@��Trigger�~��B�@�A�����Collider���覡
/// </summary>



public class InteractV2 : MonoBehaviour
{
    public string objName; //����Ψ��⪺�W�r
    public string prompt;  //���ʴ��� ex.���, �B��...

    private Text objNameText; //�W�٤�r
    private Text promptText; //���ʴ��ܤ�r
    private Image pressF;   //����F���ϥ�

    private Transform objTransform; //���󪺦�m
    
    protected Camera mainCamera; //�D�@�ɬ۾�
    protected Vector3 objPos;       //����@�ɮy��

    //UI���׽վ�
    protected Vector3 objNameOffset = new Vector3(0f, 2f, 0f);
    protected Vector3 pressFOffset = new Vector3(0f, 80f, 0f);
    protected Vector3 promptOffset = new Vector3(0f, 125f, 0f);

    protected bool isInRange;


    // Start is called before the first frame update
    public virtual void Start()
    {
        mainCamera = Camera.main;
        objTransform = GetComponent<Transform>();
        objNameText = this.transform.Find("InteractPrefab/ObjName").GetComponent<Text>();
        promptText = this.transform.Find("InteractPrefab/Prompt").GetComponent<Text>();
        pressF = this.transform.Find("InteractPrefab/PressF").GetComponent<Image>();
        objNameText.text = $"<color=#FFD700>{objName}</color>\n";   
        promptText.text = prompt;
        isInRange = false;
        hide(); //��l���A������
    }

    // Update is called once per frame
    public virtual void Update()
    {
        //����ù���m
        objPos = mainCamera.WorldToScreenPoint(objTransform.position + objNameOffset);
        objNameText.transform.position = objPos;
        pressF.transform.position = objPos + pressFOffset;
        promptText.transform.position = objPos + promptOffset;


    }


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
        isInRange = true;
    }

    private void OnTriggerExit(Collider other) //���}Ĳ�o�d�������
    {
        hide();
        isInRange = false;
    }



    
}
