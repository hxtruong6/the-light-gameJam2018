using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // TODO: This class need to be a static class 
    private Vector3 cameraOffset;

    [Range(0.01f, 1.0f)]
    public float smoothFactor;
    public float minX, maxX, minY, maxY;
    public float thresholdClampX = 1f;
    public float thresholdClampY = 2f;
    // TODO: thresholdClampf x,y will be calculate follow maxX and maxY
    GameObject player;
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerBehaviour>().gameObject;
        cameraOffset = transform.position - player.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsGameOver())
            return;

        Vector3 newPos = player.transform.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
        transform.LookAt(player.transform);
        Vector3 checkedPos = transform.position;
        checkedPos.x = Mathf.Clamp(transform.position.x, minX + thresholdClampX, maxX - thresholdClampX);
        checkedPos.y = Mathf.Clamp(transform.position.y, minY + thresholdClampY, maxY - thresholdClampY);
        transform.position = checkedPos;

    }
}
