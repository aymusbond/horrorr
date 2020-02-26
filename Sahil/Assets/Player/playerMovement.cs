using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public CharacterController controller;

    public Light flashlight;

    public float currentSpeed;
    public float normalSpeed = 6f;
    public float sprintSpeed = 12f;
    public float crouchSpeed = 3f;

    public static bool isWalking;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public Transform headCheck;
    public float headDistance = 0.4f;
    public LayerMask headMask;

    Vector3 velocity;
    bool isGrounded;
    bool isHeadCovered;

    private void Start()
    {
        flashlight.enabled = false;
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.enabled = !flashlight.enabled;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isHeadCovered = Physics.CheckSphere(headCheck.position, headDistance, headMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -5f;
        }

        if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) 
            && !Input.GetKey(KeyCode.LeftControl))
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;


        if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
        {
            currentSpeed = sprintSpeed;
        }
        else if(isHeadCovered)
        {
            currentSpeed = crouchSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            currentSpeed = crouchSpeed;
        }
        else
        {
            currentSpeed = normalSpeed;
        }


        if (Input.GetKey(KeyCode.LeftControl))
        {
            controller.height = 1.5f;
        }
        else if(!isHeadCovered)
        {
            controller.height = 3.8f;
        }



        controller.Move(move * currentSpeed * Time.deltaTime);



        if(Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
