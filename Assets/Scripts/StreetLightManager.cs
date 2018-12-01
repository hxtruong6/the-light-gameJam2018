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
        bool validPosition = false;
        while (!validPosition)
        {
            newPos = new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), 0);
            validPosition = true;
            humans = player.GetComponent<PlayerParty>().humans;

            // Check not collision with human list
            for (int i = 0; i < humans.Count; i++)
            {
                if (Vector3.Distance(humans[i].transform.position, newPos) <= thresholdDistanceObstacle)
                {
                    validPosition = false;
                    break;
                }
            }
        }
        Instantiate(streetLight, newPos, Quaternion.identity);
    }


}
