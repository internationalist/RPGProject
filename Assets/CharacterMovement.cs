using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour {
    Animator anim;
    private Transform mainCamera;
    public Slider staminaSlider;
    public int maxStamina;
    public int fallMultiplier;
    public int regainMultiplier;
    public int minStaminaToRun;
    private bool jumping = false;

    private int fallRate;
    private int regainRate;
    private bool canRun=true;

    public float turnSpeed = 1.0f;
    private float turnEndAngle = 0;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        fallRate = 1;
        regainRate = 1;
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //only allow to run once stamina has built up.
        AllowRunWhenStaminaFill();
        //Set character to start sprinting if left shift is pressed while moving, if stamina allows. (Check animator).
        HandleStaminaWhenRunning();
        if (Input.GetAxis("Vertical") > 0)
        {
            //Change direction of movement to match the direction camera is facing when user presses move key.
            TurnToCameraDirection();
        }
        //start movement of character
        StartMovement();

        //For rigidbody vwithout gravity make sure character not floating
        GroundCharacterWhenNotJumping();
    }

    private void HandleStaminaWhenRunning()
    {
        if (Input.GetKey(KeyCode.LeftShift) && canRun)
        {
            anim.SetBool("sprinting", true);
            //reduce the stamina while sprinting
            HandleStaminaLoss();
        }
        else
        {
            anim.SetBool("sprinting", false);
            //Restore the stamina while walking
            HandleStaminaGain();
        }
    }

    private void StartMovement()
    {
        anim.SetFloat("movement", Input.GetAxis("Vertical"));
    }

    private void TurnToCameraDirection()
    {
        bool turnComplete = Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, mainCamera.eulerAngles.y)) < 1;
        if (!turnComplete)
        {
            if(turnEndAngle == 0)
            {
                turnEndAngle = Mathf.DeltaAngle(transform.eulerAngles.y, mainCamera.eulerAngles.y);
            }
            transform.eulerAngles += new Vector3(0, Mathf.Lerp(0, Mathf.DeltaAngle(transform.eulerAngles.y, mainCamera.eulerAngles.y), Time.deltaTime * turnSpeed), 0);
        } else
        {
            turnEndAngle = 0;
        }
    }

    private void AllowRunWhenStaminaFill()
    {
        if (staminaSlider.value > minStaminaToRun)
        {
            canRun = true;
        }
    }

    private void GroundCharacterWhenNotJumping()
    {
        if (!jumping)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

    private void HandleStaminaGain()
    {
        if (staminaSlider.value < maxStamina)
        {
            staminaSlider.value += Time.deltaTime / regainRate * regainMultiplier;
        }
        else
        {
            staminaSlider.value = maxStamina;
        }
    }

    private void HandleStaminaLoss()
    {
        if (staminaSlider.value > 0)
        {
            staminaSlider.value -= Time.deltaTime / fallRate * fallMultiplier;
        }
        else
        {
            //once stamin becomes zero, prevent run anymore.
            staminaSlider.value = 0;
            canRun = false;
        }
    }
}
