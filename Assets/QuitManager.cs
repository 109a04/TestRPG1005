using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitManager : MonoBehaviour
{
    
    public GameObject EscUI;

    private void Start()
    {
        EscUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscUI.SetActive(true);
        }
    }

    public void PressYes()
    {
        Application.Quit();
    }

    public void PressNo()
    {
        EscUI.SetActive(false);
    }
    
}
