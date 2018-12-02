using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasCountStart : MonoBehaviour
{
    public Text countDownText;
    public GameObject splash;
    public GameObject canvasMainMenu;
    public GameObject canvasGame;
    public GameObject highScorePanel;
    private int currentCountDown;
    // Use this for initialization
    void Start()
    {
        highScorePanel.gameObject.SetActive(false);
        canvasMainMenu.gameObject.SetActive(false);
        StartCoroutine(CountDownText());
        currentCountDown = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator CountDownText()
    {
        //while (GetComponent<GameManager>().isGameOver == false)
        //{
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSecondsRealtime(1);
            currentCountDown -= 1;
            countDownText.text = currentCountDown.ToString();
            if (currentCountDown == 0)
            {
                canvasGame.gameObject.SetActive(true);
                splash.gameObject.SetActive(false);
                Time.timeScale = 1;
                this.gameObject.SetActive(false);
            }
        }
        //}
    }
}
