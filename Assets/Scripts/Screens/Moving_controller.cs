using UnityEngine;

public class Moving_controller : MonoBehaviour
{
    public Transform platform;
    public Transform leftEdge;
    public Transform rightEdge;

    public float speed = 1.5f;

    int direction = 1;

    private void FixedUpdate()
    {
        Vector2 target = currentMovementTarget();
        Vector2 newPosition = Vector2.MoveTowards(platform.position, target, speed * Time.fixedDeltaTime);
        platform.GetComponent<Rigidbody2D>().MovePosition(newPosition);

        if ((target - (Vector2)platform.position).sqrMagnitude < 0.01f)
            direction *= -1;
    }

    Vector2 currentMovementTarget()
    {
        if (direction == 1)
        {
            return rightEdge.position;
        }

        return leftEdge.position;
    }
}
