using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject exclamationUI; //驚嘆號UI
    public GameObject StatusUI; //整塊怪物狀態欄
    public Slider hpSlider;     //血條
    public Text enemyName;      //名稱文字
    public Text enemyLevel;     //等級文字

    private Camera mainCamera; //主相機
    private Transform enemyTransfrom;
    private Vector3 offset = new Vector3(0f, 2.5f, 0f); // UI元素在頭頂上的偏移值

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
            //隱藏UI
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
