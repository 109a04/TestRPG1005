using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyCamera : MonoBehaviour
{
    // Start is called before the first frame update
    // �ʺA���ͦ��@�Ӭ۾��A�]���������� main camera �O�j�a�@�Ϊ�
    // �s�u���ɭԤj�a������i�J��P�@�� scene �̭��A�o�˪��� camera �|�X�{�v�������A
    // �|�ɭP�u���@�ӳ������e���A��L�������e���O�ťժ�
    // ���S���T�w�|���h�֭Ӫ��a�X�{�b�����̭��A�ҥH�n�ʺA���ͦ��۾�

    public GameObject copyCamera;//�n�Q�ƻs������
    public GameObject superGameObject;//�n�Q��m�b���Ӫ��󩳤U

    private GameObject childGameObject;//�Q�ƻs�X�Ӫ�����
    void Start()
    {
        //copyGameObject = GameObject.Find("MainCamera");
        childGameObject = Instantiate(copyCamera);//�ƻscopyGameObject����(�s�P�Ӫ��󨭤W���}���@�_�ƻs)
        childGameObject.transform.parent = superGameObject.transform;//���superGameObject����
        childGameObject.transform.localPosition = new Vector3(3, 3, -3);//�ƻs�X�Ӫ������m���y�Ь�superGameObject���󤺪����I
        
        //childGameObject.AddComponent<NullScript>(); //�ʺA�W�[�W��"NullScript"���}���즹���󨭤W
        //�U���o�@�檺�\�ର�N�ƻs�X�Ӫ��l����R�W��CopyObject
        childGameObject.name = "MainCamera"; 
    }
}
