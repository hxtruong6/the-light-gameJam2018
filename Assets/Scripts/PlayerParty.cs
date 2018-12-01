using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParty : MonoBehaviour {
    public int humanCount;
    [System.NonSerialized] public List<HumanBehaviour> humans = new List<HumanBehaviour>();

    [SerializeField] private HumanBehaviour humanPrefab;

    // Use this for initialization
    void Start () {
        //var humInstantiate = Instantiate(humanPrefab, transform.position, transform.rotation, transform);
        //humInstantiate.GetComponent<SpriteRenderer>().enabled = false;
        //humans.Add(humInstantiate);
    }

    public void AddNewFellows()
    {
        if (humans.Count == 0) return;
        float x = humans[humans.Count - 1].transform.position.x;
        float y = humans[humans.Count - 1].transform.position.y;
        float distance = humans[humans.Count - 1].circleColliderRadius * 2;

        Vector3 directVec = GetComponent<PlayerBehaviour>().forwardPosition.position - humans[humans.Count - 1].transform.position;
        directVec = -directVec.normalized * distance;

        Vector3 nextPosHuman = new Vector3(x, y, 0) + directVec;
        var humanInstantiate = Instantiate(humanPrefab, nextPosHuman, Quaternion.identity);
        humans.Add(humanInstantiate);
    }

    public void AddMember(GameObject party)
    {
        //float x = humans[humans.Count - 1].transform.position.x;
        //float y = humans[humans.Count - 1].transform.position.y;
        //float distance = humans[humans.Count - 1].circleColliderRadius * 2;

        //Vector3 directVec = GetComponent<PlayerBehaviour>().forwardPosition.position - humans[humans.Count - 1].transform.position;
        //directVec = -directVec.normalized * distance;

        //Vector3 nextPosHuman = new Vector3(x, y, 0) + directVec;
        //party.transform.position = nextPosHuman;
        humans.Add(party.gameObject.GetComponent<HumanBehaviour>());


    }
    public void AddNewPlayerToParty(HumanBehaviour humanToAdd)
    {
        humans.Add(humanToAdd);
    }

    // Update is called once per frame
    void Update () {
		
	}


    public void CutMembers(HumanBehaviour human)
    {
        // Remove from this to the end of list
        var index = this.humans.IndexOf(human);
        for (int i = index + 1; i < humans.Count; i++)
        {
            humans[i].enabled = false;
        }

        humans.Remove(human);
        //humans.RemoveRange(index, humans.Count - 1);
    }

    public void RemoveLastMember()
    {
        if (humans.Count == 0) return;
        Destroy(humans[humans.Count - 1].gameObject);
        humans.RemoveAt(humans.Count - 1);
    }
}
