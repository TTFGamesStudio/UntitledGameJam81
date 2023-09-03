using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playConversation : MonoBehaviour
{
    public dialogConversation convo;

    public dialogConversation startConvo;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindObjectOfType<dialogManager>().startDialog(startConvo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play()
    {
        Debug.Log("player called");
        GameObject.FindObjectOfType<dialogManager>().startDialog(convo);
    }
}
