using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControlCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCamera;
    public float rotationSpeed = 3f; // ��������t��
    public float cameraHeight = 5f; // �۾�����

    private float mouseX, mouseY; // �Ω�s�x�ƹ�������
    private bool isRotating = false; // �аO�O�_���b����
    void Start()
    {
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
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
