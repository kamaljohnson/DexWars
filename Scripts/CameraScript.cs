using UnityEngine;

public class CameraScript : MonoBehaviour {


    
    public void changePosition(Vector3 Rotation)
    {
        transform.Rotate(Rotation);
    }
}
