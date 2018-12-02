using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject canvasGame;
    public GameObject canvasEnd;
    public bool isGameOver = false;
    public static GameManager instance;
    public GameObject fog;
    public GameObject playerLight;

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
        ScoreManager.instance.StopAllCoroutines();
        canvasGame.gameObject.SetActive(false);
        canvasEnd.gameObject.SetActive(true);
        isGameOver = true;
        GetComponent<StreetLightManager>().enabled = false;
        fog.GetComponent<SpriteRenderer>().color = Color.gray;
        for (int i = 0; i < EnemyManager.instance.listEnemy.Count; i++)
        {
            EnemyManager.instance.listEnemy[i].GetComponent<SpriteRenderer>().enabled = true;
        }

        StreetLight[] lights = GameObject.FindObjectsOfType<StreetLight>();

        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].GetComponentInChildren<SpriteRenderer>().gameObject.SetActive(false);
        }
        
        playerLight.SetActive(false);

    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
