using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class interactionTriggers : MonoBehaviour
{
    [SerializeField] private bool inStartCrawlTrigger;
    [SerializeField] private bool startCrawlDone;
    [SerializeField] private TextMeshProUGUI interactText;

    [SerializeField] private PlayableDirector monsterBiteDirector;
    [SerializeField] private PlayableDirector monsterRunDirector;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inStartCrawlTrigger)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                GameObject.FindObjectOfType<characterCrawl>().startCrawling();
                startCrawlDone = true;
                inStartCrawlTrigger = false;
                interactText.text = "";
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "startCrawlTrigger" && !startCrawlDone)
        {
            interactText.text = "E to Enter the 'hole'";
            inStartCrawlTrigger = true;
        }
        
        if (other.tag == "stopCrawlTrigger")
        {
            GameObject.FindObjectOfType<characterCrawl>().stopCrawling();
        }

        if (other.tag == "monsterShadowTrigger")
        {
            Destroy(monsterBiteDirector.gameObject);
            Destroy(other.gameObject);
            monsterRunDirector.Play();
        }

        if (other.tag == "fallSoundTrigger")
        {
            GameObject.Find("fallingSound").GetComponent<AudioSource>().Play();
        }

        if (other.tag == "gameOverTrigger")
        {
            SceneManager.LoadScene(0);
        }

        if (other.tag == "rockslideTrigger")
        {
            GameObject.Find("Rockslide").GetComponent<PlayableDirector>().Play();
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "startCrawlTrigger")
        {
            interactText.text = "";
            inStartCrawlTrigger = false;
        }
    }
}
