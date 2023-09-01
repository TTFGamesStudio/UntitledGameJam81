using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class mouseLook : MonoBehaviour
{
    [Header ("Parameters")]
    [SerializeField] private float lookSpeed;
    [SerializeField] private Vector2 rot;
    [SerializeField] private Vector2 xClamp;
    [SerializeField] private float lookAtDistance;
    
    [Header ("References")]
    [SerializeField] private Transform collider;
    [SerializeField] private Transform camera;
    [SerializeField] private Vector2 input;
    [SerializeField] private TextMeshProUGUI useText;
    [SerializeField] private characterMotor motor;
    [SerializeField] private interactionTriggers triggers;
    [SerializeField] private Rigidbody rb;

    [Header("Data")] 
    [SerializeField] private bool paused;
    [SerializeField] private bool lockMouse;
    [SerializeField] private GameObject lookAtObject;
    [SerializeField] private LayerMask lookAtMask;
    [SerializeField] private bool cursorLocked = true;
    [SerializeField] private bool lookingAtNote = false;

    // Start is called before the first frame update
    void Start()
    {
        rot = new Vector2(0, collider.rotation.eulerAngles.y);
        motor = GetComponent<characterMotor>();
        triggers = GetComponent<interactionTriggers>();
        lockCursor();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            updateInput();
            getRotation();
            getLookAtObject();
        }
        else
        {
            useText.color = new Color(1, 1, 1,0);
        }
        //do interaction
        if (Input.GetKeyUp(KeyCode.E) && lookAtObject != null)
        {
            if (lookAtObject.tag == "note")
            {
                //the object is a note
                if (lookingAtNote)
                {
                    lookingAtNote = false;
                    rb.isKinematic = false;
                    unPause();
                    motor.unPause();
                    lookAtObject.GetComponent<noteController>().returnNote();
                }
                else
                {
                    lookingAtNote = true;
                    rb.isKinematic = true;
                    pause();
                    motor.pause();
                    lookAtObject.GetComponent<noteController>().startNote();
                }
            }
        }
    }
    
    void updateInput()
    {
        input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    float yRot()
    {
        return rot.y + calculateRot(input.x);
    }

    float xRot()
    {
        Debug.Log(rot.x + " | " + calculateRot(input.y) + " | " + Mathf.Clamp(rot.x + calculateRot(input.y), xClamp.x, xClamp.y) );
        return Mathf.Clamp(rot.x + calculateRot(input.y), xClamp.x, xClamp.y);
    }

    void getRotation()
    {
        rot.x = xRot();
        rot.y = yRot();
        camera.localRotation = Quaternion.Euler(rot.x, 0, 0);
        collider.localRotation = Quaternion.Euler(0, rot.y, 0);
    }
    
    float calculateRot(float input)
    {
        return input * Time.deltaTime * lookSpeed;
    }

    void cursorLock()
    {
        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void lockCursor()
    {
        cursorLocked = true;
        cursorLock();
    }

    public void unlockCursor()
    {
        cursorLocked = false;
        cursorLock();
    }

    public void pause()
    {
        paused = true;
    }

    public void unPause()
    {
        paused = false;
    }

    void getLookAtObject()
    {
        if (Physics.Raycast(camera.position, camera.forward,out RaycastHit hit, lookAtDistance, lookAtMask))
        {
            lookAtObject = hit.transform.gameObject;
        }
        else
        {
            lookAtObject = null;
        }

        if (lookAtObject != null && !lookingAtNote)
        {
            useText.color = new Color(1, 1, 1,1);
        }
        else
        {
            useText.color = new Color(1, 1, 1,0);
        }
    }

    void interactWithObject()
    {
        
    }
}
