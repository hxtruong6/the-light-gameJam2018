using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioTriggerEnter : MonoBehaviour {
    private AudioSource audioScource;
    private void Start()
    {
        audioScource = gameObject.GetComponent<AudioSource>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyBehaviour>())
        {
            audioScource.Play();
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyBehaviour>())
        {
            audioScource.Stop();
        }
    }
}
