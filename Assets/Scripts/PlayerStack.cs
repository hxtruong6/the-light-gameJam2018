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
    private int currentFear;

    private void Awake()
    {
        playerStack = this;
    }
    // Use this for initialization
    void Start () {
        currentFear = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    static public void PlayerFear(int humanNotFear, int maxFear)
    {
        playerStack.humanCount = playerStack.player.GetComponent<PlayerBehaviour>().human.Count;
        if (playerStack.humanCount < humanNotFear)
        {
            if (playerStack.currentFear < maxFear)
            {

            }
        }
    }
}
