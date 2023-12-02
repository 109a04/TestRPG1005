using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // ���a����
    public float rotationSpeed = 3f; // ��������t��
    public float cameraHeight = 5.5f; // �۾�����

    private float mouseX, mouseY; // �Ω�s�x�ƹ�������
    private bool isRotating = false; // �аO�O�_���b����


    void Start()
    {
        Vector3 direction = new Vector3(0, cameraHeight, -5); // �վ�۾������שM�Z�����a���Z��
        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);
        transform.position = player.position + rotation * direction;
        transform.LookAt(player.position);
    }

    void Update()
    {
        HandleRotationInput();
    }

    void LateUpdate()
    {
        if (isRotating)
        {
            HandleCameraMovement();
        }
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
        transform.position = player.position + rotation * direction;
        transform.LookAt(player.position);
    }
}
