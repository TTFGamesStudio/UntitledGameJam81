using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("load",10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void load()
    {
        SceneManager.LoadScene(1);
    }
}

