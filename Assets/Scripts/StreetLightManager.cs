using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class StreetLightManager : MonoBehaviour
{
    public int maxLightNumber;
    [SerializeField] private GameObject streetLight;
    [SerializeField] private float radius = 5f;
    [SerializeField] private float thresholdDistanceObstacle = 1.0f;
    private GameObject player;
    private List<HumanBehaviour> humans = new List<HumanBehaviour>();

    private float timeCount = 5f;
    private FollowPlayer followPlayer;

    private int countLight = 0;
    // Use this for initialization
    void Start()
    {
        followPlayer = FindObjectOfType<FollowPlayer>();
        countLight = 0;
        player = FindObjectOfType<PlayerBehaviour>().gameObject;
        if (player == null)
        {
            print("Cannot find the player");
        }
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
        Vector3 newPos = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), 0) + player.transform.position;
        countLight = 0;
        while (!isAvailblePosition(newPos))
        {
            countLight++;
            //print("Light: " + countLight);
            newPos = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), 0) + player.transform.position;
            if (countLight > 10) return;
        }

        Instantiate(streetLight, newPos, Quaternion.identity);
    }
    private bool isAvailblePosition(Vector3 pitvotPos)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(pitvotPos.x, pitvotPos.y),
            streetLight.GetComponent<StreetLight>().circleColliderRadius + thresholdDistanceObstacle);

        return hitColliders.Length == 0 && (pitvotPos.x > followPlayer.minX && pitvotPos.x < followPlayer.maxX && pitvotPos.y > followPlayer.minY && pitvotPos.y < followPlayer.maxY);
    }


}
