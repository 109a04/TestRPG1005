using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EquipmentManager : MonoBehaviour
{
    public GameObject equipmentUI; //�˳Ƥ�����UI
    public Transform EquipmentTransfrom; //Prefab�ͦ��b����a��
    public GameObject EquipmentPrefab;

    public WeaponItem weaponItem; //�Z����
    public BaseItemController equipment; //����ഫ
    public Transform PlayerSkeleton; //���a���[
    public Transform playerHand; //���a�ⳡ��m
    public GameObject equipmentModel; //�ݷ|�|��Ҥƪ��D��ҫ�

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
        UpdateParameter();
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

    public void UpdateParameter()
    {
        playerAttributeManager.Instance.weapon = (int) weaponItem.weaponType;
        playerAttributeManager.Instance.element = (int)weaponItem.elementType;
    }

    private void ShowModel()
    {
        if (weaponItem == null) return;
        Debug.Log("�˳ƪZ��");
        equipmentModel = Instantiate(weaponItem.weaponModel, playerHand);
        equipmentModel.transform.parent = PlayerSkeleton;

        SetLayer(equipmentModel, 8);

    }

    private void DestoryModel()
    {
        Destroy(equipmentModel);
    }

    private void SetLayer(GameObject obj ,int layerNum) //���j�]�w����ϼh
    {
        obj.layer = layerNum;

        foreach(Transform child in obj.transform)
        {
            SetLayer(child.gameObject, layerNum);
        }
    }
}
