using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public Text highScore;
    public GameObject canvasGame;
    public GameObject panelHighScore;
    public GameObject panelHelp;
    public GameObject countdownCanvas;
	// Use this for initialization
	void Start () {
        Time.timeScale = 0;
        canvasGame.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ViewHighScore()
    {
        panelHighScore.gameObject.SetActive(true);
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
    public void ViewHelpPanel()
    {
        panelHelp.gameObject.SetActive(true);
    }
    public void Back()
    {
        panelHelp.gameObject.SetActive(false);
    }
    public void StartButtonGame()
    {
        countdownCanvas.gameObject.SetActive(true);
    }
}
