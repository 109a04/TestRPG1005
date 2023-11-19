using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponItemController : BaseItemController
{
    private Image itemIcon;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        itemIcon = this.transform.Find("ItemIcon").GetComponent<Image>(); //武器圖示
        if (thisItem == null)
        {
            itemIcon.gameObject.SetActive(false); //隱藏圖示
        }
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(1) && isMouseOverItem)
        {
            //替換武器回背包
            ChangeWeapon();
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
        // 武器特有的滑鼠離開邏輯
    }

    private void ChangeWeapon()
    {
        Debug.Log("替換武器");
    }
}
