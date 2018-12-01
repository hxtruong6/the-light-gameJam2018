﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStack : MonoBehaviour
{
    public Image fearValue;
    public Image batteryValue;
    public GameObject player;
    static PlayerStack playerStack;
    private int humanCount;
    private float currentFear;
    private float currentBattery;
    private float timerFear;
    private float timerBattery;
    [SerializeField] private int damageFear = 6;
    [SerializeField] private int damageBattery = 2;
    [SerializeField] private float scareFactor = 0.3f;

    private void Awake()
    {
        playerStack = this;
    }
    // Use this for initialization
    void Start()
    {
        currentFear = 0;
        currentBattery = player.GetComponent<PlayerBehaviour>().maxBattery;
    }

    // Update is called once per frame
    void Update()
    {
        batteryValue.fillAmount = currentBattery / player.GetComponent<PlayerBehaviour>().maxBattery;
        LifeBattery();
        LifeFear();
    }

    public static void PlayerFear(int numberHumanNotFear, int maxFear)
    {
        playerStack.humanCount = playerStack.player.GetComponent<PlayerBehaviour>().humans.Count;
        if (playerStack.humanCount < numberHumanNotFear)
        {
            if (playerStack.currentFear < maxFear && playerStack.currentFear >= 0 && playerStack.humanCount < numberHumanNotFear)
            {
                if (playerStack.timerFear <= 0)
                {
                    playerStack.currentFear += (playerStack.damageFear - playerStack.humanCount) * playerStack.scareFactor;
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

    public static bool LifeBattery()
    {
        if (playerStack.currentBattery <= 0)
        {
            playerStack.player.GetComponent<PlayerBehaviour>().lightOn = false;
            return false;
        }
        else return true;
    }

    public static bool LifeFear()
    {
        if (playerStack.currentFear >= playerStack.player.GetComponent<PlayerBehaviour>().maxFear) return true;
        else return false;
    }

    public static void IncreaseBattery(int addBattery)
    {
        if (playerStack.currentBattery < playerStack.player.GetComponent<PlayerBehaviour>().maxBattery)
        {
            playerStack.currentBattery += addBattery;
        }
    }
}
