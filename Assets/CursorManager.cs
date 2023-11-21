using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CursorManager : MonoBehaviour
{
    public Texture2D cursorTexture;


    void Update()
    {
        Cursor.SetCursor(cursorTexture, Vector3.zero, CursorMode.ForceSoftware);
    }
}
