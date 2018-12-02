using System;
using System.Collections.Generic;
using UnityEngine;

public class PretendingEnemyBehaviour : MonoBehaviour
{

    private GameObject player;
    [SerializeField] private float speed;
    [SerializeField] public float circleColliderRadius;
    [SerializeField] private float distanceWithLight;
    [SerializeField] private float pretendingTime;
    private List<GameObject> humans = new List<GameObject>();
    private bool preventingTheLight;
    private float timeCountDown;
    private Vector3 prevStreetLigthCollider;


    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerBehaviour>().gameObject;
        gameObject.GetComponent<CircleCollider2D>().radius = circleColliderRadius;
        preventingTheLight = false;
        timeCountDown = 3f;
    }

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
            PretendingWayToPlayer();
        }
    }

    private void PretendingWayToPlayer()
    {
        //desired_velocity = normalize(target - position) * max_velocity
        //steering = desired_velocity - velocity
        float distancePretendTarget = GetDistancePretendTarget(
            transform.position,
            player.transform.position,
            player.transform.position + player.GetComponent<PlayerBehaviour>().moveVelocity * pretendingTime
        );

    }

    private float GetDistancePretendTarget(Vector3 a, Vector3 b, Vector3 c)
    {
        double cosVal = (a.x * b.x + a.y * b.y) / (Math.Sqrt((a.x * a.x + a.y * a.y) * (b.x * b.x + b.y * b.y)));
        //return Math.Sqrt(Math.Pow(a.magnitude, 2) + Math.Pow(b.magnitude, 2) - 2 * a.magnitude * b.magnitude * cosVal);
        return 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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
