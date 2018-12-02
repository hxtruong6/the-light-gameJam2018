using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour {
    private AudioSource audioSource;
	// Use this for initialization
	void Start () {
        audioSource = this.gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayEnter()
    {
        audioSource.Play();
    }
}
