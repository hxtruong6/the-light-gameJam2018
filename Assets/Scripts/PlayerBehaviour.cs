using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerBehaviour : MonoBehaviour {
    public float speed;
    public GameObject flashlight;
    public bool lightOn;
    public List<GameObject> human;
    private Rigidbody2D rb2d;
    private bool isDead;
    

	// Use this for initialization
	void Start () {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
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
