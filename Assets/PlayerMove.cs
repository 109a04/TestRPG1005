using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 10;//角色速度
    public float RotateSpeed = 1f;//轉動速度(沒用)

    public float Gravity = -9.8f;//重力預設
    private Vector3 Velocity = Vector3.zero;//預設為0的速度向量

    public float JumpHight = 3f;//跳躍高度
    private bool IsGround;//是否在地面
    public Transform GroundCheck;
    public float CheckRadius = 0.2f;//是否在地面檢查器的半徑
    public LayerMask layerMask;//篩選地面層

    // Start is called before the first frame update
    void Start()
    {
        controller = transform.GetComponent<CharacterController>();
}

    // Update is called once per frame
    void Update()
    {
        PlayerMoveMethod();
    }

    private void PlayerMoveMethod()
    {
        //是否在地面檢查
        IsGround = Physics.CheckSphere(GroundCheck.position, CheckRadius, layerMask);
        if (IsGround && Velocity.y < 0)//Velocity.y為垂直速度，因為垂直速度為負等於角色正在落下
        {
            Velocity.y = 0;//角色回到地面後，重置垂直速度
        }

        //跳躍
        if (Input.GetButtonDown("Jump"))
        {
            Velocity.y = Mathf.Sqrt(JumpHight * -2 * Gravity);//套用跳躍公式
        }

        //w,a,s,d的輸入
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        //相機往前和往右的方向
        var cameraForward = Camera.main.transform.forward;
        var cameraRight = Camera.main.transform.right;

        //利用w,a,s,d的輸入和相機往前和往右的方向來算出角色方向
        var moveDirection = (cameraForward * vertical + cameraRight * horizontal).normalized;

        //角色移動
        controller.Move(moveDirection * speed * Time.deltaTime);

        //重力，讓角色能回到地面
        Velocity.y += Gravity * Time.deltaTime;
        controller.Move(new Vector3(0f, Velocity.y, 0f) * Time.deltaTime);


        //mouse vector - player vector ,can get the vector point mouse from player 
        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        var point = Input.mousePosition - playerScreenPoint;
        var angle = Mathf.Atan2(point.x, point.y) * Mathf.Rad2Deg;//get the rotate angle

        //角色轉動
        controller.transform.eulerAngles = new Vector3(controller.transform.eulerAngles.x, angle, controller.transform.eulerAngles.z);
    }

}
