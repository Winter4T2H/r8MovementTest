using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using UnityEngine;

/*
 * 
 * Just a Hello World here XD
 * Git is not working!!!
 * 
 * All script made by Winter4T2H.
 * 
 */

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -20f;
    public float jumpHeight = 3f;
    public bool secondJump;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float wallDistance = 0.1f;
    public Transform leftWCheck;
    public LayerMask leftWMask;
    public Transform rightWCheck;
    public LayerMask rightWMask;

    Vector3 velocity;
    bool isGrounded;
    bool rightWtouch;
    bool leftWtouch;
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Jump and double jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Thread th_FJump = new Thread(CharaterFirstJump);
            th_FJump.Start();
        }
        if (Input.GetButtonDown("Jump") && isGrounded != true && secondJump == false)
        {
            Thread th_SJump = new Thread(CharaterSecondJump);
            th_SJump.Start();
        }








        // Gravity function
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            secondJump = false; // Set secondJump to false
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        if(isGrounded == true)
        {
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
        }
        else
        {
            Vector3 move = transform.right * x / 3 + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
        }
        


        // Sprint
        if (Input.GetButton("Fire3"))
        {
            speed = 24f;
        }
        else
        {
            speed = 12f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }




    void WallChecks()
    {
        rightWtouch = Physics.CheckSphere(rightWCheck.position, wallDistance, rightWMask);
        leftWtouch = Physics.CheckSphere(leftWCheck.position, wallDistance, leftWMask);
    }
    void CharaterFirstJump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * 2f * -2f * gravity);
    }
    void CharaterSecondJump()
    {
        secondJump = true;
        velocity.y = Mathf.Sqrt(jumpHeight * 1.5f * -2f * gravity);
    }
}
