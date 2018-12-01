using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    [SerializeField] private float radius;
    public List<GameObject> listEnemy = new List<GameObject>();
    public static EnemyManager instance;
    private float timeCount = 5;
    // Use this for initialization
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeCount > 2.0f)
        {
            RandomEnemy();
            timeCount = 0;
        }
        timeCount += Time.deltaTime;
    }

    void RandomEnemy()
    {
        var pos2D = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), 0);
        var rootPos = new Vector3(0, 0, 0);
        //TODO: check player position
        var enemyClone =  Instantiate(enemy, pos2D + rootPos, Quaternion.identity);
        enemyClone.transform.parent = this.transform;
        listEnemy.Add(enemyClone);
    }
}
