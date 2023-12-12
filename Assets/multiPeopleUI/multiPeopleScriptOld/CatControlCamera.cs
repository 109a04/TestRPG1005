using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControlCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera mainCamera;
    public float rotationSpeed = 3f; // ��������t��
    public float cameraHeight = 5f; // �۾�����

    private float mouseX, mouseY; // �Ω�s�x�ƹ�������
    private bool isRotating = false; // �аO�O�_���b����

    public GameObject copyCamera;//�n�Q�ƻs������
    public GameObject superGameObject;//�n�Q��m�b���Ӫ��󩳤U

    private GameObject childGameObject;//�Q�ƻs�X�Ӫ�����
    void Start()
    {
        //copyGameObject = GameObject.Find("MainCamera");
        childGameObject = Instantiate(copyCamera);//�ƻscopyGameObject����(�s�P�Ӫ��󨭤W���}���@�_�ƻs)
        //childGameObject.transform.parent = superGameObject.transform;//���superGameObject����
        //childGameObject.transform.localPosition = Vector3.zero;//�ƻs�X�Ӫ������m���y�Ь�superGameObject���󤺪����I

        //childGameObject.AddComponent<NullScript>(); //�ʺA�W�[�W��"NullScript"���}���즹���󨭤W
        //�U���o�@�檺�\�ର�N�ƻs�X�Ӫ��l����R�W��CopyObject
        childGameObject.name = "MainCamera"+ Random.Range(1, 10);


        //mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        //Debug.Log(gameObject.transform.childCount);
        mainCamera = childGameObject.GetComponent<Camera>();
        Vector3 direction = new Vector3(0, cameraHeight, -5); // �վ�۾������שM�Z�����a���Z��
        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);
        mainCamera.transform.position = mainCamera.transform.position + rotation * direction;
        //transform.position = transform.position + rotation * direction;
        mainCamera.transform.LookAt(this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        HandleRotationInput();
    }

    void LateUpdate()
    {
        HandleCameraMovement();
    }

    void HandleRotationInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true; // ����U�k��ɶ}�l����
        }

        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false; // ���}�k��ɰ������
        }

        if (isRotating)
        {
            mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
            mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
            mouseY = Mathf.Clamp(mouseY, -35, 60); // ��������M�������d��
        }
    }

    void HandleCameraMovement()
    {
        Vector3 direction = new Vector3(0, cameraHeight, -5); // �վ�۾������שM�Z�����a���Z��
        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);
        mainCamera.transform.position = this.transform.position + rotation * direction;

        this.transform.rotation = Quaternion.Euler(0, mouseX, 0);

        mainCamera.transform.LookAt(this.transform.position);
    }
}
