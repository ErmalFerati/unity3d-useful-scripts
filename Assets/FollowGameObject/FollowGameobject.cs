using UnityEngine;

public class FollowGameobject : MonoBehaviour
{
    public Transform target;

    public float speed = 1.0f;

    public bool x = true;
    public bool y = true;
    public bool z = true;

    private Vector3 offset;

    private void Start()
    {
        offset = target.position - transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,
            new Vector3(
                x == true ? target.position.x + offset.x : transform.position.x,
                y == true ? target.position.y + offset.y : transform.position.y,
                z == true ? target.position.z + offset.z : transform.position.z
            ), speed * Time.deltaTime);
    }
}