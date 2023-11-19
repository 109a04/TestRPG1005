using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteract : InteractV2
{
    private ItemPickup itemPickup;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        itemPickup = this.gameObject.GetComponent<ItemPickup>();    
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (isInRange && Input.GetKeyDown(KeyCode.F))
        {
            itemPickup.Pickup();
        }
    }
}
