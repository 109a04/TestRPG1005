using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 10;//����t��
    public float RotateSpeed = 1f;//��ʳt��(�S��)

    public float Gravity = -9.8f;//���O�w�]
    private Vector3 Velocity = Vector3.zero;//�w�]��0���t�צV�q

    public float JumpHight = 3f;//���D����
    private bool IsGround;//�O�_�b�a��
    public Transform GroundCheck;
    public float CheckRadius = 0.2f;//�O�_�b�a���ˬd�����b�|
    public LayerMask layerMask;//�z��a���h

    private int jumpCount = 0;  // �O�����D����


    public float horizontalRotationSpeed = 2f; // ��������t��
    public float verticalRotationSpeed = 2f;   // ��������t��
    public float verticalRotationLimit = 80f;  // ��������W�U��

    private float verticalRotation = 0f;  // �x�s�������઺����

    private bool isRotating = false; // �аO�O�_���b����

    // Start is called before the first frame update
    void Start()
    {
        controller = transform.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(playerAttributeManager.Instance.hp == 0)) //�p�G���a�S�����~�ಾ��
        {
            PlayerMoveMethod();
        }
    }

    private void PlayerMoveMethod()
    {
        //Debug.Log("���a����");
        //�O�_�b�a���ˬd
        //IsGround = Physics.Raycast(GroundCheck.position, Vector3.down, 0.1f, layerMask);
        IsGround = Physics.CheckSphere(GroundCheck.position, CheckRadius, layerMask);
        if (IsGround)
        {
            Debug.Log("���a");
        }
        if (IsGround && Velocity.y < 0)//Velocity.y�������t�סA�]�������t�׬��t���󨤦⥿�b���U
        {
            Velocity.y = 0;//����^��a����A���m�����t��
            jumpCount = 0;//���m���D����
        }

        //���D
        if (Input.GetButtonDown("Jump") && jumpCount < 1)
        {
            jumpCount++;
            Velocity.y = Mathf.Sqrt(JumpHight * -2 * Gravity);//�M�θ��D����
        }

        //���O�A�������^��a��
        Velocity.y += Gravity * Time.deltaTime;
        controller.Move(new Vector3(0f, Velocity.y, 0f) * Time.deltaTime);


        /* �¤�k
        
        //w,a,s,d����J
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        //�۾����e�M���k����V
        var cameraForward = Camera.main.transform.forward;
        var cameraRight = Camera.main.transform.right;

        //�Q��w,a,s,d����J�M�۾����e�M���k����V�Ӻ�X�����V
        var moveDirection = (cameraForward * vertical + cameraRight * horizontal).normalized;

        //���Ⲿ��
        controller.Move(moveDirection * speed * Time.deltaTime);


        //
        //�����N�|Ĳ�o�����F��panel
        if (transform.position.y < -30f)
        {
            GameManager.Instance.SetIsDead();
            Debug.Log(transform.position.y);
        }
        //

       
        //mouse vector - player vector ,can get the vector point mouse from player 
        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        var point = Input.mousePosition - playerScreenPoint;

        //�ƹ������̡A�����N������
        //var angle = Mathf.Atan2(point.x, point.y) * Mathf.Rad2Deg;//get the rotate angle
        var angle = Mathf.Atan2(point.x, point.y) * Mathf.Rad2Deg * RotateSpeed;

        //�ƹ������̡A����M�����N������
        controller.transform.eulerAngles = new Vector3(controller.transform.eulerAngles.x, angle, controller.transform.eulerAngles.z);

        //����M�����X�M�a��A���O�֦R�F
        //
        controller.transform.rotation = Quaternion.Slerp(
            controller.transform.rotation,
            Quaternion.Euler(new Vector3(0, angle, 0)),
            RotateSpeed * Time.deltaTime
        );
        //

        */

        //�s��k

        // ���o�ƹ������ʶq
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // ���ਤ�������V
        //transform.Rotate(Vector3.up * mouseX * horizontalRotationSpeed);

        /*
        // �����������
        verticalRotation -= mouseY * verticalRotationSpeed;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);

        // �]�w�۾�����������
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        */

        // ���ʤ�V
        var moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;


        // �N���ʤ�V�ഫ���۹��۾���������V
        moveDirection = Camera.main.transform.TransformDirection(moveDirection);

        // ���ʨ���
        controller.Move(moveDirection * speed * Time.deltaTime);

    }

}