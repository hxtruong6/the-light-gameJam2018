using System.Collections.Generic;
using UnityEngine;

public class StreetLightManager : MonoBehaviour
{
    public int maxLightNumber;
    [SerializeField] private GameObject streetLight;
    [SerializeField] private float radius = 5f;

    [SerializeField] private GameObject player;
    [SerializeField] private float thresholdDistanceObstacle = 1.0f;

    private List<HumanBehaviour> humans = new List<HumanBehaviour>();

    private float timeCount = 5f;

    private int countLight = 0;
    // Use this for initialization
    void Start()
    {
        countLight = 0;
        StreetLightSpawn();
        StreetLightSpawn();
        StreetLightSpawn();
    }

    // Update is called once per frame
    void Update()
    {

        if (timeCount >= 5.0f)
        {
            StreetLightSpawn();
            timeCount = 0;
        }
        timeCount += Time.deltaTime;
    }

    void StreetLightSpawn()
    {
        StreetLight[] lights = GameObject.FindObjectsOfType<StreetLight>();
        if (lights.Length >= maxLightNumber)
            return;
        Vector3 newPos = Vector3.zero;
        newPos = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), 0);
        countLight = 0;
        while (!isAvailblePosition(newPos))
        {
            countLight++;
            //print("Light: " + countLight);
            newPos = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), 0);
            if (countLight > 10) return;
        }

        Instantiate(streetLight, newPos, Quaternion.identity);
    }
    private bool isAvailblePosition(Vector3 pitvotPos)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(pitvotPos.x, pitvotPos.y),
            streetLight.GetComponent<StreetLight>().circleColliderRadius + thresholdDistanceObstacle);
        return hitColliders.Length == 0;
    }


}
