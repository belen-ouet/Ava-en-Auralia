using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KenStateMachine_2 : MonoBehaviour
{
    public float walkSpeed; // player left right walk speed
    public float jumpForce = 10f;
    bool isGrounded = true;
    
    Animator animator;
    Rigidbody2D rigidBody2D;
    Transform transform;

    //animation states - the values in the animator conditions
    enum KenStates {OCIOSO, CAMINAR, AGACHARSE, SALTAR, HADOOKEN, MAX_KEN_STATES};
                    //0,    1,       2,         3,      4,        5
    const string STATE_OCIOSO_NAME = "Ken Ocioso";
    enum Directions { LEFT, RIGHT, MAX_DIR };
                    //0,    1,     2
    Directions currentDirection = Directions.LEFT;
    KenStates currentAnimationState = KenStates.OCIOSO;
    bool jumping = false;

    void Start()
    {
        
        //define the animator attached to the player
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();

        Physics2D.queriesStartInColliders = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        switch(currentAnimationState)
        { 
            case KenStates.OCIOSO:
            //Check for keyboard input
                if (Input.GetMouseButtonDown(0)){
                    changeState(KenStates.HADOOKEN);

                }else if (!checkJump()){
                
                   if (0.0f != Input.GetAxis("Horizontal")){
                        changeState(KenStates.CAMINAR);
                   }
                   
                   
                   if(Input.GetAxis("Jump") > 0 && isGrounded == true){
                        rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, 0);
                        rigidBody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

                        isGrounded = false;
                        animator.SetBool("IsGrounded", true);

                    }
                    
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
                     
                }

                break;

            case KenStates.CAMINAR:
                checkJump();
                break;
        }

        checkDir();
        if (!Input.anyKey){
            changeState(KenStates.OCIOSO);
        }

        bool checkJump() {
            //Check for keyboard input
            if (Input.GetKey(KeyCode.Space) && !jumping) {
                //simple jump code using unity physics
                rigidBody2D.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
                changeState(KenStates.SALTAR);
                jumping = true;
                return true;
            }
            /*else if (Input.GetKey(KeyCode.S)) {
                changeState(KenStates.AGACHARSE);
                return true;
            }*/
            return false;
        }


        
    }


    void checkDir () {
        //All states can change its direction any time
        Rigidbody rb = GetComponent<Rigidbody>();
        if (Input.GetKey(KeyCode.D)) {
            changeDirection(Directions.RIGHT);
            transform.Translate(Vector3.left * walkSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A)) {
            changeDirection(Directions.LEFT);
            transform.Translate(Vector3.left * walkSpeed * Time.deltaTime);
        }
    }

    //--------------------------------------
    // Change the player’s animation state
    //--------------------------------------

    void changeState(KenStates state){
        if (currentAnimationState != state)
        {
            animator.SetInteger("DestinationState", (int)state);
            currentAnimationState = state;
        }
    }

    bool isPlaying(KenStates state) { return state == currentAnimationState; }

    void changeDirection(Directions direction){
        if (currentDirection != direction){
            currentDirection = direction;
            switch (direction) {
                case Directions.RIGHT:
                    transform.Rotate(0, 180, 0);
                    break;
                case Directions.LEFT:
                    transform.Rotate(0, -180, 0);
                    break;
            }
        }
    } //End changeDirection method
}
