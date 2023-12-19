using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerNav : MonoBehaviour
{
    private int speed; //移動速度
    private NavMeshAgent navMeshAgent; //防撞牆導航
    private CharacterController characterController; //控制器
    private float jumpForce = 55f;
    public float rotationSpeed = 700f;
    private float verticalVelocity; //垂直重力


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = transform.GetComponent<NavMeshAgent>();
        characterController = transform.GetComponent<CharacterController>();
        speed = playerAttributeManager.Instance.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAttributeManager.Instance.hp == 0) return;
        if (ChatManager.Instance.inputField.isFocused) return;

        
        if (IsGround())
        {
            Debug.Log("著地");
            verticalVelocity = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        Move();

    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 使用 GetButton 替代 GetAxis 以確保在按鍵釋放時也能正確處理
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
            direction = Camera.main.transform.TransformDirection(direction);
            transform.forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
        else
        {
            // 沒有按鍵輸入時將方向向量設置為零
            //characterController.Move(Vector3.zero);
        }
    }

    
    private void Jump()
    {
        Debug.Log("跳");
        navMeshAgent.velocity += Vector3.up * jumpForce;
    }

    //檢測角色是否在地面
    private bool IsGround()
    {
        float rayLength = 1f;
        return Physics.Raycast(transform.position, Vector3.down, rayLength);
    }

    public void ChangeSpeed(int newSpeed)
    {
        speed = newSpeed;
    }
}
