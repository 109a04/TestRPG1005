using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerNav : MonoBehaviour
{
    private int speed; //���ʳt��
    private NavMeshAgent navMeshAgent; //������ɯ�
    private CharacterController characterController; //���
    private float jumpForce = 55f;
    public float rotationSpeed = 700f;
    private float verticalVelocity; //�������O


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
            Debug.Log("�ۦa");
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

        // �ϥ� GetButton ���N GetAxis �H�T�O�b��������ɤ]�ॿ�T�B�z
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
            // �S�������J�ɱN��V�V�q�]�m���s
            //characterController.Move(Vector3.zero);
        }
    }

    
    private void Jump()
    {
        Debug.Log("��");
        navMeshAgent.velocity += Vector3.up * jumpForce;
    }

    //�˴�����O�_�b�a��
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
