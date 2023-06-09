using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public Collider2D standingCollider;
    public Transform groundCheckCollider;
    public LayerMask groundLayer;

    const float groundCheckRadius = 0.2f;
    [SerializeField] float speed = 2;
    [SerializeField] float jumpPower =500;
    public int totalJumps;
    int availableJumps;
    float horizontalValue;
    
    bool isGrounded=true;
    bool facingRight = true;
    bool multipleJump;
    bool coyoteJump;
    bool isDead = false;

    void Awake()
    {
        availableJumps = totalJumps;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Store the horizontal value
        horizontalValue = Input.GetAxisRaw("Horizontal");

        //If we press Jump button enable jump 
        if (Input.GetButtonDown("Jump"))
            Jump();
         //If we press LClick button enable jump 
        if (Input.GetButtonDown("Fire1"))
            StartCoroutine(Attack());
         //If we press RClick button enable jump 
        if (Input.GetButtonDown("Fire2"))
        StartCoroutine(Swing());
        //Set the yVelocity Value
        animator.SetFloat("yVelocity", rb.velocity.y);

        //If you fall
         Vector3 scale = transform.localScale;
        if (transform.position.y < -10)
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        GroundCheck();
        Move(horizontalValue);        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheckCollider.position, groundCheckRadius);
        Gizmos.color = Color.red;
    }

    void GroundCheck()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;
        //Check if the GroundCheckObject is colliding with other
        //2D Colliders that are in the "Ground" Layer
        //If yes (isGrounded true) else (isGrounded false)
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
            if (!wasGrounded)
            {
                availableJumps = totalJumps;
                multipleJump = false;
            }        
        }    
        else
        {
            if (wasGrounded)
                StartCoroutine(CoyoteJumpDelay());
        }

        //As long as we are grounded the "Jump" bool
        //in the animator is disabled
        animator.SetBool("Jump", !isGrounded);
    }

    #region Jump
    IEnumerator CoyoteJumpDelay()
    {
        coyoteJump = true;
        yield return new WaitForSeconds(0.2f);
        coyoteJump = false;
    }

    void Jump()
    {
        if (isGrounded)
        {
            multipleJump = true;
            availableJumps--;

            rb.velocity = Vector2.up * jumpPower;
            animator.SetBool("Jump", true);
        }
        else
        {
            if(coyoteJump)
            {
                multipleJump = true;
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }

            if(multipleJump && availableJumps>0)
            {
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }
        }
    }
    #endregion

    void Move(float dir)
    {      
        #region Move
        //Set value of x using dir and speed
        float xVal = dir * speed * 100 * Time.fixedDeltaTime;

        //Create Vec2 for the velocity
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        //Set the player's velocity
        rb.velocity = targetVelocity;
 
        //If looking right and clicked left (flip to the left)
        if(facingRight && dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
        //If looking left and clicked right (flip to rhe right)
        else if(!facingRight && dir > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }

        //(0 idle , 4 walking)
        //Set the float xVelocity according to the x value 
        //of the RigidBody2D velocity 
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        #endregion
    }  

   IEnumerator Attack(){
        animator.SetBool("Attack", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Attack", false);
    }

    IEnumerator Swing(){
        animator.SetBool("Swing", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Swing", false);
    }
    
    public void Die()
    {
        isDead = true;
        FindObjectOfType<LevelManager>().Restart();
    }

    public void ResetPlayer()
    {
        isDead = false;
    }
    
}
