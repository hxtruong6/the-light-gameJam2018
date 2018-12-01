using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject canvasGame;
    public GameObject canvasEnd;
    public bool isGameOver = false;
    public static GameManager instance;
    void Awake()
    {
        
    }
    // Use this for initialization
    void Start()
    {
        instance = this;
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EndGame()
    {
        canvasGame.gameObject.SetActive(false);
        canvasEnd.gameObject.SetActive(true);
        isGameOver = true;
        GetComponent<StreetLightManager>().enabled = false;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
