using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class NetworkCameraFollow : MonoBehaviour
{
    public Transform player; // 玩家物件
    public float rotationSpeed = 3f; // 視角旋轉速度
    public float cameraHeight = 5f; // 相機高度

    private float mouseX, mouseY; // 用於存儲滑鼠的移動
    private bool isRotating = false; // 標記是否正在旋轉
    //public NetworkObject networkObject;
    public NetworkCharacterControllerPrototype networkCharactorController;


    void Start()
    {
        
        if (networkCharactorController)
        {
            Vector3 direction = new Vector3(0, cameraHeight, -5); // 調整相機的高度和距離玩家的距離
            Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);
            transform.position = transform.position + rotation * direction;
            transform.LookAt(player.transform.position);
        }
            
    }

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
        

        if (networkCharactorController)
        {
            Vector3 direction = new Vector3(0, cameraHeight, -5); // 調整相機的高度和距離玩家的距離
            Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0);
            transform.position = transform.position + rotation * direction;
            player.rotation = Quaternion.Euler(0, mouseX, 0);
            transform.LookAt(player.transform.position);
        }
    }
}
