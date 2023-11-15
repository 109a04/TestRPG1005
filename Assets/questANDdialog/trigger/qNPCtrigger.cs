using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qNPCtrigger : Interactable
{
    private Questable qTable;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        qTable = this.gameObject.GetComponent<Questable>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (isInRange && Input.GetKeyDown(KeyCode.F))
        {
            qTable.DelegateQuest();
        }
    }
}
