using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class titleLoader : MonoBehaviour
{
    public int gameIndex = 1;
    public int creditsIndex = 2;

    public PlayableDirector playTimeline;
    public PlayableDirector creditsTimeline;
    public PlayableDirector quitTimeline;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playGame()
    {
        SceneManager.LoadScene(gameIndex);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void loadCredits()
    {
        SceneManager.LoadScene(creditsIndex);
    }

    public void startPlay()
    {
        playTimeline.Play();
    }

    public void startQuit()
    {
        quitTimeline.Play();
    }

    public void startCredits()
    {
        creditsTimeline.Play();
    }
}
