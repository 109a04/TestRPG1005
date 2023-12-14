using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CharacterMovementHandler : NetworkBehaviour
{
    //Vector2 viewInput;
    
    //Rotation
    //float cameraRotationX = 0;

    //Other components
    NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;
    Camera localCamera;

    private void Awake()
    {
        networkCharacterControllerPrototypeCustom = GetComponent<NetworkCharacterControllerPrototypeCustom>();
        localCamera = GetComponentInChildren<Camera>();
        if(localCamera == null)
        {
            Debug.Log("找不到相機啦");
        }
        else
        {
            Debug.Log("找到了在" + localCamera);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /*void Update()
    {
        cameraRotationX += viewInput.y * Time.deltaTime * networkCharacterControllerPrototypeCustom.viewUpDownRotationSpeed;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -15, 45); // 限制視角轉動的角度

        localCamera.transform.localRotation = Quaternion.Euler(cameraRotationX, 0, 0);
    }*/

    public override void FixedUpdateNetwork()
    {
        // 這裡要去重溫 photon 的模式
        // 只有 server 才有真正的物理模擬
        // client 只是單純接收來自 server 的資訊
        // client 的操作也是傳送到 server 去做處理的
        if(GetInput(out NetworkInputData networkInputData))
        {
            //Rotate the view
            //networkCharacterControllerPrototypeCustom.Rotate(networkInputData.rotationInput);
            // Rotate the transform according to the client aim vector
            transform.forward = networkInputData.aimForwardVector;

            //Cancel out rotation on X axis as we don't want our character to tilt
            Quaternion rotation = transform.rotation;
            rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, rotation.eulerAngles.z);
            transform.rotation = rotation;

            //Move
            Vector3 moveDirection = transform.forward * networkInputData.movementInput.y + transform.right * networkInputData.movementInput.x;
            moveDirection.Normalize();

            networkCharacterControllerPrototypeCustom.Move(moveDirection);

            //Jump
            if (networkInputData.isJumpPressed)
            {
                networkCharacterControllerPrototypeCustom.Jump();
            }

            // 檢查有沒有掉出世界之外
            CheckFallRespawn();

        }
    }

    void CheckFallRespawn()
    {
        if(transform.position.y < -12)
        {
            transform.position = Utils.GetRandomSpawnPoint();
        }
    }

    /*public void SetViewInputVector(Vector2 viewInput)
    {
        this.viewInput = viewInput;
    }*/

}
