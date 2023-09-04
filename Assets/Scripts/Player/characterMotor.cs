using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMotor : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform facing;
    [SerializeField] private Rigidbody rb;

    [Header("Parameters")] 
    [SerializeField] private float walkSpeed;
    [SerializeField] private Vector2 input;
    [SerializeField] private float gravity;
    [SerializeField] private float sharpness = 15;

    [Header("Data")] 
    [SerializeField] private bool paused;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        dialogManager.dialogEndedEvent += unPauseDialog;
        pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            updateInput();
            controller.Move(getMoveVector());
        }
    }

    void updateInput()
    {
        input = exponentialLerp(input,new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }

    Vector3 getMoveVector()
    {
        //in order:
            //forward back
            //strafing
            //gravity
        Vector3 move =
            (facing.forward * walkSpeed * Time.deltaTime * input.y) +
            (facing.right * walkSpeed * Time.deltaTime * input.x) +
            new Vector3(0, gravity, 0);
        return move;
    }
    
    public void pause()
    {
        paused = true;
        controller.enabled = false;
        rb.isKinematic = true;
    }

    public void unPause()
    {
        paused = false;
    }

    public void unPauseDialog()
    {
        dialogManager.dialogEndedEvent -= unPauseDialog;
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        rb.isKinematic = false;
        controller.enabled = true;
        unPause();
    }
    
    float exponentialLerp(float a, float b)
    {

        return Mathf.Lerp(a, b, 1f - Mathf.Exp(-sharpness * Time.deltaTime));
    }

    Vector3 exponentialLerp(Vector3 a, Vector3 b)
    {
        return Vector3.Lerp(a,b,1f - Mathf.Exp(-sharpness * Time.deltaTime));
    }
    
    Vector2 exponentialLerp(Vector2 a, Vector2 b)
    {
        return Vector2.Lerp(a,b,1f - Mathf.Exp(-sharpness * Time.deltaTime));
    }
}
