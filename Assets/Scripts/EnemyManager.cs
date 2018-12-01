using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    [SerializeField] private float radius;
    public List<GameObject> listEnemy = new List<GameObject>();
    public static EnemyManager instance;
    [SerializeField] private float thresholdDistanceObstacle;
    private bool enableSpawnEnemy = true;
    private float timeCount = 5;
    // Use this for initialization
    void Start()
    {
        instance = this;
        enableSpawnEnemy = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (listEnemy.Count > 10 || !enableSpawnEnemy) return;
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
        listEnemy.Add(enemyClone);
    }

    private bool isAvailblePosition(Vector3 pitvotPos)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(pitvotPos.x, pitvotPos.y),
            enemy.GetComponent<EnemyBehaviour>().circleColliderRadius + thresholdDistanceObstacle);
        return hitColliders.Length == 0;
    }

    public void ClearAllEnemy()
    {
        for (int i = 0; i < listEnemy.Count; i++)
        {
            Destroy(listEnemy[i].gameObject);
        }
        EnemyManager.instance.listEnemy.Clear();
        enableSpawnEnemy = false;
    }


}
