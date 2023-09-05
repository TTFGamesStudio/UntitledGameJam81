using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterCrawlTrigger : conversationTrigger
{
    public AudioSource crawlSound;
    public dialogManager m;

    
    
    public override void play()
    {
        crawlSound.Play();
        m.startDialog(convo);
    }
}
