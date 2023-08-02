using UnityEngine;

public class TouchingObject : MonoBehaviour
{
    public ContactFilter2D contactFilter;
    public float groundDistance = 0.05f;

    CapsuleCollider2D touchingCol;
    Animator anim;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];

    [SerializeField]
    private bool _isGrounded = true;
    public bool IsGrounded
    {
        get => _isGrounded;
        set
        {
            _isGrounded = value;
            anim.SetBool(AnimationString.isGround, value);
        }
    }
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        touchingCol = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0;

    }
}
