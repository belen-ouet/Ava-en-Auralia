using UnityEngine;
using System.Collections;

public class Ken : MonoBehaviour {
    //public float walkSpeed; // player left right walk speed
    //public float jumpForce;

    public float MaxInclination;

    [SerializeField]
    float walkSpeed; // player left right walk speed
    [SerializeField]
    float jumpForce;

    Animator animator;
    Rigidbody2D rigidBody2D;
    Transform transform;

    //animation states - the values in the animator conditions
    public enum KenStates { OCIOSO, CAMINAR, AGACHARSE, SALTAR, HADOOKEN, MAX_KEN_STATES };
                            //0,     1,          2,      3,          4,          5
    const string STATE_OCIOSO_NAME = "Ken Ocioso";
                     //0,     1,      2
    enum Directions { LEFT, RIGHT, MAX_DIR };
    Directions currentDirection = Directions.LEFT;
    public KenStates currentAnimationState = KenStates.OCIOSO;

    bool jumping = false;

    // Use this for initialization
    void Start()
    {
        //define the animator attached to the player
        animator    = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        transform   = GetComponent<Transform>();

        //Setting the default orientation of the device
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    //--------------------------------------
    // Change the players animation state
    //--------------------------------------
    void changeState(KenStates state) {

        if (currentAnimationState != state)
        {
            animator.SetInteger("DestinationState", (int)state);
            currentAnimationState = state;
        }
    }

    bool isPlaying(KenStates state) { return state == currentAnimationState; }

    // Update is called once per frame
    void Update() {
        switch (currentAnimationState)
        { case KenStates.OCIOSO:
                //Check for keyboard input
                if (Input.GetMouseButtonDown(0))
                    changeState(KenStates.HADOOKEN);
                else if (!checkJump())
                    if (0.0f != Input.GetAxis("Horizontal"))
                        changeState(KenStates.CAMINAR);
                break;
            //case KenStates.SALTAR:
            //case KenStates.AGACHARSE:
            //case KenStates.HADOOKEN:
            //    //Check for time outs state changes in Unity Animator
            //    //if (STATE_OCIOSO == animator.GetInteger("DestinationState"))
            //    //if (animator.GetCurrentAnimatorStateInfo(0).IsName(STATE_OCIOSO_NAME))
            //    //    changeState(STATE_OCIOSO);
            //    break;
            case KenStates.CAMINAR:
                //Check for time outs state changes in Unity Animator
                //if (animator.GetCurrentAnimatorStateInfo(0).IsName(STATE_OCIOSO_NAME))
                //    changeState(STATE_OCIOSO);
                break;
        }
        checkJump();
        checkDir();
        checkAngle();
        if (!Input.anyKey)
            changeState(KenStates.OCIOSO);
    }

//----------------
// Validates movement
//----------------
void checkDir ()
    {
        //All states can change its direction any time
        if (Input.GetKey(KeyCode.D)) {
            changeDirection(Directions.RIGHT);
            transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A)) {
            changeDirection(Directions.LEFT);
            transform.Translate(Vector3.left * walkSpeed * Time.deltaTime);
        }
    }

    //----------------
    // Validates movement
    //----------------
    bool checkJump()
    {
        //Check for keyboard input or accelerometers 
        if (!jumping)
        {
            if (Input.GetKey("up") || 0.1f <= Input.acceleration.z)
            {
                //simple jump code using unity physics
                rigidBody2D.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
                changeState(KenStates.SALTAR);
                jumping = true;
                return true;
            }
            else if (Input.GetKey("down") || -0.1f >= Input.acceleration.z)
            {
                changeState(KenStates.AGACHARSE);
                return true;
            }
        }
        return false;
    }

    //--------------------------------------
    // Check if player has collided with the floor
    //--------------------------------------
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Suelo") {
            changeState(KenStates.OCIOSO);
            jumping = false;
        }
    }

    //--------------------------------------
    // Flip player sprite for left/right walking
    //--------------------------------------
    void changeDirection(Directions direction)
	{
		if (currentDirection != direction)
		{
			currentDirection = direction;
            switch (direction) {
                case Directions.RIGHT:
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    break;
                case Directions.LEFT:
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    break;
            }
        }
	}

    private void checkAngle()
    {
        float zAngle = transform.eulerAngles.z%360;

        if (zAngle < 180)
        {
            if (zAngle > MaxInclination)
                transform.localRotation = Quaternion.AngleAxis(MaxInclination - 1, Vector3.forward);
        }
        else
        {
            zAngle -= 360;
            if (zAngle < -MaxInclination)
                transform.localRotation = Quaternion.AngleAxis(MaxInclination - 1, Vector3.back);
        }
        Debug.Log("Angulo = " + zAngle);
    }
}