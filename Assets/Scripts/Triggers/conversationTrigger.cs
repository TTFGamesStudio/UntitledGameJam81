using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conversationTrigger : MonoBehaviour
{
    public dialogConversation convo;
    public bool pause;
    public bool destroyAfter;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void play()
    {
        dialogManager.instance.startDialog(convo);
        if (destroyAfter)
        {
            Destroy(gameObject);
        }
    }
}
