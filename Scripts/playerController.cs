using UnityEngine;

/*
 * 1. script to direct the camara to move (HINT make the camara move over to the corners of am imaginary cube over the main maze cube)
 * 2. move the camera to the next position by using the direction of movement of the player and the current position of the camera  
 */ 

public class playerController : MonoBehaviour {

    public bool playerCameraIsRotating = false;
    enum Direciton  //direction of the player movement
    {
        right,
        left,
        forward,
        back
    };
    public float playerSpeed;   //the speed of the player also manipulates the animation speed of the player 
    private bool atEdge;    //to check if the player reached the edge of the maze(to trigger the camera movement and the player rotation)


    void Start () {
		
	}
	
	void Update () {
		
	}
}
