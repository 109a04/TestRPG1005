using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ReturnToMyGame : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickCallBack);
        // �o�ӬO�٨S������h�H�Ҧ��A�b�h�H�ն����e�����^�ӥΪ����ӫ��s
    }

    public void OnClickCallBack()
    {
        // ���Ѫ��o�ǳ��O�A�쥻�Q�n�ǰe�^���w���a�I�A���O�n���|������������
        // �̫�o�{�F��²�檺��k�]�^�������ݭn�ǰe��m
        //Vector3 move;
        //move = new Vector3(18.0f, 10.0f, -7.5f);
        //Debug.Log("�{�b�ǹL�Ӫ���m�O " + oriPlace);
        //GameObject catNow = GameObject.Find("Cat");
        //catNow.GetComponent<Transform>().position = move; 
        //SceneManager.LoadScene("SampleScene");
        //catNow.GetComponent<Transform>().position = move;
        //Debug.Log("���ʧ��᪺��m�O " + catNow.GetComponent<Transform>().position);
        SceneManager.UnloadSceneAsync("teamUI");
        GameObject cat = GameObject.Find("Cat");
        cat.GetComponentInChildren<Camera>().enabled = true;
    }
}
