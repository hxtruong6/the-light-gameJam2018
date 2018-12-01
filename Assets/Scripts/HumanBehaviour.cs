using UnityEngine;

public class HumanBehaviour : MonoBehaviour
{
    public float movementThreshold = 0.5f;
    public float circleColliderRadius;
    private Rigidbody2D rigid;
    private Vector3 smoothVelocity;

    // Use this for initialization
    void Start()
    {
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
        // TODO: Cant not collider
        print("Other: " + other.name);
        var enemy = other.gameObject.GetComponent<EnemyBehaviour>();
        if (enemy)
        {
            //GameObject.FindObjectOfType<PlayerBehaviour>().CutMembers(this);
            //FindObjectOfType<PlayerBehaviour>().humans.Remove(this);
            FindObjectOfType<PlayerBehaviour>().RemoveLastMember();
            Destroy(enemy.gameObject);
            //Destroy(this.gameObject);
        }

    }
}
