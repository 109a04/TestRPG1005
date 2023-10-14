using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talkable : MonoBehaviour
{
    public string[] lines;
    private bool playerInRange;
    private bool diaFinish = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        //玩家接觸npc
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        //玩家離開npc
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (playerInRange && dialogManager.instance.dialogueBox.activeInHierarchy == false && diaFinish == false && Input.GetKeyDown(KeyCode.F))
        {
            dialogManager.instance.ShowDialogue(lines);
            diaFinish = true;
        }
    }
}
