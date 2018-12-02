using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactPlayer : MonoBehaviour {
    bool isCalled;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerBehaviour>())
        {
           
            var player = collision.GetComponent<PlayerBehaviour>().gameObject;
            if (player.GetComponent<PlayerParty>().humans.Count >= player.GetComponent<PlayerParty>().maxHumanInParty)
                return;
            if (isCalled)
                return;
            isCalled = true;

            player.GetComponent<PlayerParty>().AddMember(gameObject);
            GetComponent<HumanBehaviour>().underStreetLight.StartCoolDownSpawnItem();
            GetComponent<HumanBehaviour>().enabled = true;
            
        }
    }
}
