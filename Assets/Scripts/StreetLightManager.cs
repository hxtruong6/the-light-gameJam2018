using System.Collections.Generic;
using UnityEngine;

public class StreetLightManager : MonoBehaviour
{

    [SerializeField] private GameObject streetLight;
    [SerializeField] private float radius = 5f;

    [SerializeField] private GameObject player;
    [SerializeField] private float thresholdDistanceObstacle = 1.0f;

    private List<HumanBehaviour> humans = new List<HumanBehaviour>();

    private float timeCount = 5f;
    // Use this for initialization
    void Start()
    {

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
        Vector3 newPos = Vector3.zero;
        newPos = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), 0);
        bool validPosition = false;
        while (!isAvailblePosition(newPos))
        {
            newPos = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), 0);
        }
        Instantiate(streetLight, newPos, Quaternion.identity);
    }
    private bool isAvailblePosition(Vector3 pitvotPos)
    {

        //Collider[]  hitColliders = Physics.OverlapSphere(pitvotPos, streetLight.GetComponent<StreetLight>().circleColliderRadius + thresholdDistanceObstacle);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(pitvotPos.x, pitvotPos.y),
            streetLight.GetComponent<StreetLight>().circleColliderRadius + thresholdDistanceObstacle);
        if (hitColliders.Length > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


}
