using System.Collections;
using UnityEngine;

public class StreetLight : MonoBehaviour
{

    public GameObject itemInside;

    [SerializeField] private float reduceFactor;
    [SerializeField] private float circleColliderRadius;
    private bool isInside;
    private Vector3 currentScale;
    public float duration = 5.0f;
    private float startTime;
    private SpriteRenderer sprite;
    void Start()
    {
        startTime = Time.time;
        sprite = GetComponent<SpriteRenderer>();

        gameObject.GetComponent<CircleCollider2D>().radius = circleColliderRadius;
        currentScale = transform.localScale;
    }

    void Fading()
    {
        float t = (Time.time - startTime) / duration;
        sprite.color = new Color(1f, 1f, 1f, 1 - Mathf.SmoothStep(0, 1, t));
    }


    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerBehaviour>())
        {
            isInside = true;
            StartCoroutine(ScaleDown());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerBehaviour>())
        {
            isInside = false;
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
        if (transform.localScale.x <= 0)
        {
            Destroy(gameObject);
        }

    }



}
