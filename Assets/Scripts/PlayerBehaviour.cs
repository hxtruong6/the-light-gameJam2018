﻿using System.Collections.Generic;
using UnityEngine;
//using SimpleInputNamespace;
public class PlayerBehaviour : MonoBehaviour
{

    public float speed;
    public GameObject flashlight;
    public bool lightOn;

    [SerializeField] private GameObject humanPrefab;
    [HideInInspector] public List<GameObject> human = new List<GameObject>();
    [SerializeField] private Transform forwardPosition;
    private Rigidbody2D rb2d;
    Vector3 moveVelocity;
    private bool isDead;

    //protected Joystick joystick;
    protected SimpleInput simpleInput;

    // Use this for initialization
    void Start()
    {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
         simpleInput = gameObject.GetComponent<SimpleInput>();

        var humInstantiate = Instantiate(humanPrefab, Vector3.zero, Quaternion.identity);
        humInstantiate.transform.parent = this.transform;
        human.Add(humInstantiate);
        for (int i = 0; i < 2; i++)
        {
            AddingHuman();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        moveVelocity = moveInput * speed * Time.deltaTime;
        //rb2d.MovePosition(rb2d.transform.position + moveVelocity);
        rb2d.transform.position += moveVelocity;

        if (moveVelocity != Vector3.zero)
        {
            float rot_z = Mathf.Atan2(moveInput.normalized.y, moveInput.normalized.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }
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
        float distance = human[human.Count - 1].GetComponent<HumanBehaviour>().circleColliderRadius * 2;

        Vector3 directVec = forwardPosition.position - human[human.Count - 1].transform.position;
        directVec = -directVec.normalized * distance;


        Vector3 nextPosHuman = new Vector3(x,y,0) + directVec;
        var humanInstantiate = Instantiate(humanPrefab, nextPosHuman, Quaternion.identity);
        humanInstantiate.transform.parent = this.transform;
        human.Add(humanInstantiate);
    }

}