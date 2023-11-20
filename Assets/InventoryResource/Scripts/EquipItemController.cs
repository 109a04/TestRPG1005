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
        

        if (Input.GetMouseButtonDown(1) && isMouseOverItem) //�ڧ�ϥιD�㪺Ĳ�o�覡�令�k���I���F
        {
            UnEquip();
        }

        FollowMousePos();
    }

    

    public void UnEquip()
    {
        InventoryManager.Instance.AddItem(thisItem);
        EquipmentManager.Instance.weaponItem = null;
        InventoryManager.Instance.UpdateList();
        EquipmentManager.Instance.UpdateSlot();
        EquipmentManager.Instance.UpdatePlayerAttribute();
        ChatManager.Instance.SystemMessage($"���U�Z��<color=#F5EC3D>{thisItem.itemName}�C</color>\n");
    }

}
