using System.Collections;
using UnityEngine;

public class StreetLight : MonoBehaviour
{
    public float fadeDuration = 5.0f;
    public float minSpawnObjectTime = 2.5f;
    public float maxSpawnObjectTime = 5f;
    public GameObject[] spawnObjects;
    public bool isOccupied;
    [SerializeField] private float reduceFactor;
    [SerializeField] public float circleColliderRadius;
    [SerializeField] public bool isStreetLightOneTime = false;
    private bool isInside;
    private Vector3 currentScale;  
    private float startTime;
    private SpriteRenderer sprite;
    private float timer;
    

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        gameObject.GetComponent<CircleCollider2D>().radius = circleColliderRadius;
        currentScale = transform.localScale;

        SpawnObjectInside();
        isOccupied = true;
    }

    public void StartCoolDownSpawnItem()
    {
        if(isStreetLightOneTime) return;
        float randomTime = Random.Range(minSpawnObjectTime, maxSpawnObjectTime);
        StartCoroutine(CoolDownSpawnItem(randomTime));
    }

    IEnumerator CoolDownSpawnItem(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnObjectInside();
    }

    void Fading()
    {
        if (sprite.color.a < 0.3f) return;
        float t = 0.5f*(Time.time - startTime) / fadeDuration;
        sprite.color = new Color(1f, 1f, 1f, sprite.color.a - Mathf.SmoothStep(0, 1, t));
    }

    private void SpawnObjectInside()
    {
        if (isOccupied == !isOccupied) return;
        var randomIndex = Random.Range(0, spawnObjects.Length);
        var spawnObject = Instantiate(spawnObjects[randomIndex], transform);
        if (spawnObject.GetComponent<HumanBehaviour>())
        {
            spawnObject.transform.SetParent(null);

            spawnObject.GetComponent<HumanBehaviour>().underStreetLight = this;
        }
        isOccupied = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerBehaviour>())
        {
            startTime = Time.time;
            isInside = true;
            //StartCoroutine(ScaleDown());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerBehaviour>())
        {
            isInside = false;
            //StopCoroutine(ScaleDown());
        }
    }

    IEnumerator ScaleDown()
    {
        float timer = 0;

        while (transform.localScale.x > 0 && isInside)
        {
            timer += Time.deltaTime;
            transform.localScale -= currentScale * Time.deltaTime * reduceFactor;
            Fading();
            yield return null;
        }
        if (transform.localScale.x < 0.5f)
        {
            Destroy(gameObject);
        }
    }
}
