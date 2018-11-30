using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject player;

    [SerializeField] private float speed;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }


    // Update is called once per frame
    void Update()
    {
        var playerPos = player.transform;
        // TODO: find the nearest human
        transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
    }

    public GameObject FindClosestPlayer()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Human");
        // TODO: get list gameobjects from Player
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
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
