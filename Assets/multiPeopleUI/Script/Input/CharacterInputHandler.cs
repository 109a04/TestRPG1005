using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    Vector2 moveInputVector = Vector2.zero; // 移動的
    Vector2 viewInputVector = Vector2.zero; // 滑鼠視角的
    bool isJumpButtonPressed = false;
    private bool isRotating = false; // 標記是否正在旋轉

    //Other components
    CharacterMovementHandler characterMovementHandler;

    private void Awake()
    {
        characterMovementHandler = GetComponent<CharacterMovementHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false; // 這裡可能要判斷一下，應該不用加這兩行
    }

    // Update is called once per frame
    void Update()
    {
        //Move input
        moveInputVector.x = Input.GetAxis("Horizontal");
        moveInputVector.y = Input.GetAxis("Vertical");

        // 用這行來處理本地端的視角 Y
        characterMovementHandler.SetViewInputVector(viewInputVector);

        //if (Input.GetMouseButtonDown(1))
        //{
            //View input
            viewInputVector.x = Input.GetAxis("Mouse X") * 10f;
            viewInputVector.y = Input.GetAxis("Mouse Y") * -1 * 3f; //Invert the mouse look
        //}

        /*if (Input.GetMouseButtonDown(1))
        {
            isRotating = true; // 當按下右鍵時開始旋轉
        }

        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false; // 當放開右鍵時停止旋轉
        }

        if (isRotating)
        {
            viewInputVector.x = Input.GetAxis("Mouse X") * 10f;
            viewInputVector.y = Input.GetAxis("Mouse Y") * -1 * 3f; //Invert the mouse look
        }

        isJumpButtonPressed = Input.GetButtonDown("Jump");*/

    }
    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData();

        //View data
        // 我只要旋轉的資料共享給其他人就可以了，不需要上下的
        networkInputData.rotationInput = viewInputVector.x;

        //Move data
        networkInputData.movementInput = moveInputVector;
        
        //Jump data
        networkInputData.isJumpPressed = isJumpButtonPressed;

        return networkInputData;
    }

}
