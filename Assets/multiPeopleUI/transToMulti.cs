using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transToMulti : Interactable
{
    // Start is called before the first frame update

    public Vector3 oriPlace;
    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (isTriggered == true)
        {
            GameObject cat = GameObject.Find("Cat");
            cat.GetComponentInChildren<Camera>().enabled = false; 
            // ����o�Ӭ۾������A���M�|����ӥd�b�@�_ 
            SceneManager.LoadSceneAsync("teamUI", LoadSceneMode.Additive);
            // �b�������@�� scene �����p�U�}�t�@�� scene
            isTriggered = !isTriggered; // �o�̤��[�o�Ӫ��ܡA�|�@�����_���s�W�@�� teamUI
            Debug.Log("are you all right?");

            //oriPlace = cat.GetComponent<Transform>().position;
            //Debug.Log("�{�b����m�O " + oriPlace);
            //SceneManager.UnloadSceneAsync("SampleScene");
            //SceneManager.LoadScene("teamUI");
            // �쥻�Q�Ϊ�����������k�A���O�o�˦^�� sample ���ɭԿ��`�O���U�h�����A��ӥγo�Ӥ�k�N���|�F
            // ���Mı�o�᭱�����h�H�A���^���٬O�|���o�ӺL�������p()���ޤF������J��F�A��
        }
    }
}
