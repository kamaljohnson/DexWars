using UnityEngine;

/*
 * 1. script to direct the camara to move (HINT make the camara move over to the corners of am imaginary cube over the main maze cube)
 * 2. move the camera to the next position by using the direction of movement of the player and the current position of the camera  
 */ 

public class playerController : MonoBehaviour {

    //all the variables used to controll the external scripts 
    public bool playerCameraIsRotating = false;
    float MazeSize = 1; //store the size of the maze
    private float MazeOffset;
    int mazeDimention = 4;  //store the dimention of the maze (eg 4X4 etc)

    //components for the player rotaration (the ground trigger items)
    //all the colliders are set to isTrigger
    public GameObject rightGround;
    public GameObject leftGround;
    public GameObject forwardGround;
    public GameObject backGround;

    ColliderScript rightcol;
    ColliderScript leftcol;
    ColliderScript forwardcol;
    ColliderScript backcol;

    bool trigFlag;

    enum Direction  //direction of the player movement
    {
        Null,
        Right,
        Left,
        Forward,
        Back
    };
    Direction trigDirection = Direction.Null;
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


    void Start () {

        rightcol = rightGround.GetComponent<ColliderScript>();
        leftcol = leftGround.GetComponent<ColliderScript>();
        forwardcol = forwardGround.GetComponent<ColliderScript>();
        backcol = backGround.GetComponent<ColliderScript>();

        trigFlag = false;

        MazeOffset = MazeSize / mazeDimention + transform.localScale.x;
        Debug.Log(MazeOffset);
    }
	
	void FixedUpdate () {
        if(rightcol.exit && !trigFlag)
        {
            trigFlag = true;
            trigDirection = Direction.Right;
        }
        if (leftcol.exit && !trigFlag)
        {
            trigFlag = true;
            trigDirection = Direction.Left;
        }
        if (forwardcol.exit && !trigFlag)
        {
            trigFlag = true;
            trigDirection = Direction.Forward;
        }
        if (backcol.exit && !trigFlag)
        {
            trigFlag = true;
            trigDirection = Direction.Back;
        }
        Debug.Log("trigger direciton : " + trigDirection);
        localForward = transform.parent.InverseTransformDirection(transform.forward);
        localRight = transform.parent.InverseTransformDirection(transform.right);
        localLeft = localRight * -1;
        localBack = localForward * -1;
        localDown = transform.parent.InverseTransformDirection(transform.up) * -1;

        if (!atEdge)
        {
            Move();
        }
        else
        {

            RotateCamera();
        }

    }

    void Move() //controls the movement of the player 
    {
        Vector3 destination = new Vector3();
        if (Input.GetAxis("Horizontal") > 0)
        {
            movementDireciton = Direction.Forward;
            destination = transform.localPosition + localRight * 2;
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            movementDireciton = Direction.Back;
            destination = transform.localPosition + localLeft* 2;
        }
        else if(Input.GetAxis("Vertical") > 0)
        {
            movementDireciton = Direction.Right;
            destination = transform.localPosition + localForward * 2;
        }
        else if(Input.GetAxis("Vertical") < 0)
        {
            movementDireciton = Direction.Left;
            destination = transform.localPosition + localBack * 2;
        }
        else
        {
            direction = new Vector3(0, 0, 0);
            destination = transform.localPosition;
        }
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, playerSpeed);

    }
    void RotateCamera() // funtion to rotate the player camera
    {

        
    }
}
