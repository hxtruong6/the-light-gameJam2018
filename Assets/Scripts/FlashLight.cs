using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLight : MonoBehaviour {

    public Image batteryValue;
    public int maxBattery;
    [SerializeField] private int damageBattery = 2;
    private float timerBattery;
    private float currentBattery;

    public float GetCurrentFlashLightBattery() { return currentBattery; }
    public void SetCurrentFlashLightBattery(int value) { currentBattery = Mathf.Clamp(value, 0f, maxBattery); }
    public int GetMaxBattery() { return maxBattery; }
    void Start () {
        currentBattery = maxBattery;
    }
	
	void Update () {
        if (GameManager.instance.IsGameOver()) return;

        if(currentBattery > 0 && GetComponent<PlayerBehaviour>().flashLightObject.activeSelf == false)
        {
            GetComponent<PlayerBehaviour>().flashLightObject.SetActive(true);
        }
        batteryValue.fillAmount = currentBattery / maxBattery;
        LifeBattery();
    }

    public void PlayerBattery(int maxBattery)
    {
        if (currentBattery >= 0 &&  currentBattery <= maxBattery)
        {
            if ( timerBattery <= 0)
            {
                 currentBattery -=  damageBattery;

                 timerBattery = 2f;
            }
            else  timerBattery -= Time.deltaTime;
        }
    }

    public bool LifeBattery()
    {
        if ( currentBattery <= 0)
        {
            GetComponent<PlayerBehaviour>().lightOn = false;
            return false;
        }
        else return true;
    }
}
