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
    public AudioClip h1;
    public AudioClip h2;
    public AudioClip h3;
    private AudioSource audioSource;

    [SerializeField] private int damageFear = 6;
    [SerializeField] private float scareFactor = 0.3f;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        currentFear = 0;
        playerParty = FindObjectOfType<PlayerBehaviour>().GetComponent<PlayerParty>();
    }


    void Update()
    {
        if (GameManager.instance.IsGameOver()) return;
        LifeFear();
        
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

    public static bool LifeFear()
    {
        if (instance.currentFear >= instance.player.GetComponent<PlayerBehaviour>().maxFear)
            return true;
        else
            return false;
    }

    private void AudioHeart(float currentfear)
    {
        audioSource.Play();
        audioSource.volume = currentFear / player.GetComponent<PlayerBehaviour>().maxFear;
        //audioSource.clip.
        if ((currentFear / player.GetComponent<PlayerBehaviour>().maxFear ) < 0.3)
        {
            if (audioSource.clip != h1)
            {
                audioSource.Stop();
                audioSource.clip = h1;
            } 
        }
        else if ((currentFear / player.GetComponent<PlayerBehaviour>().maxFear) >= 0.8)
        {
            if (audioSource.clip != h3)
            {
                audioSource.Stop();
                audioSource.clip = h3;
            }
        }
        else
        {
            if (!audioSource.isPlaying && audioSource.clip != h2)
            {
                audioSource.Stop();
                audioSource.clip = h2;
            }
        }


        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
