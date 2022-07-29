using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables for player movement and FlipPlayer() functionality
    public float speed;
    public float jumpForce;
    private float moveInput;
    private bool facingRight = true;

    //public variables for jump mechanic and ground checks
    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpTime;

    //private variables for jump mechanic and ground checks
    private bool isJumping;
    private float jumpTimeCounter;

    //components
    private Rigidbody2D rBody;
    private GameObject player;
    private Animator playerAnimator;
    

    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerAnimator = player.GetComponent<Animator>();
    }
    //physics
    private void FixedUpdate()
    {
        #region PlayerMovement
        //move player
        moveInput = Input.GetAxis("Horizontal");
        rBody.velocity = new Vector2(moveInput * speed, rBody.velocity.y);
        //flip player when switching directions
        if (facingRight == false && moveInput > 0)
        {
            FlipPlayer();
        }
        else if (facingRight == true && moveInput < 0)
        {
            FlipPlayer();
        }
        
        #endregion
    }
    //Animation
    void Update()
    {
        #region JumpCode
        //check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        //jump - check if space pressed - check if grounded
        
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            jumpTimeCounter = jumpTime;
            isJumping = true;
            rBody.velocity = Vector2.up * jumpForce;
        }
        if (Input.GetKey(KeyCode.Space) && isJumping == true) 
        {
            if (jumpTimeCounter > 0)
            {
                rBody.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
                playerAnimator.SetBool("IsJumping", true);
            }
            else
            {
                isJumping = false;
                playerAnimator.SetBool("IsJumping", false);
            }
        }
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            isJumping = false;
        }
        #endregion
        #region AnimationControl
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            playerAnimator.SetBool("IsRunning", true);
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            playerAnimator.SetBool("IsRunning", false);
        }
        #endregion
    }
    //Flips Player character on the X axis to the negative complement of its scale.
    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
