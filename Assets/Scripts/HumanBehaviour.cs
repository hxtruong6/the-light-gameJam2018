using UnityEngine;

public class HumanBehaviour : MonoBehaviour
{
    public float circleColliderRadius;
    [SerializeField] private float maxDistanceWithThePrevious;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxForce;

    private Rigidbody2D rigid;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        gameObject.GetComponent<CircleCollider2D>().radius = circleColliderRadius;
    }


    public void Arriving(Vector3 target)
    {
        rigid.position = target;
        return;

        Vector3 desired = (target - this.transform.position);

        //The distance is the magnitude of the vector pointing from location to target.
        float distance = desired.magnitude;
        desired = desired.normalized;
        if (distance < maxDistanceWithThePrevious)
        {
            //...set the magnitude according to how close we are.
            //float m = map(d, 0, 100, 0, maxspeed);
            float m = (distance / maxDistanceWithThePrevious) * maxSpeed;
            desired = desired * m;
        }
        else
        {
            //Otherwise, proceed at maximum speed.
            desired = desired * maxSpeed;
        }
        rigid.transform.position = desired;

        //The usual steering = desired - velocity
        //Vector3 steer = desired - rigid.velocity;
        //steer = Vector2.ClampMagnitude(steer, maxForce);
        //rigid.AddForce(steer);
    }


}
