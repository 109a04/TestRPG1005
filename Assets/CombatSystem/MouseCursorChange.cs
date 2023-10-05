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
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                // ��ƹ��a���b�Ǫ��W�ɡA���ƹ��ϥܬ��۩w�q���ϥ�
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
