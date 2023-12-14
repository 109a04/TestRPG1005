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
            Debug.Log("�䤣��۾���");
        }
        else
        {
            Debug.Log("���F�b" + localCamera);
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
        cameraRotationX = Mathf.Clamp(cameraRotationX, -15, 45); // ���������ʪ�����

        localCamera.transform.localRotation = Quaternion.Euler(cameraRotationX, 0, 0);
    }*/

    public override void FixedUpdateNetwork()
    {
        // �o�̭n�h���� photon ���Ҧ�
        // �u�� server �~���u�������z����
        // client �u�O��±����Ӧ� server ����T
        // client ���ާ@�]�O�ǰe�� server �h���B�z��
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

            // �ˬd���S�����X�@�ɤ��~
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
