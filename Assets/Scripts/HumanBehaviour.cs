using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBehaviour : MonoBehaviour {
    public float circleColliderRadius;
	// Use this for initialization
	void Start () {
	    gameObject.GetComponent<CircleCollider2D>().radius = circleColliderRadius;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
