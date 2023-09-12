using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InvertYButton : MonoBehaviour
{
    public settings playerSettings;
    // Start is called before the first frame update
    void Start()
    {
        playerSettings = GameObject.FindObjectOfType<settings>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InvertButton()
    {
        Debug.Log("buttonClicked");
        if (playerSettings.invertY)
            playerSettings.invertY = false;
        else
        {
            playerSettings.invertY = true;
        }
    }

    public void toTitle()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
