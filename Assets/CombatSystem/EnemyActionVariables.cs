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
    internal float wanderRadius = 10.0f;

    //�����d�����
    internal float visionRadius = 12.0f; //�����d��
    internal float chaseRadius = 15.0f;  //�l���d��A������d��y�j
    internal float attackRadius = 7.0f; //�����d��A��W����̤p

    //UI����
    protected Camera mainCamera; //�D�۾�
    internal Transform enemyTransfrom;
    internal GameObject exclamationUI; //��ĸ�UI
    protected Vector3 offset = new Vector3(0f, 2.5f, 0f); // UI�����b�Y���W��������

    public GameObject StatusUI; //����Ǫ����A��
    public Slider hpSlider;     //���
    public Text enemyName;      //�W�٤�r
    public Text enemyLevel;     //���Ť�r

    //����������j���F��
    internal bool canAttack;



    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        enemyTransfrom = GetComponent<Transform>();
        currentSpeed = walkSpeed;
        exclamationUI = GameObject.Find("Canvas/Exclamation");
        enemyName = StatusUI.transform.Find("Name").GetComponent<Text>();
        enemyLevel = StatusUI.transform.Find("Level").GetComponent<Text>();
        hpSlider = StatusUI.GetComponent<Slider>();

        
        
        if(exclamationUI == null)
        {
            Debug.LogError("Exclamation UI not found!");
        }
        else
        {   //����UI
            exclamationUI.SetActive(false);
        }
        canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 enemyScrPos = mainCamera.WorldToScreenPoint(enemyTransfrom.position + offset);
        exclamationUI.transform.position = enemyScrPos;
    }
}
