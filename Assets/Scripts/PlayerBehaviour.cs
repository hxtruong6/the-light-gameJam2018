using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SimpleInputNamespace;
public class PlayerBehaviour : MonoBehaviour {
    public float speed;
    public GameObject flashlight;
    public bool lightOn;
    public List<GameObject> human;
    private Rigidbody2D rb2d;
    Vector3 moveVelocity;
    private bool isDead;

    //protected Joystick joystick;
    protected SimpleInput simpleInput;
    

	// Use this for initialization
	void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        //joystick = gameObject.GetComponent<Joystick>();
        simpleInput = gameObject.GetComponent<SimpleInput>();
	}

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        Vector3 moveInput = new Vector3( Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        moveVelocity = moveInput * speed *Time.deltaTime;
        //rb2d.MovePosition(rb2d.transform.position + moveVelocity);
        rb2d.transform.position += moveVelocity;

        if (moveVelocity != Vector3.zero)
        {
            float rot_z = Mathf.Atan2(moveInput.normalized.y, moveInput.normalized.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            lightOn = !lightOn;
            FlashLight(lightOn);
        }
    }

    private void FlashLight(bool lightOn)
    {
        if (lightOn) flashlight.gameObject.SetActive(true);
        else flashlight.gameObject.SetActive(false);
    }

}
