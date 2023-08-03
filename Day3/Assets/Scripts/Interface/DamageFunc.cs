using UnityEngine;

public class DamageFunc : MonoBehaviour
{
    [SerializeField] Health health;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable target = collision.GetComponent<IDamageable>();
        target?.TakeDamage(health.damage);
    }
}
