using UnityEngine;

/*
 * 1. script to direct the camara to move (HINT make the camara move over to the corners of am imaginary cube over the main maze cube)
 * 2. move the camera to the next position by using the direction of movement of the player and the current position of the camera  
 */ 

public class playerController : MonoBehaviour {

    //all the variables used to controll the external scripts 
    public bool playerCameraIsRotating = false;
    float MazeSize = 4; //store the size of the maze (the dimention of the maze)
    /* TODO : make the script get the size of the maze from code
     */
    private float MazeOffset;

    //components for the player rotaration (the ground trigger items)
    //all the colliders are set to isTrigger
    public GameObject rightCollider;
    public GameObject leftCollider;
    public GameObject forwardCollider;
    public GameObject backCollider;

    ColliderScript rightcol;
    ColliderScript leftcol;
    ColliderScript forwardcol;
    ColliderScript backcol;

    bool canMoveRight;
    bool canMoveLeft;
    bool canMoveForward;
    bool canMoveBack;

    float stepOffSet = 1f;   //the distance between each step (start and the end)

    private float playerMeshSize = 0.25f;

    bool trigFlag;
    bool rotateFlag;

    enum Direction  //direction of the player movement
    {
        Null,
        Right,
        Left,
        Forward,
        Back
    };
    //all the variables to controll the player 
    private float playerSpeed = 0.01f;   //the speed of the player also manipulates the animation speed of the player 
    private bool atEdge;    //to check if the player reached the edge of the maze(to trigger the camera movement and the player rotation)
    private Direction movementDireciton;

    Vector3 direction;

    Vector3 localRight;
    Vector3 localLeft;
    Vector3 localForward;
    Vector3 localBack;
    Vector3 localDown;

    Vector3 destination = new Vector3();
    bool destinationFlag = false;
    Vector3 preDestination = new Vector3();


    private void Start () {
        
        rightcol = rightCollider.GetComponent<ColliderScript>();
        leftcol = leftCollider.GetComponent<ColliderScript>();
        forwardcol = forwardCollider.GetComponent<ColliderScript>();
        backcol = backCollider.GetComponent<ColliderScript>();

        trigFlag = false;
        rotateFlag = false;
        atEdge = false;
        destinationFlag = true;
        destination = transform.localPosition;
        preDestination = transform.localPosition;
        MazeOffset = MazeSize/2 - MazeSize/8 + playerMeshSize * 3 / 2;
        Debug.Log(MazeOffset);
    }
	
	private void FixedUpdate ()
    {
        WallCollisionCheck();
        Debug.Log("CanMoveRight  : " + canMoveRight);
        Debug.Log("CanMoveLeft  : " + canMoveLeft);
        Debug.Log("CanMoveForward  : " + canMoveForward);
        Debug.Log("CanMoveBack  : " + canMoveBack);

        localForward = transform.parent.InverseTransformDirection(transform.forward);
        localRight = transform.parent.InverseTransformDirection(transform.right);
        localLeft = localRight * -1;
        localBack = localForward * -1;
        localDown = transform.parent.InverseTransformDirection(transform.up) * -1;

        if (!atEdge)
        {
            Move();
        }
        if(atEdge && !rotateFlag)
        {
            rotateFlag = true;
            ChangePlane();
            RotateCamera();
        }

    }

    void Move() //controls the movement of the player 
    {
        Debug.Log(destinationFlag);
        if (Input.GetAxis("Horizontal") > 0)
        {
            if (canMoveRight)
            {
                if (!destinationFlag)
                {
                    if (movementDireciton == Direction.Left)
                    {
                        destination = preDestination;
                    }
                }
                else
                {
                    destinationFlag = false;
                    movementDireciton = Direction.Right;
                    preDestination = transform.localPosition;
                    destination = transform.localPosition + localRight * stepOffSet;
                }
            }
        }
        else if(Input.GetAxis("Horizontal") < 0) 
        {
            if (canMoveLeft)
            {
                if (!destinationFlag)
                {
                    if (movementDireciton == Direction.Right)
                    {
                        destinationFlag = false;
                        destination = preDestination;
                    }
                }
                else
                {
                    destinationFlag = false;
                    movementDireciton = Direction.Left;
                    preDestination = transform.localPosition;
                    destination = transform.localPosition + localLeft * stepOffSet;
                }
            }
        }
        else if(Input.GetAxis("Vertical") > 0)
        {
            if (canMoveForward)
            {
                if (!destinationFlag)
                {
                    if (movementDireciton == Direction.Back)
                    {
                        destinationFlag = false;
                        destination = preDestination;
                    }
                }
                else
                {
                    destinationFlag = false;
                    movementDireciton = Direction.Forward;
                    preDestination = transform.localPosition;
                    destination = transform.localPosition + localForward * stepOffSet;
                }
            }
        }
        else if(Input.GetAxis("Vertical") < 0)
        {
            if (canMoveBack)
            {
                if (!destinationFlag)
                {
                    if (movementDireciton == Direction.Forward)
                    {
                        destinationFlag = false;
                        destination = preDestination;
                    }
                }
                else
                {
                    destinationFlag = false;
                    movementDireciton = Direction.Back;
                    preDestination = transform.localPosition;
                    destination = transform.localPosition + localBack * stepOffSet;
                }
            }
        }
        if(transform.localPosition == destination)
        {
            preDestination = destination;
            movementDireciton = Direction.Null;
            destinationFlag = true;
        }
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, playerSpeed);
    }
    void WallCollisionCheck()
    {
        if(rightcol.hit)
        {
            canMoveRight = false;
        }
        else
        {
            canMoveRight = true;
        }

        if (leftcol.hit)
        {
            canMoveLeft = false;
        }
        else
        {
            canMoveLeft = true;
        }

        if (forwardcol.hit)
        {
            canMoveForward = false;
        }
        else
        {
            canMoveForward = true;
        }

        if (backcol.hit)
        {
            canMoveBack = false;
        }
        else
        {
            canMoveBack = true;
        }

    }
    void ChangePlane()
    {
        Debug.Log("Changing plane");
    }
    void RotateCamera()
    {

    }
}
