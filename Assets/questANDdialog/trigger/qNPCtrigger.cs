using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qNPCtrigger : Interactable
{
    private Questable qTable;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        qTable = this.gameObject.GetComponent<Questable>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (isInRange && Input.GetKeyDown(KeyCode.F))
        {
            NPCMotion.Instance.animator.SetTrigger("TalkTrigger");

            qTable.DelegateQuest();
        }
    }
}
