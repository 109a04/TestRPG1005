using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalCameraHandler : MonoBehaviour
{
    public Transform cameraAnchorPoint;

    // Input
    Vector2 viewInput;

    // Rotation
    float cameraRotationX = 0;
    float cameraRotationY = 0;

    // Other components
    NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;
    Camera localCamera;

    private void Awake()
    {
        localCamera = GetComponent<Camera>();
        networkCharacterControllerPrototypeCustom = GetComponentInParent<NetworkCharacterControllerPrototypeCustom>();
    }


    // Start is called before the first frame update
    void Start()
    {
        if (localCamera.enabled)
        {
            localCamera.transform.parent = null;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(cameraAnchorPoint == null)
        {
            return;
        }
        if (!localCamera.enabled)
        {
            return;
        }

        // Move the camera to the position of the player
        localCamera.transform.position = cameraAnchorPoint.position;

        //�p�����
        cameraRotationX += viewInput.y * Time.deltaTime * networkCharacterControllerPrototypeCustom.viewUpDownRotationSpeed;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -35, 60);

        cameraRotationY += viewInput.x * Time.deltaTime * networkCharacterControllerPrototypeCustom.rotationSpeed;

        localCamera.transform.rotation = Quaternion.Euler(cameraRotationX, cameraRotationY, 0);
    }

    public void SetViewInputVector(Vector2 viewInput)
    {
        this.viewInput = viewInput;
    }

}
