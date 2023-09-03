using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoDelete : MonoBehaviour
{
    public bool autoDestroy = false;

    public float autoDestroyTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        if (autoDestroy)
        {
            Invoke("Des",autoDestroyTime);
        }
    }

    public void setup(float time)
    {
        Invoke("Des",time);
    }
    
    private void Des()
    {
        Destroy(gameObject);
    }
}
