using System.Collections;
using UnityEngine;

public class StreetLightScaleDown : MonoBehaviour
{

    public GameObject streetLigthEffect;
    public GameObject streetLigthEnd;
    [SerializeField] private float timeDestroy = 4f;
    private ParticleSystem lightEnd;

    private bool isPlayingEndParticle;

    void Start()
    {
        lightEnd = streetLigthEnd.GetComponentInChildren<ParticleSystem>();
        if (lightEnd == null)
        {
            print("No light end particle");
        }
        isPlayingEndParticle = false;
        // Set duration play particle
        lightEnd.Stop();
        var main = lightEnd.main;
        main.duration = timeDestroy;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //print("on street light scale down: " + other.name);
        if (other.GetComponent<PlayerBehaviour>())
        {
            if (!isPlayingEndParticle)
            {
                StartCoroutine(PlayScaleDownPariticle());
            }
        }
    }

    private IEnumerator PlayScaleDownPariticle()
    {
        isPlayingEndParticle = true;
        streetLigthEnd.gameObject.SetActive(true);
        StartCoroutine(WaitToDestroyStreetLightEffect());
        yield return new WaitForSeconds(timeDestroy);
        Destroy(this.gameObject);
    }
    
    private IEnumerator WaitToDestroyStreetLightEffect()
    {
        yield return new WaitForSeconds(1.5f);
        streetLigthEffect.gameObject.SetActive(false);

    }
}
