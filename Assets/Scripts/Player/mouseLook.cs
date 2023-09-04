using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Vector3 = System.Numerics.Vector3;

public class mouseLook : MonoBehaviour
{
    [Header ("Parameters")]
    [SerializeField] private float lookSpeed;
    [SerializeField] private Vector2 rot;
    [SerializeField] private Vector2 xClamp;
    [SerializeField] private float lookAtDistance;
    [SerializeField] private float sharpness = 10f;
    [SerializeField] private AudioClip notePickup;
    [SerializeField] private AudioClip notePutDown;
    
      [Header ("References")]
    [FormerlySerializedAs("collider")] [SerializeField] private Transform _col;
    [FormerlySerializedAs("camera")] [SerializeField] private Transform _cam;
    [SerializeField] private Vector2 input;
    [SerializeField] private TextMeshProUGUI useText;
    [SerializeField] private characterMotor motor;
    [SerializeField] private interactionTriggers triggers;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private dialogConversation noteConvo;
    [SerializeField] private GameObject audioPrefab;

    [Header("Data")] 
    [SerializeField] private bool paused;
    [SerializeField] private bool lockMouse;
    [SerializeField] private GameObject lookAtObject;
    [SerializeField] private LayerMask lookAtMask;
    [SerializeField] private bool cursorLocked = true;
    [SerializeField] private bool lookingAtNote = false;
    [SerializeField] private bool lookedAtNote;

    // Start is called before the first frame update
    void Start()
    {
        rot = new Vector2(0, _col.rotation.eulerAngles.y);
        motor = GetComponent<characterMotor>();
        triggers = GetComponent<interactionTriggers>();
        lockCursor();
        rb = GetComponent<Rigidbody>();
        
        
        dialogManager.dialogEndedEvent += unPauseDialog;
        pause();
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
                    if (!lookedAtNote)
                    {
                        lookedAtNote = true;
                        GameObject.FindObjectOfType<dialogManager>().startDialog(noteConvo);
                    }
                    spawnSFX(notePutDown);
                    lookingAtNote = false;
                    rb.isKinematic = false;
                    GetComponent<CharacterController>().enabled = true;
                    unPause();
                    motor.unPause();
                    lookAtObject.GetComponent<noteController>().returnNote();
                }
                else
                {
                    spawnSFX(notePickup);
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
        input = new Vector2(exponentialLerp(input.x,Input.GetAxis("Mouse X")), exponentialLerp(input.y,Input.GetAxis("Mouse Y")));
    }

    float yRot()
    {
        return rot.y + calculateRot(input.x);
    }

    float xRot()
    {
        return Mathf.Clamp(rot.x + calculateRot(input.y), xClamp.x, xClamp.y);
    }

    void getRotation()
    {
        rot.x = xRot();
        rot.y = yRot();
        _cam.localRotation = Quaternion.Euler(rot.x, 0, 0);
        _col.localRotation = Quaternion.Euler(0, rot.y, 0);
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
        if (Physics.Raycast(_cam.position, _cam.forward,out RaycastHit hit, lookAtDistance, lookAtMask))
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
    
    public void unPauseDialog()
    {
        dialogManager.dialogEndedEvent -= unPauseDialog;
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

    void spawnSFX(AudioClip soundToPlay)
    {
        GameObject G = Instantiate(audioPrefab);
        AudioSource a=G.GetComponent<AudioSource>();
        autoDelete d = G.GetComponent<autoDelete>();
        a.clip =soundToPlay;
        a.Play();
        d.setup(a.clip.length);
    }
}


