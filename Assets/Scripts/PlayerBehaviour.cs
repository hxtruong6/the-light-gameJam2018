using System.Collections.Generic;
using UnityEngine;
public class PlayerBehaviour : MonoBehaviour
{
    public float speed;
    public GameObject flashlight;
    public bool lightOn;

    [SerializeField] private GameObject humanPrefab;
    [HideInInspector] public List<GameObject> human = new List<GameObject>();
    private Rigidbody2D rb2d;
    private bool isDead;


    // Use this for initialization
    void Start()
    {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        var humInstantiate = Instantiate(humanPrefab, Vector3.zero, Quaternion.identity);
        humInstantiate.transform.parent = this.transform;
        human.Add(humInstantiate);
        for (int i = 0; i < 1; i++)
        {
            AddingHuman();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 tempVect = new Vector3(h, v, 0);
        tempVect = tempVect.normalized * speed * Time.deltaTime;
        rb2d.MovePosition(rb2d.transform.position + tempVect);
        //rb2d.rotation = Quaternion.Lerp(rb2d.transform.rotation + tempVect);
        float angle = Mathf.Atan2(tempVect.y, tempVect.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lightOn = !lightOn;
            FlashLight(lightOn);
        }
    }

    private void FlashLight(bool lightOn)
    {
        if (lightOn) flashlight.gameObject.SetActive(true);
        else flashlight.gameObject.SetActive(false);
    }

    public void AddingHuman()
    {
        float x = human[human.Count - 1].transform.position.x;
        float y = human[human.Count - 1].transform.position.y;
        float addingRadius = human[human.Count - 1].GetComponent<HumanBehaviour>().circleColliderRadius;
        print("forward: " + human[human.Count - 1].transform.forward);
        print("pos: " + human[human.Count - 1].transform.position);
        Vector3 nextPosHuman = -human[human.Count - 1].transform.forward + new Vector3(addingRadius, addingRadius, 0);
        var humanInstantiate = Instantiate(humanPrefab, nextPosHuman, Quaternion.identity);
        humanInstantiate.transform.parent = this.transform;
        human.Add(humanInstantiate);
    }

}
