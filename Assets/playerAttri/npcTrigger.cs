using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcTrigger : Interactable
{
    //public StoreManager storeManager;
    public GameObject panelnpc;//µøµ¡

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (isTriggered == true)
        {
            //storeManager.ToggleStoreUI(isTriggered);
            panelnpc.SetActive(true);
            isTriggered = false;
        }
        else
        {
            //storeManager.ToggleStoreUI(isTriggered);
        }
    }
}
