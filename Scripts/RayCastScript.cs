using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastScript : MonoBehaviour {

	// Use this for initialization
    public enum Direction
    {
        Right,
        Left,
        Forward,
        Back
    };
    Vector3 localRight;
    Vector3 localLeft;
    Vector3 localForward;
    Vector3 localBack;
    public Direction rayDirection;
    Ray localRay;
    Vector3 localDirection;
    public bool hittingWall;  
    void Start () {
	
	}

    // Update is called once per frame
    void Update() {
        localForward = transform.parent.InverseTransformDirection(transform.forward);
        localRight = transform.parent.InverseTransformDirection(transform.right);
        localLeft = localRight * -1;
        localBack = localForward * -1;
        
        switch (rayDirection)
        {
            case Direction.Right:
                localRay = new Ray(transform.localPosition, localRight);
                localDirection = localRight;
                break;

            case Direction.Left:
                localRay = new Ray(transform.localPosition, localLeft);
                localDirection = localLeft;
                break;

            case Direction.Forward:
                localRay = new Ray(transform.localPosition, localForward);
                localDirection = localForward;
                break;

            case Direction.Back:
                localRay = new Ray(transform.localPosition, localBack);
                localDirection = localBack;
                break;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(localDirection), out hit, 0.50f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(localDirection) * hit.distance, Color.red);
            hittingWall = true;
        }
        else
        {
            hittingWall = false;
            Debug.DrawRay(transform.position, transform.TransformDirection(localDirection) * .5f, Color.white);
        }

    }
}
