using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settings : MonoBehaviour
{
    public bool invertY = true;
    public bool paused = false;

    private CharacterController controller;
    private mouseLook mouse;
    private characterMotor motor;
    private characterCrawl crawl;
    private Rigidbody rigid;

    public GameObject invertX;
    public GameObject pauseMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindObjectOfType<CharacterController>();
        mouse = GameObject.FindObjectOfType<mouseLook>();
        motor = GameObject.FindObjectOfType<characterMotor>();
        crawl = GameObject.FindObjectOfType<characterCrawl>();
        rigid = controller.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (paused)
            {
                paused = false;
                unPause();
            }
            else
            {
                paused = true;
                pause();
            }
        }

        if (paused)
        {
            invertX.SetActive(invertY);
        }
    }

    void pause()
    {
        controller.enabled = false;
        mouse.enabled = false;
        motor.enabled = false;
        crawl.enabled = false;
        rigid.isKinematic = true;
        pauseMenu.SetActive(true);
        mouse.unlockCursor();
    }

    void unPause()
    {
        controller.enabled = true;
        mouse.enabled = true;
        motor.enabled = true;
        crawl.enabled = true;
        rigid.isKinematic = false;
        pauseMenu.SetActive(false);
        mouse.lockCursor();
    }
}
