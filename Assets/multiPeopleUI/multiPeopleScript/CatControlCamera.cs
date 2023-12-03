using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControlCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCamera;
    public float rotationSpeed = 3f; // 視角旋轉速度
    public float cameraHeight = 5f; // 相機高度

    private float mouseX, mouseY; // 用於存儲滑鼠的移動
    private bool isRotating = false; // 標記是否正在旋轉
    void Start()
    {
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        Vector3 direction = new Vector3(0, cameraHeight, -5); // 調整相機的高度和距離玩家的距離
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
            isRotating = true; // 當按下右鍵時開始旋轉
        }

        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false; // 當放開右鍵時停止旋轉
        }

        if (isRotating)
        {
            mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
            mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
            mouseY = Mathf.Clamp(mouseY, -35, 60); // 限制仰角和俯角的範圍
        }
    }

    void HandleCameraMovement()
    {
        Vector3 direction = new Vector3(0, cameraHeight, -5); // 調整相機的高度和距離玩家的距離
        Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);
        mainCamera.transform.position = this.transform.position + rotation * direction;

        this.transform.rotation = Quaternion.Euler(0, mouseX, 0);

        mainCamera.transform.LookAt(this.transform.position);
    }
}
