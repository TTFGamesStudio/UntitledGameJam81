using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;

public class noteController : MonoBehaviour
{
    private Vector3 startPoisition;
    private Quaternion startRotation;

    [SerializeField] private Transform endPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        startPoisition = transform.position;
        startRotation = transform.rotation;
        endPosition = GameObject.Find("notePosition").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startNote()
    {
        transform.position = endPosition.position;
        transform.rotation = endPosition.rotation;
    }

    public void returnNote()
    {
        transform.position = startPoisition;
        transform.rotation = startRotation;
    }
}
