using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public string objName; //物件名稱
    public string prompt; //互動提示
    public Transform interactTransform; //互動位置，把想要互動的物件拉到這裡
    public Text nameText; //把字拉進來
    public Text hintText; //提示文字
    public Image pressF;  //按鈕圖片
    protected Vector3 offset = new Vector3(0f, 2f, 0f); // UI元素在頭頂上的偏移值
    protected Vector3 moreHigh = new Vector3(0f, 80f, 0f); //更高
    protected Vector3 moreMoreHigh = new Vector3(0f, 125f, 0f); //更更高
    protected Camera mainCamera;
    public Transform player;

    protected bool isInRange;
    protected bool isTriggered;
    private bool isTurning = false; // 新增一個標誌位，表示是否正在進行轉向

    // Start is called before the first frame update
    public virtual void Start()
    {
        mainCamera = Camera.main;
        isTriggered = false; //初始化
        isInRange = false;
        nameText.gameObject.SetActive(false); //隱藏以下UI元素
        hintText.gameObject.SetActive(false);
        pressF.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").transform; //找尋玩家物件
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            nameText.gameObject.SetActive(true);
            hintText.gameObject.SetActive(true);
            pressF.gameObject.SetActive(true);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            nameText.gameObject.SetActive(false);
            hintText.gameObject.SetActive(false);
            pressF.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    public virtual void Update()
    {
        // 將物件的世界空間位置轉換為屏幕空間位置
        Vector3 npcScrPos = mainCamera.WorldToScreenPoint(interactTransform.position + offset);
        nameText.text = $"<color=#FFD700>{objName}</color>\n";
        hintText.text = prompt;

        // 將UI元素的位置設置為螢幕空間位置
        nameText.transform.position = npcScrPos;
        pressF.transform.position = npcScrPos + moreHigh;
        hintText.transform.position = npcScrPos + moreMoreHigh;

        //判斷是否開始互動
        if (isInRange == true && Input.GetKeyDown(KeyCode.F))
        {
            isTriggered = !isTriggered; //繼承腳本可以用這個變數去做額外的判斷執行
                                        //例：if (isTriggered) { 你想要讓它做的事情 }
            if (!isTurning)
            {
                StartCoroutine(TurnTowardsPlayer());
            }
        }
    }

    IEnumerator TurnTowardsPlayer()
    {
        isTurning = true;

        // 先計算目標方向
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

        // 進行平滑旋轉
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5.0f * Time.deltaTime);
            yield return null;
        }

        // 轉向完成後設置 isInRange 為 false

        isTurning = false;
    }
}
