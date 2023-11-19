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
        itemIcon = this.transform.Find("ItemIcon").GetComponent<Image>(); //�Z���ϥ�
        if (thisItem == null)
        {
            itemIcon.gameObject.SetActive(false); //���ùϥ�
        }
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(1) && isMouseOverItem)
        {
            //�����Z���^�I�]
            ChangeWeapon();
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        // �b�o�̹�{�ƹ��a���ƥ�A�A�i�H���g�Ϊ̽եΰ�������k
        base.OnPointerEnter(eventData);
        // �Z���S�����ƹ��a���޿�
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        // �b�o�̹�{�ƹ����}�ƥ�A�A�i�H���g�Ϊ̽եΰ�������k
        base.OnPointerExit(eventData);
        // �Z���S�����ƹ����}�޿�
    }

    private void ChangeWeapon()
    {
        Debug.Log("�����Z��");
    }
}
