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
    public GameObject Right;
    public GameObject Left;
    public GameObject Forward;
    public GameObject Back;

    RayCastScript rightRay;
    RayCastScript leftRay;
    RayCastScript forwardRay;
    RayCastScript backRay;

    public GameObject MazeBody;
    MazeBodyRotation mazeRotation;

    bool canMoveRight = false;
    bool canMoveLeft = false;
    bool canMoveForward = false;
    bool canMoveBack = false;

    bool isMoving = false;
    bool inJunction = true;

    float stepOffSet = 1f;   //the distance between each step (start and the end)
    float DownStep;

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
    Direction tempDirection;
    //all the variables to controll the player 
    private float playerSpeed = 0.05f;   //the speed of the player also manipulates the animation speed of the player 
    private bool atEdge;    //to check if the player reached the edge of the maze(to trigger the camera movement and the player rotation)
    private Direction movementDireciton;

    Vector3 direction;

    Vector3 localRight;
    Vector3 localLeft;
    Vector3 localForward;
    Vector3 localBack;
    Vector3 localDown;

    Vector3 RightRotation;
    Vector3 LeftRotation;
    Vector3 ForwardRotation;
    Vector3 BackRotation;

    Vector3 destination = new Vector3();
    bool destinationFlag = false;

    private void Start () {

        isMoving = false;

        rightRay = Right.GetComponent<RayCastScript>();
        leftRay = Left.GetComponent<RayCastScript>();
        forwardRay = Forward.GetComponent<RayCastScript>();
        backRay = Back.GetComponent<RayCastScript>();

        RightRotation = Vector3.back;
        LeftRotation = Vector3.forward;
        ForwardRotation = Vector3.right;
        BackRotation = Vector3.left;

        trigFlag = false;
        rotateFlag = false;
        atEdge = false;
        destinationFlag = true;
        destination = transform.localPosition;

        MazeOffset = MazeSize / 2;
        DownStep = MazeOffset / 4;
        mazeRotation = MazeBody.GetComponent<MazeBodyRotation>();

    }
	
	private void FixedUpdate ()
    {
        WallCollisionCheck();
        EdgeDetection();
        JunctionDetection();
        localForward = transform.parent.InverseTransformDirection(transform.forward);
        localRight = transform.parent.InverseTransformDirection(transform.right);
        localLeft = localRight * -1;
        localBack = localForward * -1;
        localDown = transform.parent.InverseTransformDirection(transform.up) * -1;

        if (!atEdge)
        {
            Move();
        }
        if(atEdge)
        {

            ChangePlane();
            Debug.Log("changing plane");
        }
               
    }
    void JunctionDetection()
    {
        if ((movementDireciton == Direction.Right || movementDireciton == Direction.Left) && (canMoveForward || canMoveBack))
        {
            inJunction = true;
        }
        else if ((movementDireciton == Direction.Forward || movementDireciton == Direction.Back) && (canMoveRight || canMoveLeft))
        {
            inJunction = true;
        }
        else if(movementDireciton == Direction.Right && !canMoveRight)
        {
            inJunction = true;
        }
        else if (movementDireciton == Direction.Left && !canMoveLeft)
        {
            inJunction = true;
        }
        else if (movementDireciton == Direction.Forward && !canMoveForward)
        {
            inJunction = true;
        }
        else if (movementDireciton == Direction.Back && !canMoveBack)
        {
            inJunction = true;
        }
        else
        {
            inJunction = false;
        }
    }
    void Move() //controls the movement of the player 
    {
        Debug.Log("tempDirection : " + tempDirection);
        if (Input.GetAxis("Horizontal") > 0 || movementDireciton == Direction.Right || tempDirection == Direction.Right)
        {
            if (canMoveRight)
            {
                tempDirection = Direction.Right;
                if (destinationFlag)
                {
                    isMoving = true;
                    destinationFlag = false;
                    movementDireciton = Direction.Right;
                    destination = transform.localPosition + localRight * stepOffSet;
                }
            }
        }
        if(Input.GetAxis("Horizontal") < 0 || movementDireciton == Direction.Left || tempDirection == Direction.Left)
        {
            if (canMoveLeft)
            {
                tempDirection = Direction.Left;
                if (destinationFlag)
                {
                    isMoving = true;
                    destinationFlag = false;
                    movementDireciton = Direction.Left;
                    destination = transform.localPosition + localLeft * stepOffSet;
                }
            }
        }
        if(Input.GetAxis("Vertical") > 0 || movementDireciton == Direction.Forward || tempDirection == Direction.Forward)
        {
            if (canMoveForward)
            {
                tempDirection = Direction.Forward;
                if (destinationFlag)
                {
                    isMoving = true;
                    destinationFlag = false;
                    movementDireciton = Direction.Forward;
                    destination = transform.localPosition + localForward * stepOffSet;
                }
            }
        }
        if(Input.GetAxis("Vertical") < 0 || movementDireciton == Direction.Back || tempDirection == Direction.Back)
        {
            if (canMoveBack)
            {
                tempDirection = Direction.Back;
                if (destinationFlag)
                {
                    isMoving = true;
                    destinationFlag = false;
                    movementDireciton = Direction.Back;
                    destination = transform.localPosition + localBack * stepOffSet;
                }
            }
        }
        if (transform.localPosition == destination)
        {
            destinationFlag = true;
            if (inJunction)
            {
                tempDirection = Direction.Null;
                movementDireciton = Direction.Null;
                isMoving = false;
            }
        }
        else if(!mazeRotation.rotate)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, playerSpeed);
        }
    }
    void WallCollisionCheck()
    {
        if(rightRay.hittingWall)
        {
            canMoveRight = false;
        }
        else
        {
            canMoveRight = true;
        }

        if (leftRay.hittingWall)
        {
            canMoveLeft = false;
        }
        else
        {
            canMoveLeft = true;
        }

        if (forwardRay.hittingWall)
        {
            canMoveForward = false;
        }
        else
        {
            canMoveForward = true;
        }

        if (backRay.hittingWall)
        {
            canMoveBack = false;
        }
        else
        {
            canMoveBack = true;
        }

    }
    void EdgeDetection()
    {
        if(transform.localPosition.x > MazeOffset + 0.05)
        {
            atEdge = true;
            transform.localPosition = new Vector3(MazeOffset, transform.localPosition.y, transform.localPosition.z);
        }
        else if (transform.localPosition.y > MazeOffset + 0.05)
        {
            atEdge = true;
            transform.localPosition = new Vector3(transform.localPosition.x, MazeOffset, transform.localPosition.z);
        }
        else if (transform.localPosition.z > MazeOffset + 0.05)
        {
            atEdge = true;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, MazeOffset);
        }
        else if (transform.localPosition.x < -(MazeOffset + 0.05))
        {
            atEdge = true;
            transform.localPosition = new Vector3(-MazeOffset, transform.localPosition.y, transform.localPosition.z);
        }
        else if (transform.localPosition.y < -(MazeOffset + 0.05))
        {
            atEdge = true;
            transform.localPosition = new Vector3(transform.localPosition.x, -MazeOffset, transform.localPosition.z);
        }
        else if (transform.localPosition.z < -(MazeOffset + 0.05))
        {
            atEdge = true;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -MazeOffset);
        }


    }
    void ChangePlane()
    {
        //Debug.Log("Changing plane");
        if (!rotateFlag)
        {
            //change the position of the player
            destination = transform.localPosition + localDown * DownStep;


            //rotate the player by 90 Degree
            PlayerRotate();

            //change the position of the camera 
            ChangeCameraPosition();

            //rotate the camera
            rotateFlag = true;
        } 
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, playerSpeed);
        if(transform.localPosition == destination)
        {
            atEdge = false;
            rotateFlag = false;
        }
    }
    void PlayerRotate()
    {

        for (int i = 0; i < 90; i++)
        {
           
            if (movementDireciton == Direction.Right)
            {
                transform.Rotate(RightRotation);
            }
            else if (movementDireciton == Direction.Left)
            {
                transform.Rotate(LeftRotation);
            }
            else if (movementDireciton == Direction.Forward)
            {
                transform.Rotate(ForwardRotation);
            }
            else if (movementDireciton == Direction.Back)
            {
                transform.Rotate(BackRotation);
            }
        }

    }

    void ChangeCameraPosition() 
    {
        mazeRotation.rotate = true;

        if (movementDireciton == Direction.Right)
        {
            mazeRotation.rotateDirection = localForward * 2;
        }
        else if (movementDireciton == Direction.Left)
        {
            mazeRotation.rotateDirection = localBack * 2;
        }
        else if (movementDireciton == Direction.Forward)
        {
            mazeRotation.rotateDirection = localLeft * 2;
        }
        else if (movementDireciton == Direction.Back)
        {
            mazeRotation.rotateDirection = localRight * 2;
        }
    }
}
