using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print(collision.gameObject);
        if (collision.GetComponent<PlayerBehaviour>())
        {
            var player = collision.GetComponent<PlayerBehaviour>().gameObject;
            player.GetComponent<PlayerParty>().AddMember(gameObject);
            GetComponent<HumanBehaviour>().enabled = true;
        }
        Destroy(this);
    }
}
