using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour {

    public int addBattery = 20;

    void Start()
    {
        
    }

    void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerBehaviour>())
        {
            var playerFlashLight = collision.GetComponent<PlayerBehaviour>().gameObject.GetComponent<FlashLight>();
            playerFlashLight.SetCurrentFlashLightBattery((int)(playerFlashLight.GetCurrentFlashLightBattery() + addBattery));
            GetComponentInParent<StreetLight>().CoolDownSpawnObject();
            Destroy(gameObject);
        }
    }
}
