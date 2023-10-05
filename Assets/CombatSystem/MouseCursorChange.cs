using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorChange : MonoBehaviour
{
    public Texture2D cursorTexture;  // 新滑鼠圖示
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public int cursorWidth = 128;
    public int cursorHeight = 128;

    void Start()
    {
        // 預設滑鼠圖示
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
                // 當滑鼠懸停在怪物上時，更改滑鼠圖示為自定義的圖示
                Cursor.SetCursor(cursorTexture, new Vector2(cursorWidth / 2, cursorHeight / 2), CursorMode.Auto);
            }
            else
            {
                // 滑鼠沒懸停在怪物上時，恢復預設滑鼠圖示
                Cursor.SetCursor(null, hotSpot, cursorMode);
            }
        }
    }
}
