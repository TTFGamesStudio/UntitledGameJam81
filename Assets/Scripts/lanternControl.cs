using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class lanternControl : MonoBehaviour
{
    public Transform player;
    public float maxDistanceToPlayer;
    public Light l;
    // Start is called before the first frame update
    void Start()
    {
            l=GetComponentInChildren<Light>();
            player = GameObject.FindGameObjectWithTag("player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < maxDistanceToPlayer)
        {
            l.enabled = true;
        }
        else
        {
            l.enabled = false;
        }
    }
}
