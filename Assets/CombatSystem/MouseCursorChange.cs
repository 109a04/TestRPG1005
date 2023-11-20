using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorChange : MonoBehaviour
{
    public Texture2D cursorTexture;  // �s�ƹ��ϥ�
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public int cursorWidth = 128;
    public int cursorHeight = 128;

    void Start()
    {
        // �w�]�ƹ��ϥ�
        Cursor.SetCursor(null, hotSpot, cursorMode);
    }

    void Update()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {

            if (hitInfo.collider != null && hitInfo.collider.CompareTag("Enemy"))
            {
                GameObject enemyTransform = hitInfo.collider.gameObject;
                float distance = Vector3.Distance(transform.position, enemyTransform.transform.position);
                // ��ƹ��a���b�Ǫ��W�ɡA���ƹ��ϥܬ��۩w�q���ϥ�
                if (distance > playerAttributeManager.Instance.atkRange) return;
                Cursor.SetCursor(cursorTexture, new Vector2(cursorWidth / 2, cursorHeight / 2), CursorMode.Auto);
            }
            else
            {
                // �ƹ��S�a���b�Ǫ��W�ɡA��_�w�]�ƹ��ϥ�
                Cursor.SetCursor(null, hotSpot, cursorMode);
            }
        }
    }
}
