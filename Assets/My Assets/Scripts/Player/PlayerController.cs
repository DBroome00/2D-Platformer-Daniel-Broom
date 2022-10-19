using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header ("Basic Movement")]
    private float activeMoveSpeed;
    public float speed;
    public float jumpForce;
    public float moveInput;

    private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    private bool facingRight = true;

    [Header("Multi Jump")]
    //MultiJump
    [SerializeField] public static bool canDoubleJump;
    private int extraJumps;
    public int extraJumpsValue;

    [Header("Ground Check")]
    //ground
    public bool isGrounded;
    public Transform groundCheck;
    public float checkGroundRadius;
    public LayerMask whatIsGround;

    [Header("Wall Check")]
    //wall
    [SerializeField] public static bool canWallSlide;
    [SerializeField] public static bool canWallJump;
    public bool isTouchingFront;
    public Transform frontcheck;
    bool wallSliding;
    public float checkWallRadius;
    public float wallSlidingSpeed;
    public LayerMask whatIsWall;

    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    [Header("Dash")]
    //Dash
    public float dashSpeed;
    public float dashLength = .5f, dashCoolDown = 1f;
    private float dashCounter;
    private float dashCoolCounter;
    [SerializeField] private bool Dashing;

void Start()
    {
        activeMoveSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        extraJumps = extraJumpsValue;
        anim = GetComponent<Animator>();
        canWallJump = false;

    }


    void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkGroundRadius, whatIsGround);

        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * activeMoveSpeed, rb.velocity.y);

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }

        //anim parameters
        anim.SetBool("Moving", moveInput != 0);
        anim.SetBool("Grounded", isGrounded);
        anim.SetBool("OnWall", wallSliding);
        anim.SetBool("Dashing", Dashing);
        

    }


    void Update()
    {

        //Ground
        if (isGrounded == true && canDoubleJump == true)
        {
            extraJumps = 1; 
        }

        if (Input.GetKeyDown(KeyCode.W) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.W) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }






        //Wall
        isTouchingFront = Physics2D.OverlapCircle(frontcheck.position, checkWallRadius, whatIsWall);

        if (isTouchingFront == true && isGrounded == false && moveInput !=0)
        {
            wallSliding = true;
            
        }
        else
        {
            wallSliding = false;
        }
        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        if (Input.GetKeyDown(KeyCode.W) && wallSliding == true && canWallJump == true)
        { 
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }
        if (wallJumping == true)
        {
            rb.velocity = new Vector2(xWallForce * moveInput, yWallForce);
        }



        //Dash
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                Dashing = true;
            }
        }

        if(dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

                if(dashCounter <= 0)
                {
                    activeMoveSpeed = speed;
                    dashCoolCounter = dashCoolDown;
                Dashing = false;
                }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
        
    }

    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }
    
    //sprite direction
    void Flip()
    {
        facingRight = !facingRight;
        transform.rotation = moveInput < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
    }


    //Power Enabler
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DoubleJumpItem"))
        {
            canDoubleJump = true;
        }
        if (collision.CompareTag("WallJumpItem"))
        {
            canWallJump = true;
        }
        if (collision.CompareTag("WallSlideItem"))
        {
            canWallSlide = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            GetComponent<Health>().TakeDamage(1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(frontcheck.position, checkWallRadius);
    }
}
