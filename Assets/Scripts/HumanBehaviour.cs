using System.Collections;
using UnityEngine;

public class HumanBehaviour : MonoBehaviour
{
    public float movementThreshold = 0.5f;
    public float circleColliderRadius;
    private Rigidbody2D rigid;
    private Vector3 smoothVelocity;
    [HideInInspector] public StreetLight underStreetLight;

    private bool isDestroying = false;
    // Use this for initialization
    void Start()
    {
        isDestroying = false;
        rigid = GetComponent<Rigidbody2D>();
        gameObject.GetComponent<CircleCollider2D>().radius = circleColliderRadius;
    }

    public void MoveToward(Vector3 target)
    {
        if (Vector3.Distance(transform.position, target) > movementThreshold)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target, ref smoothVelocity, 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<EnemyBehaviour>() && !isDestroying)
        {
            StartCoroutine(HumanDestroying(other.gameObject));
        }

    }

    private IEnumerator HumanDestroying(GameObject enemy)
    {
        // TODO: this is hard code #_#
        isDestroying = true;
        enemy.GetComponent<EnemyBehaviour>().enabled = false;
        this.enabled = false;

        var timeSlash = 2f;
        var sprite = this.gameObject.GetComponent<SpriteRenderer>();
        var newColor = new Color(214, 210, 218, 255);
        var prevColor = sprite.color;
        var enemyColor = enemy.GetComponent<SpriteRenderer>().color;

        sprite.color = newColor;
        enemy.GetComponent<SpriteRenderer>().color = newColor;
        yield return new WaitForSeconds(0.4f);
        sprite.color = prevColor;
        enemy.GetComponent<SpriteRenderer>().color = enemyColor;
        yield return new WaitForSeconds(0.4f);
        sprite.color = newColor;
        enemy.GetComponent<SpriteRenderer>().color = newColor;
        yield return new WaitForSeconds(0.4f);
        sprite.color = prevColor;
        enemy.GetComponent<SpriteRenderer>().color = enemyColor;
        yield return new WaitForSeconds(0.4f);
        enemy.GetComponent<SpriteRenderer>().color = newColor;
        sprite.color = newColor;
        
        yield return new WaitForSeconds(1f);
        FindObjectOfType<PlayerParty>().RemoveLastMember();
        FindObjectOfType<PlayerParty>().numberPartyText.text = FindObjectOfType<PlayerParty>().humans.Count.ToString();

        EnemyManager.instance.listEnemy.Remove(enemy.gameObject);
        Destroy(enemy.gameObject);
    }
}
