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

    [SerializeField] private HumanBehaviour humanPrefab;
    [System.NonSerialized] public List<HumanBehaviour> humans = new List<HumanBehaviour>();
    [SerializeField] private Transform forwardPosition;
    private Rigidbody2D rb2d;
    private bool isDead;
   
    // Use this for initialization
    private void Start()
    {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();

        var humInstantiate = Instantiate(humanPrefab, transform.position, transform.rotation, transform);
        humans.Add(humInstantiate);
        for (int i = 0; i < 10; i++)
        {
            AddNewFellows();
        }
    }

    private void AddNewFellows()
    {
        float x = humans[humans.Count - 1].transform.position.x;
        float y = humans[humans.Count - 1].transform.position.y;
        float distance = humans[humans.Count - 1].circleColliderRadius * 2;

        Vector3 directVec = forwardPosition.position - humans[humans.Count - 1].transform.position;
        directVec = -directVec.normalized * distance;

        Vector3 nextPosHuman = new Vector3(x, y, 0) + directVec;
        var humanInstantiate = Instantiate(humanPrefab, nextPosHuman, Quaternion.identity);
        humans.Add(humanInstantiate);
    }

    // Update is called once per frame
    private void Update()
    {
        if (isDead) return;

        UpdateMovement();
        UpdateFellowMovement();

        if (Input.GetKeyDown(KeyCode.Space) || SimpleInput.GetButtonDown("Light"))
        {
            lightOn = !lightOn;

        }
        PlayerStack.PlayerFear(numberHumanNotFear, maxFear);
        FlashLight(lightOn);
    }

    private void UpdateMovement()
    {
        var xDir = SimpleInput.GetAxis("Horizontal") + Input.GetAxis("Horizontal");
        var yDir = SimpleInput.GetAxis("Vertical") + Input.GetAxis("Vertical");
        Vector3 moveInput = new Vector3(xDir, yDir, 0).normalized;
        var moveVelocity = moveInput * speed * Time.deltaTime;

        rb2d.transform.position += moveVelocity;

        if (moveVelocity != Vector3.zero)
        {
            float rot_z = Mathf.Atan2(moveInput.normalized.y, moveInput.normalized.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }
    }

    private void UpdateFellowMovement()
    {
        for (int i = 0; i < humans.Count - 1; i++)
        {
            humans[i + 1].MoveToward(humans[i].transform.position);
        }
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
}
