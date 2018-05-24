using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBodyRotation : MonoBehaviour {

    enum Direction  //for the direcitons 
    {
        None,
        Right,
        Left,
        Forward,
        Back
    };
    public Vector3 rotateDirection;
    int counter;
    public bool rotate;
    void FixedUpdate()
    {

        if (rotate)
        {
            counter++;
            transform.Rotate(rotateDirection);
            if (counter == 45)
            {
                Debug.Log("rotating");
                rotate = false;
                counter = 0;

            }
        }

    }
}
