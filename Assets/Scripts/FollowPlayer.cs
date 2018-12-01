using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
    private Vector3 cameraOffset;

    [Range(0.01f, 1.0f)]
    public float smoothFactor;

    GameObject player;
	void Start () {
        player = GameObject.FindObjectOfType<PlayerBehaviour>().gameObject;
        cameraOffset = transform.position - player.transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.IsGameOver())
            return;
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        Vector3 newPos = player.transform.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
        transform.LookAt(player.transform);
    }
}
