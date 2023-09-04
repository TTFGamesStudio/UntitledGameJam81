using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterCrawlTrigger : conversationTrigger
{
    public AudioSource crawlSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void play()
    {
        Debug.Log("try");
        crawlSound.Play();
        dialogManager.instance.startDialog(convo);
    }
}
