using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour {

    public int addBattery = 20;
    bool isCalled;

    void Start()
    {
        
    }

    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerBehaviour>())
        {
            if (isCalled)
                return;
            isCalled = true;

            var playerFlashLight = collision.GetComponent<PlayerBehaviour>().gameObject.GetComponent<FlashLight>();
            playerFlashLight.SetCurrentFlashLightBattery((int)(playerFlashLight.GetCurrentFlashLightBattery() + addBattery));
            //GetComponentInParent<StreetLight>().StartCoolDownSpawnItem();
            GetComponentInParent<StreetLight>().isOccupied = false;
            Destroy(gameObject);
        }
    }
}
