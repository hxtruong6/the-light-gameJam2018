using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed;
    public GameObject flashLightObject;
    public bool lightOn;
    public int maxFear;
    public int numberHumanNotFear;

    [SerializeField] public Transform forwardPosition;
    private Rigidbody2D rb2d;
    private FlashLight flashLight;
    private PlayerParty playerParty ;

    // Use this for initialization
    private void Start()
    {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        flashLight = GetComponent<FlashLight>();
        playerParty = GetComponent<PlayerParty>();

    }

  

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.instance.IsGameOver()) return;
        UpdateMovement();
        UpdateFellowMovement();

        if (Input.GetKeyDown(KeyCode.Space) || SimpleInput.GetButtonDown("Light"))
        {
            if (flashLight.LifeBattery())
            {
                lightOn = !lightOn;
            }

        }
        FlashLightToggle(lightOn);
        PlayerStack.instance.PlayerFear(numberHumanNotFear, maxFear);
        if (PlayerStack.LifeFear())
        {
            GameManager.instance.EndGame();
        }

        if(lightOn)
        {
            flashLight.PlayerBattery(flashLight.maxBattery);
        }
    }

    private void UpdateMovement()
    {
        var xDir = SimpleInput.GetAxis("Horizontal") + Input.GetAxisRaw("Horizontal");
        var yDir = SimpleInput.GetAxis("Vertical") + Input.GetAxisRaw("Vertical");
        Vector3 moveInput = new Vector3(xDir, yDir, 0).normalized;
        var moveVelocity = moveInput * speed * Time.deltaTime;

        rb2d.transform.position += moveVelocity;

        var xRotate = SimpleInput.GetAxis("HorizontalRotate");
        var yRotate = SimpleInput.GetAxis("VerticalRotate");
        Vector3 rotateInput = new Vector3(xRotate, yRotate, 0).normalized;

        if (moveVelocity != Vector3.zero && rotateInput * speed * Time.deltaTime == Vector3.zero)
        {
            float rot_z = Mathf.Atan2(moveInput.normalized.y, moveInput.normalized.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }
        else if (rotateInput * speed * Time.deltaTime != Vector3.zero)
        {
            float rot_z = Mathf.Atan2(rotateInput.normalized.y, rotateInput.normalized.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }
    }

    private void UpdateFellowMovement()
    {
        if (playerParty.humans.Count == 0) return;
        for (int i = 0; i < playerParty.humans.Count - 1; i++)
        {
            playerParty.humans[i + 1].GetComponent<HumanBehaviour>().MoveToward(playerParty.humans[i].transform.position);
        }
    }

    private void FlashLightToggle(bool lightOn)
    {
        if (lightOn)
        {
            flashLightObject.gameObject.SetActive(true);       
        }
        else
        {
            flashLightObject.gameObject.SetActive(false);
            List<GameObject> enemies = EnemyManager.instance.listEnemy;
            if(enemies.Count > 0)
            {
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
    }


}
