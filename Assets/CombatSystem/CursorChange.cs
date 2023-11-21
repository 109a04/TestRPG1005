using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// �Y�Ǫ��b���a�������d�򤺡A�ƹ�����Ǫ��W��ɴ�йϥܷ|����
/// </summary>
public class CursorChange : MonoBehaviour
{
    public Texture2D normaltexture; //�쥻�ƹ��ϥ�
    public Texture2D fightTexture;  // �԰��ƹ��ϥ�
    public Texture2D toggleButtonTexture; //������s�W�誺�ϥ�
    private Vector2 hotSpot = Vector2.zero;


    void Update()
    {
        Cursor.SetCursor(normaltexture, hotSpot, CursorMode.ForceSoftware);

        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider == null) return; //�p�G�g�u�S�g��F�褣����
            if (!hitInfo.collider.CompareTag("Enemy")) return; //Tag ���O�Ǫ��]������
            GameObject enemyTransform = hitInfo.collider.gameObject;
            float distance = Vector3.Distance(transform.position, enemyTransform.transform.position); //�ˬd�P�Ǫ�����m

            
            if (distance > playerAttributeManager.Instance.atkRange) return; //�S���b���a�������d�򤺤]������
            Cursor.SetCursor(fightTexture, hotSpot, CursorMode.ForceSoftware); // ��ƹ��a���b�Ǫ��W�ɡA���ƹ��ϥܬ��۩w�q���ϥ�

            
            
        }
    }
}
