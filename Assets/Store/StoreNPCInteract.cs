using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreNPCInteract : Interactable
{
    public StoreManager storeManager;

    public override void Update()
    {
        base.Update();

        if (isTriggered == true)
        {
            storeManager.ToggleStoreUI(isTriggered);
        }
        else
        {
            storeManager.ToggleStoreUI(isTriggered);
        }
    }
}
