using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float allEnemySpeed;
    [SerializeField] private float radius;
    public List<GameObject> listEnemy = new List<GameObject>();
    public static EnemyManager instance;
    [SerializeField] private float thresholdDistanceObstacle;
    [SerializeField] private int maxNumberEnemy;

    [SerializeField] private float timeBetweenSpawn = 4f;
    [SerializeField] private float increaseEnemyCoolDown = 10f;
    [SerializeField] private float increaseEnemySpeedCoolDown = 20f;

    private GameObject player;

    private bool enableSpawnEnemy = true;
    private float timeCount = 5;

    private int countEnemy = 0;
    // Use this for initialization
    void Start()
    {
        instance = this;
        enableSpawnEnemy = true;
        countEnemy = 0;
        player = FindObjectOfType<PlayerBehaviour>().gameObject;
        StartCoroutine(IncreaseEnemy(increaseEnemyCoolDown));
        StartCoroutine(IncreaseEnemySpeed(increaseEnemySpeedCoolDown));
    }

    void Update()
    {
        if (listEnemy.Count > maxNumberEnemy || !enableSpawnEnemy) return;

        if (timeCount > timeBetweenSpawn)
        {
            RandomEnemy();
            timeCount = 0;
        }
        timeCount += Time.deltaTime;
    }


    IEnumerator IncreaseEnemy(float waitTime)
    {
        while (GetComponent<GameManager>().isGameOver == false)
        {
            yield return new WaitForSeconds(waitTime);
            maxNumberEnemy++;
        }
    }

    IEnumerator IncreaseEnemySpeed(float waitTime)
    {
        while (GetComponent<GameManager>().isGameOver == false)
        {
            yield return new WaitForSeconds(waitTime);
            allEnemySpeed += 0.5f;
            for (int i = 0; i < listEnemy.Count; i++)
            {
                listEnemy[i].GetComponent<EnemyBehaviour>().SetSpeed(allEnemySpeed);
            };
        }
    }

    void RandomEnemy()
    {
        var newPos = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), 0) + player.transform.position;
        countEnemy = 0;

        while (!isAvailblePosition(newPos))
        {
            newPos = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), 0) + player.transform.position;
            countEnemy++;
            //print("Enemy: " + countEnemy);
            if (countEnemy > 10) return;
        }
        var enemyClone = Instantiate(enemy, newPos , Quaternion.identity);
        enemyClone.transform.parent = this.transform;
        enemyClone.GetComponent<EnemyBehaviour>().SetSpeed(allEnemySpeed);
        listEnemy.Add(enemyClone);
    }

    private bool isAvailblePosition(Vector3 pitvotPos)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(pitvotPos.x, pitvotPos.y),
            enemy.GetComponent<EnemyBehaviour>().circleColliderRadius + thresholdDistanceObstacle*1.5f);
        return hitColliders.Length == 0;
    }

    public void ClearAllEnemy()
    {
        enableSpawnEnemy = false;
        this.enabled = false;     
    }


}
