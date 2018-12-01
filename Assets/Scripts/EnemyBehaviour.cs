using UnityEngine;

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
        transform.position = Vector2.MoveTowards(transform.position, closestPlayer.transform.position, speed * Time.deltaTime);
    }

    public GameObject FindClosestPlayer()
    {
        var human = player.GetComponent<PlayerBehaviour>().humans;
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        for (int i = 0; i < human.Count; i++)
        {
            var go = human[i].gameObject;
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
