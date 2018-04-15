using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour {

    public bool exit;
    private void Start()
    {
        exit = false;
    }
    private void OnTriggerExit(Collider other)
    {
        exit = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        exit = false;
    }
}
