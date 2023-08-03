using UnityEngine;

public enum EnemyDirection
{
    Left,
    Right
}

public class EnemyControl : MonoBehaviour
{
    [SerializeField] protected EnemyDirection direction;
    [SerializeField] protected float speed = 2f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;

    protected float distanceToMove;
    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        if (distanceToMove <= 0)
        {
            GetDirection();
            distanceToMove = Random.Range(8, 11);
        }

        Vector3 moveDirection = direction == EnemyDirection.Left ? Vector3.left : Vector3.right;
        rb.MovePosition(transform.position + moveDirection * Time.fixedDeltaTime * speed);
        distanceToMove -= Time.fixedDeltaTime * speed;
        AnimUpdate();
    }

    protected virtual void GetDirection()
    {
        direction = direction == EnemyDirection.Left ? EnemyDirection.Right : EnemyDirection.Left;
    }

    void AnimUpdate()
    {

    }
}
