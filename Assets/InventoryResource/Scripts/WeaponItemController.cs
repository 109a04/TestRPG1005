using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponItemController : BaseItemController
{
    public static WeaponItemController Instance;
    public Transform playerHand;

    public float screenHeightThreshold = 0.7f; // 螢幕高度的閾值
    public float screenWidthThreshold = 0.8f; // 螢幕寬度的閾值


    private void Awake()
    {
        Instance = this;
    }

    private Image itemIcon;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        itemIcon = transform.Find("ItemIcon").GetComponent<Image>(); //武器圖示

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (thisItem == null)
        {
            Debug.Log("武器為空");
            itemIcon.gameObject.SetActive(false); //隱藏圖示
        }
        else
        {
            itemIcon.sprite = thisItem.itemIcon;
            itemIcon.gameObject.SetActive(true); //否則顯示圖示
        }

        if (Input.GetMouseButtonDown(1) && isMouseOverItem)
        {
            //替換武器回背包
            InventoryManager.Instance.AddItem(thisItem); //把武器道具塞回去
            ChatManager.Instance.SystemMessage($"卸下武器<color=#F5EC3D>{thisItem.itemName}。</color>\n");
            thisItem = null;
        }

        //這一大串真的好醜，之後有時間會簡化
        Vector3 mouseScreenPos = Input.mousePosition;

        // 將滑鼠螢幕座標轉換成世界座標
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, 10f));

        // 將世界座標轉換成螢幕座標，只取 x 軸值
        float mouseScreenX = Camera.main.WorldToScreenPoint(mouseWorldPos).x;

        // 將世界座標轉換成螢幕座標，只取 y 軸值
        float mouseScreenY = Camera.main.WorldToScreenPoint(mouseWorldPos).y;

        // 判斷滑鼠是否位於螢幕的某處之上
        bool isAboveThreshold = mouseScreenY > Screen.height * screenHeightThreshold;

        // 判斷滑鼠是否位於螢幕的右側
        bool isOnRightSide = mouseScreenX > Screen.width * screenWidthThreshold;

        if (isMouseOverItem)
        {
            if (isAboveThreshold)
            {
                if (!isOnRightSide)
                {
                    DescriptionPanel.transform.position = Input.mousePosition + new Vector3(250, -200, 0);
                }
                else
                {
                    DescriptionPanel.transform.position = Input.mousePosition + new Vector3(-250, -200, 0);
                }
            }
            else
            {
                if (!isOnRightSide)
                {
                    DescriptionPanel.transform.position = Input.mousePosition + new Vector3(250, 100, 0);
                }
                else
                {
                    DescriptionPanel.transform.position = Input.mousePosition + new Vector3(-250, 100, 0);
                }

            }
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        // 在這裡實現滑鼠懸停事件，你可以重寫或者調用基類的方法
        base.OnPointerEnter(eventData);
        // 武器特有的滑鼠懸停邏輯
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        // 在這裡實現滑鼠離開事件，你可以重寫或者調用基類的方法
        base.OnPointerExit(eventData);
    }

    public void ChangeWeapon(Item newWeaponItem)
    {
        Debug.Log("替換武器");
        if (thisItem == null) //如果當前沒有任何裝備
        {
            thisItem = newWeaponItem; //直接換上裝備
        }
        else
        {
            InventoryManager.Instance.AddItem(thisItem); //把武器道具塞回去
            thisItem = newWeaponItem;
        }
    }

}
