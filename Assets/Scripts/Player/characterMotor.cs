using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMotor : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform facing;

    [Header("Parameters")] 
    [SerializeField] private float walkSpeed;
    [SerializeField] private Vector2 input;
    [SerializeField] private float gravity;

    [Header("Data")] 
    [SerializeField] private bool paused;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
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
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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
    }

    public void unPause()
    {
        paused = false;
    }
}
