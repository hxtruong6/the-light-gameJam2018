using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public float speed;
    public GameObject flashlight;
    public bool lightOn;
    public int maxBattery;
    public int maxFear;
    public int numberHumanNotFear;

    [SerializeField] private GameObject humanPrefab;
    [HideInInspector] public List<GameObject> human = new List<GameObject>();
    [SerializeField] private Transform forwardPosition;
    [SerializeField] private float thresholdDistance = 0.5f;
    private Rigidbody2D rb2d;
    Vector3 moveVelocity;
    private bool isDead;
    private bool isFollowing;
    private float movingTime = 0;

    // Use this for initialization
    void Start()
    {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();

        var humInstantiate = Instantiate(humanPrefab, Vector3.zero, Quaternion.identity);
        humInstantiate.transform.parent = this.transform;
        human.Add(humInstantiate);
        for (int i = 0; i < 10; i++)
        {
            AddingHuman();
        }

        isFollowing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        Vector3 moveInput = new Vector3(SimpleInput.GetAxis("Horizontal") + Input.GetAxis("Horizontal"), SimpleInput.GetAxis("Vertical") + Input.GetAxis("Vertical"), 0);
        moveVelocity = moveInput * speed * Time.deltaTime;
        //rb2d.MovePosition(rb2d.transform.position + moveVelocity);
        rb2d.transform.position += moveVelocity;

        // set the first human


        if (moveVelocity != Vector3.zero)
        {
            //movingTime += Time.deltaTime;
            //if (!isFollowing && movingTime >2f)
            //{
            //    movingTime = 0;
            //    var startPosition = human[0].GetComponent<HumanBehaviour>().transform.position;
            //    human[0].GetComponent<HumanBehaviour>().transform.position = transform.position;
            //    HumanFollowing(startPosition);
            //}
            float rot_z = Mathf.Atan2(moveInput.normalized.y, moveInput.normalized.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }
        if (Input.GetKeyDown(KeyCode.Space) || SimpleInput.GetButtonDown("Light"))
        {
            lightOn = !lightOn;

        }
        PlayerStack.PlayerFear(numberHumanNotFear, maxFear);
        FlashLight(lightOn);
    }

    void HumanFollowing(Vector3 startPosition)
    {
        isFollowing = true;
        Vector3 previousPosition = startPosition;
        Vector3 tempPosition = Vector3.zero;
        for (int i = 1; i < human.Count; i++)
        {
            //if (Vector3.Distance(human[i].transform.position, previousPosition) >= thresholdDistance)
            {
                tempPosition = human[i].transform.position;
                human[i].GetComponent<HumanBehaviour>().Arriving(previousPosition);
                previousPosition = tempPosition;
            }
        }
        isFollowing = false;
    }

    private IEnumerator FollowingPlayer()
    {
        isFollowing = true;
        for (int i = 1; i < human.Count; i++)
        {
            yield return new WaitUntil(() => Vector3.Distance(human[i].transform.position, human[i - 1].transform.position) >= thresholdDistance);
            human[i].GetComponent<HumanBehaviour>().Arriving(human[i - 1].transform.position);

        }

        isFollowing = false;
    }

    private void FlashLight(bool lightOn)
    {
        if (lightOn)
        {
            flashlight.gameObject.SetActive(true);
            PlayerStack.PlayerBattery(maxBattery);
        }
        else
        {
            flashlight.gameObject.SetActive(false);
        }
    }

    public void AddingHuman()
    {
        float x = human[human.Count - 1].transform.position.x;
        float y = human[human.Count - 1].transform.position.y;
        float distance = human[human.Count - 1].GetComponent<HumanBehaviour>().circleColliderRadius * 2;

        Vector3 directVec = forwardPosition.position - human[human.Count - 1].transform.position;
        directVec = -directVec.normalized * distance;


        Vector3 nextPosHuman = new Vector3(x, y, 0) + directVec;
        var humanInstantiate = Instantiate(humanPrefab, nextPosHuman, Quaternion.identity);
        humanInstantiate.transform.parent = this.transform;
        human.Add(humanInstantiate);
    }

}
