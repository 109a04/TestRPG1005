using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �s�U�ةǪ����ʬ������ܼơA�ڬO�Q�����ʳt�ק����d�򪺳��Τ@�աA���D����ڭ��٦�����ɶ��@�Ӥ@�ӳ]�w
/// </summary>


public class EnemyActionVariables : MonoBehaviour
{
    //�t�׬���
    internal float currentSpeed;
    internal float walkSpeed = 5.0f; //�`�A���ʳt��
    internal float chaseSpeed = 8.0f; //�l�����A���ʳt��

    //�C���d��
    public float wanderRadius = 10.0f;

    //�����d�����
    public float visionRadius = 12.0f; //�����d��
    public float chaseRadius = 15.0f;  //�l���d��A������d��y�j
    public float attackRadius = 7.0f; //�����d��A��W����̤p


    //����������j���F��
    internal bool canAttack;
    internal bool isBeaten; //�Q���F



    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = walkSpeed;
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
