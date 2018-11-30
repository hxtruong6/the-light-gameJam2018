using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStack : MonoBehaviour {
    public Image fearValue;
    public Image batteryValue;
    public GameObject player;
    static PlayerStack playerStack;
    private int humanCount;
    private float currentFear;
    private float currentBattery;
    [SerializeField] private float timerFear;
    [SerializeField] private float timerBattery;
    [SerializeField] private int damageFear = 6;
    [SerializeField] private int damageBattery = 2;

    private void Awake()
    {
        playerStack = this;
    }
    // Use this for initialization
    void Start () {
        currentFear = 0;
        currentBattery = player.GetComponent<PlayerBehaviour>().maxBattery;
	}
	
	// Update is called once per frame
	void Update () {
        batteryValue.fillAmount = currentBattery / player.GetComponent<PlayerBehaviour>().maxBattery;
    }

    public static void PlayerFear(int numberHumanNotFear, int maxFear)
    {
        playerStack.humanCount = playerStack.player.GetComponent<PlayerBehaviour>().human.Count;
        if (playerStack.humanCount < numberHumanNotFear)
        {
            if (playerStack.currentFear < maxFear && playerStack.currentFear >= 0 && playerStack.humanCount < numberHumanNotFear)
            {
                if (playerStack.timerFear <= 0)
                {
                    playerStack.currentFear += playerStack.damageFear - playerStack.humanCount;
                    playerStack.fearValue.fillAmount = playerStack.currentFear / maxFear;
                    playerStack.timerFear = 1f;
                }
                else playerStack.timerFear -= Time.deltaTime;
            }
        }
    }

    public static void PlayerBattery(int maxBattery)
    {
        if (playerStack.currentBattery >= 0 && playerStack.currentBattery <= maxBattery)
        {
            if (playerStack.timerBattery <= 0)
            {
                playerStack.currentBattery -= playerStack.damageBattery;
                
                playerStack.timerBattery = 2f;
            }
            else playerStack.timerBattery -= Time.deltaTime;
        }
    }
}
