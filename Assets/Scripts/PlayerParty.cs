using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerParty : MonoBehaviour
{
    public int humanCount;
    public List<GameObject> humans = new List<GameObject>();
    public int maxHumanInParty = 5;
    public Text numberPartyText;
    [SerializeField] private HumanBehaviour humanPrefab;

    // Use this for initialization
    void Start()
    {
        humans.Add(this.gameObject);
        numberPartyText.text = "1";
        //print("first human count: " + humans.Count);
    }

    public void AddMember(GameObject party)
    {
        if (humans.IndexOf(party) >= 0) return;
        Vector3 nextPosHuman = transform.position;
        if (humans.Count > 0)
        {
            float x = humans[humans.Count - 1].transform.position.x;
            float y = humans[humans.Count - 1].transform.position.y;
            float distance = 0.0f;
            if (humans[humans.Count - 1].GetComponent<HumanBehaviour>())
            {
                distance = humans[humans.Count - 1].GetComponent<HumanBehaviour>().circleColliderRadius * 2;

            }
            else
            {
                // TODO: don't set hard code
                distance = 1f;
            }

            Vector3 directVec = GetComponent<PlayerBehaviour>().forwardPosition.position - humans[humans.Count - 1].transform.position;
            directVec = -directVec.normalized * distance;

            nextPosHuman = new Vector3(x, y, 0) + directVec;
        }
        else
        {
            print("No exsited humans");
            return;
        }
        party.transform.position = nextPosHuman;
        party.transform.SetParent(null);
        humans.Add(party);
        numberPartyText.text = humans.Count.ToString();

    }
    // Update is called once per frame
    void Update()
    {
    }

    public void RemoveLastMember()
    {
        if (humans.Count == 0) return;
        Destroy(humans[humans.Count - 1].gameObject);
        humans.RemoveAt(humans.Count - 1);
    }
}
