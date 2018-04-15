using UnityEngine;

public class CameraScript : MonoBehaviour {

    public GameObject player;

	void Update () {
        transform.LookAt(player.transform);
	}
}
