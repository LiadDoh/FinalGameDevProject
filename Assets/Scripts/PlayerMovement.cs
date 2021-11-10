using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    private float walkSpeed = 5f;
    private float crouchSpeed = 3f;
    private float runSpeed = 15f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public float jumpHeight = 1f;
    Camera cam;

    public LayerMask groundMask;
    bool isGrounded;
    bool isStanding;
    Vector3 velocity;
    private float originalHeight;

    private AudioSource footStepSound;
    // Start is called before the first frame update
    void Start()
    {
        speed = walkSpeed;
        cam = Camera.main;
        originalHeight = controller.height;
        footStepSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = transform.position;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftControl) && isGrounded)
        {
            speed = crouchSpeed;
            controller.height = 0.5f;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            speed = runSpeed;
            controller.height = originalHeight;
            if (isGrounded && Input.GetAxis("Vertical") != 0 && (Math.Abs(temp.x - transform.position.x) + Math.Abs(temp.z - transform.position.z) > 0.1
                || Math.Abs(temp.x - transform.position.x) + Math.Abs(temp.z - transform.position.z) < -0.1)) // Walking audio
            {
                if (footStepSound.isPlaying == false)
                {
                    footStepSound.Play();
                }
            }
            else if (!(Input.GetAxis("Vertical") < 0)) // Stop walking audio
                footStepSound.Stop();
        }
        else
        {
            speed = walkSpeed;
            controller.height = originalHeight;
        }

    }

}
