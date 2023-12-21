using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToSingle : MonoBehaviour
{
    // Start is called before the first frame update
    public void backClick()
    {
        SceneManager.LoadScene("SampleScene");
        GameObject networkRunner = GameObject.Find("Network runner");
        Destroy(networkRunner);
    }
}
