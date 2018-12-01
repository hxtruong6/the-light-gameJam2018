using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStack : MonoBehaviour
{
    public Image fearValue;
    
    public GameObject player;
    public static PlayerStack instance;
    
    private float currentFear;

    private float timerFear;
    PlayerParty playerParty;

    [SerializeField] private int damageFear = 6;
    [SerializeField] private float scareFactor = 0.3f;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
        currentFear = 0;
        playerParty = FindObjectOfType<PlayerBehaviour>().GetComponent<PlayerParty>();
    }


    void Update()
    {
        if (GameManager.instance.IsGameOver()) return;
        ReachFearLimit();
    }

    public void PlayerFear(int numberHumanNotFear, int maxFear)
    {
        playerParty.humanCount = instance.player.GetComponent<PlayerParty>().humans.Count;
        if (playerParty.humanCount < numberHumanNotFear)
        {
            if (instance.currentFear < maxFear && instance.currentFear >= 0 && playerParty.humanCount < numberHumanNotFear)
            {
                if (instance.timerFear <= 0)
                {
                    instance.currentFear += (instance.damageFear - playerParty.humanCount) * instance.scareFactor;
                    instance.fearValue.fillAmount = instance.currentFear / maxFear;
                    instance.timerFear = 1f;
                }
                else
                    instance.timerFear -= Time.deltaTime;
            }
        }
    } 

    public static bool ReachFearLimit()
    {

        if (instance.currentFear >= instance.player.GetComponent<PlayerBehaviour>().maxFear)
            return true;
        else
            return false;
    }
}
