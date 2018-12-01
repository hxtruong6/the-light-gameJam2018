using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTriggerEnter : MonoBehaviour {
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyBehaviour>())
        {
            var enemy = collision.GetComponent<EnemyBehaviour>();
            enemy.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyBehaviour>())
        {
            var enemy = collision.GetComponent<EnemyBehaviour>();
            enemy.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
