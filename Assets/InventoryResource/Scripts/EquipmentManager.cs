using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EquipmentManager : MonoBehaviour
{
    public GameObject equipmentUI; //裝備介面的UI
    public Transform EquipmentTransfrom; //Prefab生成在什麼地方
    public GameObject EquipmentPrefab;

    public WeaponItem weaponItem; //武器欄
    public BaseItemController equipment; //資料轉換
    public Transform PlayerSkeleton; //玩家骨架
    public Transform playerHand; //玩家手部位置
    public GameObject equipmentModel; //待會會實例化的道具模型

    public static EquipmentManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        equipmentUI.SetActive(false);
        UpdateSlot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Equip(WeaponItem newWeapon)
    {
        if (weaponItem != null)
        {
            EquipItemController equipItemController = FindObjectOfType<EquipItemController>();
            InventoryManager.Instance.AddItem(weaponItem);
            weaponItem = null;
            InventoryManager.Instance.UpdateList();
            UpdateSlot();
            DestoryModel();
        }
        weaponItem = newWeapon;
        ShowModel();
        UpdatePlayerAttribute();
        PlayerAttack.Instance.SetPlayerAtkParameter();
    }

    public void UpdateSlot()
    {
        foreach (Transform child in EquipmentTransfrom)
        {
            Destroy(child.gameObject);
        }

        equipment = new BaseItemController();

        if (weaponItem == null) { return; }
        GameObject obj = Instantiate(EquipmentPrefab, EquipmentTransfrom);
        var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
        itemIcon.sprite = weaponItem.itemIcon;

        equipment = obj.GetComponent<BaseItemController>();
        equipment.SetItem(weaponItem);
    }

    public void UpdatePlayerAttribute()
    {
        if(weaponItem == null)
        
        {
            playerAttributeManager.Instance.weapon = 0;
            playerAttributeManager.Instance.element = 0;
            playerAttributeManager.Instance.attack = playerAttributeManager.Instance.origin_attack * 2;
            playerAttributeManager.Instance.atkRange = 10;
            return;
        }

        playerAttributeManager.Instance.weapon = (int) weaponItem.weaponType;
        playerAttributeManager.Instance.element = (int) weaponItem.elementType;
        playerAttributeManager.Instance.attack = playerAttributeManager.Instance.origin_attack * 2 + weaponItem.damage;

        switch (weaponItem.weaponType) //根據不同武器改變攻擊範圍
        {
            case WeaponItem.WeaponType.Sword:
                playerAttributeManager.Instance.atkRange = 15;
                break;
            case WeaponItem.WeaponType.MagicWand:
                playerAttributeManager.Instance.atkRange = 20;
                break;
            case WeaponItem.WeaponType.Heavy:
                playerAttributeManager.Instance.atkRange = 5;
                break;
            case WeaponItem.WeaponType.Bow:
                playerAttributeManager.Instance.atkRange = 25;
                break;
        }
    }

    private void ShowModel()
    {
        if (weaponItem == null) return;
        Debug.Log("裝備武器");
        equipmentModel = Instantiate(weaponItem.weaponModel, playerHand);
        equipmentModel.transform.parent = PlayerSkeleton;

        SetLayer(equipmentModel, 8);

    }

    private void DestoryModel()
    {
        Destroy(equipmentModel);
    }

    private void SetLayer(GameObject obj ,int layerNum) //遞迴設定物件圖層
    {
        obj.layer = layerNum;

        foreach(Transform child in obj.transform)
        {
            SetLayer(child.gameObject, layerNum);
        }
    }
}
