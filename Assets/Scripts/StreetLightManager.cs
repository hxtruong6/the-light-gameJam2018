using System.Collections.Generic;
using UnityEngine;

public class StreetLightManager : MonoBehaviour
{
    public int maxLightNumber;
    [SerializeField] private float radius = 5f;
    [SerializeField] private float thresholdDistanceObstacle = 1.0f;
    [SerializeField] private List<GameObject> streetLightList = new List<GameObject>();
    [Header("SUM MUST BE = 1")] [SerializeField] [Tooltip("Sum of probability MUST BE equal 1")] private List<float> streetLightProbabilities = new List<float>();
    private GameObject player;

    private float timeCount = 5f;
    private FollowPlayer followPlayer;

    private int countLight = 0;
    // Use this for initialization
    //[ExecuteInEditMode]
    //void OnValidate()
    //{
    //    if (streetLightProbabilities.Count != streetLightList.Count)
    //    {
    //        streetLightProbabilities.Clear();
    //        for (int i = 0; i < streetLightList.Count; i++)
    //        {
    //            streetLightProbabilities.Add((float)1 / streetLightList.Count);
    //        }
    //    }
    //}
    //{
    //    if (streetLightProbabilities.Count != streetLightList.Count)
    //    {
    //        streetLightProbabilities.Clear();
    //        for (int i = 0; i < streetLightList.Count; i++)
    //        {
    //            streetLightProbabilities.Add((float)1 / streetLightList.Count);
    //        }
    //    }
    //    else
    //    {
    //        float sum = 0;
    //        for (int i = 0; i < streetLightProbabilities.Count; i++)
    //        {
    //            if (streetLightProbabilities[i] > 1 || streetLightProbabilities[i] < 0)
    //            {
    //                streetLightProbabilities[i] = 0;
    //            }

    //            sum += streetLightProbabilities[i];
    //        }

    //        if (sum > 1 || sum < 0)
    //        {
    //            for (int i = 0; i < streetLightList.Count; i++)
    //            {
    //                streetLightProbabilities[i] = (float)1 / streetLightList.Count;
    //            }
    //        }
    //        else
    //        {
    //            streetLightProbabilities[streetLightProbabilities.Count - 1] = 1 - sum;
    //        }
    //    }
    //}

    void Start()
    {
        followPlayer = FindObjectOfType<FollowPlayer>();
        countLight = 0;
        player = FindObjectOfType<PlayerBehaviour>().gameObject;
        if (player == null)
        {
            print("Cannot find the player");
        }

        // TODO: Check if all of streetlight is static and not destroy
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
        // check if street light too much
        StreetLight[] lights = GameObject.FindObjectsOfType<StreetLight>();
        if (lights.Length >= maxLightNumber)
            return;
        // Spawn
        Vector3 newPos = new Vector3(UnityEngine.Random.Range(-radius, radius), UnityEngine.Random.Range(-radius, radius), 0) + player.transform.position;
        countLight = 0;
        int index = GetProbabilityIndex(streetLightProbabilities);
        if (index == -1)
        {
            // make default
            index = 0;
        }


        while (!isAvailblePosition(newPos, index))
        {
            countLight++;
            //print("Light: " + countLight);
            newPos = new Vector3(UnityEngine.Random.Range(-radius, radius), UnityEngine.Random.Range(-radius, radius), 0) + player.transform.position;
            if (countLight > 10) return;
        }

        Instantiate(streetLightList[index], newPos, Quaternion.identity);
    }

    private int GetProbabilityIndex(List<float> probabilies)
    {
        float r = UnityEngine.Random.Range(0.0f, 1.0f);
        if (probabilies.Count == 0) return -1;
        if (r < probabilies[0]) return 0;
        float currSum = probabilies[0];
        for (int i = 1; i < probabilies.Count - 1; i++)
        {
            if (r >= currSum && r < currSum + probabilies[i])
            {
                return i;
            }
        }
        return probabilies.Count - 1;
    }

    private bool isAvailblePosition(Vector3 pitvotPos, int index)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(pitvotPos.x, pitvotPos.y),
            streetLightList[index].GetComponent<StreetLight>().circleColliderRadius + thresholdDistanceObstacle);

        return hitColliders.Length == 0 && (pitvotPos.x > followPlayer.minX && pitvotPos.x < followPlayer.maxX && pitvotPos.y > followPlayer.minY && pitvotPos.y < followPlayer.maxY);
    }


}
