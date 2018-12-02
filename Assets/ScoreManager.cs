using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	[HideInInspector] public int playerScore = 0;
    public Text scoreText;
    public static ScoreManager instance;
    private PlayerBehaviour player;
    private PlayerParty playerParty;
	void Start ()
    {
        instance = this;
        player = FindObjectOfType<PlayerBehaviour>();
        playerParty = player.GetComponent<PlayerParty>();
        StartCoroutine(CountScore());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    
    IEnumerator CountScore()
    {
        while(GetComponent<GameManager>().isGameOver == false)
        {
            yield return new WaitForSeconds(1);
            playerScore += 10 + (2 * playerParty.humanCount);
            scoreText.text = playerScore.ToString();
            if (playerScore > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", playerScore);
                //highScoreText.text = currentScore.ToString();
            }
​
        }
    }
}
