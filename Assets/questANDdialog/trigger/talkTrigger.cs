using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talkTrigger : Interactable
{
    private Talkable qTalk;



    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        qTalk = this.gameObject.GetComponent<Talkable>();
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (isInRange && Input.GetKeyDown(KeyCode.F))
        {
            
            qTalk.StartTalk();

            isInRange = false;
        }
    }

    
}
