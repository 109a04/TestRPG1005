using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    Vector2 moveInputVector = Vector2.zero; // ���ʪ�
    Vector2 viewInputVector = Vector2.zero; // �ƹ�������
    bool isJumpButtonPressed = false;
    private bool isRotating = false; // �аO�O�_���b����

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
        //Cursor.visible = false; // �o�̥i��n�P�_�@�U�A���Ӥ��Υ[�o���
    }

    // Update is called once per frame
    void Update()
    {
        //Move input
        moveInputVector.x = Input.GetAxis("Horizontal");
        moveInputVector.y = Input.GetAxis("Vertical");

        // �γo��ӳB�z���a�ݪ����� Y
        characterMovementHandler.SetViewInputVector(viewInputVector);

        //if (Input.GetMouseButtonDown(1))
        //{
            //View input
            viewInputVector.x = Input.GetAxis("Mouse X") * 10f;
            viewInputVector.y = Input.GetAxis("Mouse Y") * -1 * 3f; //Invert the mouse look
        //}

        /*if (Input.GetMouseButtonDown(1))
        {
            isRotating = true; // ����U�k��ɶ}�l����
        }

        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false; // ���}�k��ɰ������
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
        // �ڥu�n���઺��Ʀ@�ɵ���L�H�N�i�H�F�A���ݭn�W�U��
        networkInputData.rotationInput = viewInputVector.x;

        //Move data
        networkInputData.movementInput = moveInputVector;
        
        //Jump data
        networkInputData.isJumpPressed = isJumpButtonPressed;

        return networkInputData;
    }

}
