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

    //Variables for jump mechanic and ground checks
    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    //components
    private Rigidbody2D rBody;
    private GameObject player;
    public Animator playerAnimator;
    

    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("BoxMan");
        playerAnimator = gameObject.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        #region PlayerMovement
        //move player
        moveInput = Input.GetAxis("Horizontal");
        Debug.Log(facingRight);
        rBody.velocity = new Vector2(moveInput * speed, rBody.velocity.y);
        //flip player when switching directions
        if (facingRight == false && moveInput > 0)
        {
            FlipPlayer();
        }
        else if(facingRight == true && moveInput < 0)
        {
            FlipPlayer();
        }
        #endregion


    }

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
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            isJumping = false;
        }
        #endregion
        #region AnimationControl
        if (Input.GetKeyDown(KeyCode.W))
        {
            playerAnimator.Play("Running");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            playerAnimator.Play("Running");
        }
        #endregion

    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector3 scaleVal = transform.localScale;
        scaleVal.x *= -1;
        transform.localScale = scaleVal;
    }
}
