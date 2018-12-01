using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    [SerializeField] private float radius;
    [SerializeField] private float thresholdDistanceObstacle;

    private float timeCount = 5;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timeCount > 4.0f)
        {
            RandomEnemy();
            timeCount = 0;
        }
        timeCount += Time.deltaTime;
    }

    void RandomEnemy()
    {
        var newPos = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), 0);
        //var rootPos = new Vector3(0, 0, 0);
        //TODO: check player position
        while (!isAvailblePosition(newPos))
        {
            newPos = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), 0);
        }
        var enemyClone = Instantiate(enemy, newPos, Quaternion.identity);
        enemyClone.transform.parent = this.transform;

    }

    private bool isAvailblePosition(Vector3 pitvotPos)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(pitvotPos.x, pitvotPos.y),
            enemy.GetComponent<EnemyBehaviour>().circleColliderRadius + thresholdDistanceObstacle);
        return hitColliders.Length == 0;
    }
}
