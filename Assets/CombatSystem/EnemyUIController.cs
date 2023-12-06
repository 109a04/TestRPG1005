using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject exclamationUI; //��ĸ�UI
    public GameObject StatusUI; //����Ǫ����A��
    public Slider hpSlider;     //���
    public Text enemyName;      //�W�٤�r
    public Text enemyLevel;     //���Ť�r

    private Camera mainCamera; //�D�۾�
    private Transform enemyTransfrom;
    private Vector3 offset = new Vector3(0f, 2.5f, 0f); // UI�����b�Y���W��������

    void Start()
    {
        mainCamera = Camera.main;
        enemyTransfrom = transform;

        enemyName = StatusUI.transform.Find("Name").GetComponent<Text>();
        enemyLevel = StatusUI.transform.Find("Level").GetComponent<Text>();

        if (hpSlider == null)
        {
            Debug.LogError("hpSlider is not assigned in the inspector.");
            return;
        }

        if (exclamationUI == null)
        {
            Debug.LogError("Exclamation UI not found!");
        }
        else
        {
            //����UI
            exclamationUI.SetActive(false);
        }

        
        
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 enemyScrPos = mainCamera.WorldToScreenPoint(enemyTransfrom.position + offset);
        exclamationUI.transform.position = enemyScrPos;
    }

}
