using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDialogs : MonoBehaviour
{
    public dialogManager manager;
    public dialogConversation shadowDialog;
    public dialogConversation crawl0;
    
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindObjectOfType<dialogManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void monsterShadowDialog()
    {
        manager.startDialog(shadowDialog);
    }

    public void startCrawling()
    {
        manager.startDialog(crawl0);
    }
}
