using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControllArm : MonoBehaviour {
    public GameObject armR;
    public GameObject armL;
    public GameObject armLR;
    public GameObject axisR;
    public GameObject axitL;
    public GameObject axitLight;
    private int choseArm = 1;
	// Use this for initialization
	void Start () {
        choseArm = PlayerPrefs.GetInt("ChoseArm", 0);
        CheckStartInput();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InputArmRight()
    {
        

            armR.gameObject.SetActive(true);
            armL.gameObject.SetActive(false);
            armLR.gameObject.SetActive(false);
            axisR.gameObject.SetActive(true);
            axitL.gameObject.SetActive(false);
            axitLight.gameObject.SetActive(false);
            PlayerPrefs.SetInt("ChoseArm", 2);

    }

    public void InputArmLeft()
    {
        

            armR.gameObject.SetActive(false);
            armL.gameObject.SetActive(true);
            armLR.gameObject.SetActive(false);
            axisR.gameObject.SetActive(false);
            axitL.gameObject.SetActive(true);
            axitLight.gameObject.SetActive(false);
            PlayerPrefs.SetInt("ChoseArm", 0);
  
    }

    public void InputArmLR()
    {
        

            armR.gameObject.SetActive(false);
            armL.gameObject.SetActive(false);
            armLR.gameObject.SetActive(true);
            axisR.gameObject.SetActive(false);
            axitL.gameObject.SetActive(true);
            axitLight.gameObject.SetActive(true);
            PlayerPrefs.SetInt("ChoseArm", 1);
  
    }
    private void CheckStartInput()
    {
        if (choseArm == 2)
            InputArmRight();
        if (choseArm == 0)
            InputArmLeft();
        if (choseArm == 1)
            InputArmLR();
    }
}
