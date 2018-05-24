using UnityEngine;

public class ColliderScript : MonoBehaviour {


    public bool hit;
    void Start()
    {
        Debug.Log("initiating the colliders. . . ");
        hit = false;
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("HIT : " + other.tag);
        Debug.Log("hitting started ");
    }
    void OnTriggerStay(Collider other)
    {
        Debug.Log("HIT : " + other.tag);
        hit = true;
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("hitting ended");
        hit = false;
    }
}
