using DG.Tweening;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float Delay;
    private bool canDamage = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canDamage)
        {
            IDamageable target = collision.GetComponent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(1);

                canDamage = false;
                DOTween.Sequence().AppendInterval(Delay).OnComplete(() => canDamage = true);
            }
        }
    }
}
