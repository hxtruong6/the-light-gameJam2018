using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    private GameObject player;

    [SerializeField] private float speed;

    [SerializeField] public float circleColliderRadius;

    private List<HumanBehaviour> humans = new List<HumanBehaviour>();

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerBehaviour>().gameObject;
        gameObject.GetComponent<CircleCollider2D>().radius = circleColliderRadius;
    }


    // Update is called once per frame
    void Update()
    {
        // TODO: need to balance game at here
        // ...

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
        //var playerBehaviour = player.GetComponent<PlayerBehaviour>();
        //List<GameObject> human = new List<GameObject>();
        //if (!player.GetComponent<PlayerBehaviour>())
        //{
        //    print("No humans get");
        //    return null;
        //}
        humans = player.GetComponent<PlayerBehaviour>().humans;
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        for (int i = 0; i < humans.Count; i++)
        {
            var go = humans[i].gameObject;
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
