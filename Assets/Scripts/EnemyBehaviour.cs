using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    private GameObject player;

    [SerializeField] private float speed;

    [SerializeField] public float circleColliderRadius;
    [SerializeField] private float distanceWithLight;

    private List<GameObject> humans = new List<GameObject>();
    private bool preventingTheLight;
    private float timeCountDown;
    private Vector3 prevStreetLigthCollider;



    // Use this for initialization
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerBehaviour>().gameObject;
        gameObject.GetComponent<CircleCollider2D>().radius = circleColliderRadius;
        preventingTheLight = false;
        timeCountDown = 3f;
    }


    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsGameOver()) return;
        // TODO: need to balance game at here
        // ...

        // Find the nearest human
        if (preventingTheLight)
        {
            // Moving the opposite with the light 
            var desired_velocity = (this.transform.position - prevStreetLigthCollider).normalized * speed * Time.deltaTime;
            //var veclocity2D = GetComponent<Rigidbody2D>().velocity;
            //var steering = desired_velocity - new Vector3(veclocity2D.x, veclocity2D.y, 0);
            //GetComponent<Rigidbody2D>().AddForce(steering);
            transform.position += desired_velocity;

            // Check 
            if (Vector3.Distance(transform.position, prevStreetLigthCollider) >= distanceWithLight)
            {
                preventingTheLight = false;
                // TODO: dont set hard code here
                timeCountDown = 3f;

            }
        }
        else if (timeCountDown > 0)
        {
            timeCountDown -= Time.deltaTime;
        }
        else
        {
            var closestPlayer = FindClosestPlayer();
            if (closestPlayer)
            {
                transform.position = Vector2.MoveTowards(transform.position, closestPlayer.transform.position,
                    speed * Time.deltaTime);
            }
        }
    }

    public GameObject FindClosestPlayer()
    {
        humans = player.GetComponent<PlayerParty>().humans;
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

        if (humans.Count == 0)
        {
            return FindObjectOfType<PlayerBehaviour>().gameObject;
        }

        return closest;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Handle end game
        if (other.GetComponent<StreetLight>())
        {
            preventingTheLight = true;
            prevStreetLigthCollider = other.transform.position;
        }
        else if (other.GetComponent<PlayerBehaviour>())
        {
            EnemyManager.instance.ClearAllEnemy();
            other.GetComponent<PlayerBehaviour>().enabled = false;
            GameManager.instance.EndGame();
        }

    }


}
