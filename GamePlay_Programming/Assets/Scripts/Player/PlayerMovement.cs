using RPGCharacterAnims.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Animator animator;

    public float drag;

    public float jumpforce;
    public float jumpcooldown;
    public float multiplier;
    bool jumpready;

    [Header("KeyBinds")]
    public KeyCode jumpkey = KeyCode.Space;

    [Header("Checking for ground")]
    public float playerHeight;
    public LayerMask isGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private float x;
    private float y;
    public float sens = -1f;
    private Vector3 rotate;



    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        jumpready = true;

    }
    private void Update()
    {
        //Check for ground
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isGround);

        MyInput();
        SpeedController();

        if (grounded)
            rb.drag = drag;
        else
            rb.drag = 0;

        y = Input.GetAxis("Mouse X");
        x = Input.GetAxis("Mouse Y");
        rotate = new Vector3(x, y * sens, 0);
        transform.eulerAngles = transform.eulerAngles - rotate;
    }

    private void FixedUpdate()
    {
        Moveplayer();
    }
    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Jump check
        if (Input.GetKey(jumpkey) && jumpready && grounded)
        {
            animator.SetBool("IsJumping", true);
            jumpready = false;

            Jump();

            Invoke(nameof(JumpReset), jumpcooldown);
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }
    }

   
    

    void Moveplayer()
    {
        //movement Direction Calculation 
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air  
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * multiplier, ForceMode.Force);

        if(moveDirection != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    void SpeedController()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //Velocity limiter 
        if(flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedvelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedvelocity.x, rb.velocity.y, limitedvelocity.z);
        }
    }

    void Jump()
    {
        // y velocity reset
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpforce, ForceMode.Impulse);

    }

        void JumpReset()
        {
            jumpready = true;
        }
    }


    

