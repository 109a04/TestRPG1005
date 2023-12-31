using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItemController : BaseItemController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        

        if (Input.GetMouseButtonDown(1) && isMouseOverItem) //我把使用道具的觸發方式改成右鍵點擊了
        {
            UnEquip();
        }

        FollowMousePos();
    }

    

    public void UnEquip()
    {
        InventoryManager.Instance.AddItem(thisItem);
        EquipmentManager.Instance.weaponItem = null;
        EquipmentManager.Instance.DestoryModel();
        InventoryManager.Instance.UpdateList();
        EquipmentManager.Instance.UpdateSlot();
        EquipmentManager.Instance.UpdatePlayerAttribute();
        if(EquipmentManager.Instance.weaponItem == null)
        {
            EquipmentManager.Instance.StickL.SetActive(true);
            EquipmentManager.Instance.StickR.SetActive(true);
        } 
    }

}
