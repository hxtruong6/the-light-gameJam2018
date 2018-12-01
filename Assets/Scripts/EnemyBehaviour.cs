using UnityEngine;
using System.Collections.Generic;

public class EnemyBehaviour : MonoBehaviour
{

    private GameObject player;

    [SerializeField] private float speed;

    [SerializeField] private float circleColliderRadius;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        gameObject.GetComponent<CircleCollider2D>().radius = circleColliderRadius;
    }


    // Update is called once per frame
    void Update()
    {
        // Find the nearest human
        var closestPlayer = FindClosestPlayer();
        if (closestPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, closestPlayer.transform.position,
                speed * Time.deltaTime);
        }
    }

    public GameObject FindClosestPlayer()
    {
        var playerBehaviour = player.GetComponent<PlayerBehaviour>();
        List<GameObject> human = new List<GameObject>();
        if (!playerBehaviour)
        {
            human = player.GetComponent<PlayerBehaviour>().human;
            print("No human get");
            return null;
        }
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        for (int i = 0; i < human.Count; i++)
        {
            var go = human[i];
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

}
