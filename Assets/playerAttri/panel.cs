using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panel : MonoBehaviour

    
{
    public GameObject paneljump;//����
    public GameObject panel1;//����
    public GameObject panel2;//����

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void panelopen()
    {
        paneljump.SetActive(true);
    }

    public void panelclose()
    {
        paneljump.SetActive(false);
    }

    public void panelBothclose()
    {
        panel1.SetActive(false);
        panel2.SetActive(false);
    }
    public void panelnext()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
    }
    public void panellast()
    {
        panel1.SetActive(true);
        panel2.SetActive(false);
    }
}
