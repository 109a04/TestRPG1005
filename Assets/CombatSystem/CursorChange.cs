using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 若怪物在玩家的攻擊範圍內，滑鼠移到怪物上方時游標圖示會改變
/// </summary>
public class CursorChange : MonoBehaviour
{
    public Texture2D normaltexture; //原本滑鼠圖示
    public Texture2D fightTexture;  // 戰鬥滑鼠圖示
    public Texture2D toggleButtonTexture; //移到按鈕上方的圖示
    private Vector2 hotSpot = Vector2.zero;


    void Update()
    {
        Cursor.SetCursor(normaltexture, hotSpot, CursorMode.ForceSoftware);

        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider == null) return; //如果射線沒射到東西不做事
            if (!hitInfo.collider.CompareTag("Enemy")) return; //Tag 不是怪物也不做事
            GameObject enemyTransform = hitInfo.collider.gameObject;
            float distance = Vector3.Distance(transform.position, enemyTransform.transform.position); //檢查與怪物的位置

            
            if (distance > playerAttributeManager.Instance.atkRange) return; //沒有在玩家的攻擊範圍內也不做事
            Cursor.SetCursor(fightTexture, hotSpot, CursorMode.ForceSoftware); // 當滑鼠懸停在怪物上時，更改滑鼠圖示為自定義的圖示

            
            
        }
    }
}
