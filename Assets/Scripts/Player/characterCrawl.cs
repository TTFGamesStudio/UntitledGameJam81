using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;
using Spline = UnityEngine.Rendering.PostProcessing.Spline;

public class characterCrawl : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private characterMotor motor;
    [SerializeField] private mouseLook mouse;
    [SerializeField] private SplinePositioner spline;
    [SerializeField] private CharacterController controller;
    [SerializeField] private SplineComputer splineToFollow;
    [SerializeField] private CapsuleCollider rb;
    [SerializeField] private Image fillLeft;
    [SerializeField] private Image fillRight;
    [SerializeField] private AudioSource dragSound;

    [Header("Crawling")] 
    [SerializeField] private bool isCrawling;
    [SerializeField] private float progress=0;
    [SerializeField] private float progressPerPull;
    [SerializeField] private float progressVelocity;

    [Header("CrawlData")] 
    [SerializeField] private float RightStamina=1;
    [SerializeField] private float leftStamina=1;
    [SerializeField] private float staminaRefillSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<characterMotor>();
        mouse = GetComponent<mouseLook>();
        spline = GameObject.FindObjectOfType<SplinePositioner>();
        controller = GetComponent<CharacterController>();
        rb = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCrawling)
        {
            spline.SetPercent(progress);
            if (Input.GetKeyUp(KeyCode.Q) && leftStamina >= 0.5f)
            {
                progressVelocity += progressPerPull * leftStamina;
                leftStamina = 0;
                staminaRefillSpeed *= .99f;
                dragSound.pitch = Random.Range(0.9f, 1.1f);
                dragSound.Play();
            }
            
            if (Input.GetKeyUp(KeyCode.E) && RightStamina >= 0.5f)
            {
                progressVelocity += progressPerPull * RightStamina;
                RightStamina = 0;
                staminaRefillSpeed *= .99f;
                dragSound.pitch = Random.Range(0.9f, 1.1f);
                dragSound.Play();
            }

            fillLeft.fillAmount = leftStamina;
            fillRight.fillAmount = RightStamina;

            if (leftStamina < 1)
            {
                leftStamina += Time.deltaTime * staminaRefillSpeed;
            }
            else
            {
                leftStamina = 1;
            }

            if (RightStamina < 1)
            {
                RightStamina += Time.deltaTime * staminaRefillSpeed;
            }
            else
            {
                RightStamina = 1;
            }

            progress += progressVelocity;
            progressVelocity = Mathf.Lerp(progressVelocity, 0, Time.deltaTime * 10);
            if (progressVelocity <= 0)
            {
                progressVelocity = 0;
            }
        }
    }

    [Button]
    public void startCrawling()
    {
        fillLeft.color = new Color(1, 1, 1, 1);
        fillRight.color = new Color(1, 1, 1, 1);
        spline.spline = splineToFollow;
        motor.pause();
        mouse.pause();
        spline.enabled = true;
        controller.enabled = false;
        isCrawling = true;
        rb.height = 0.5f;

    }

    [Button]
    public void stopCrawling()
    {
        
        fillLeft.color = new Color(1, 1, 1, 0);
        fillRight.color = new Color(1, 1, 1, 0);
        spline.spline = null;
        motor.unPause();
        mouse.unPause();
        spline.enabled = false;
        controller.enabled = true;
        isCrawling = false;
        rb.height = 0.2f;
    }
}
