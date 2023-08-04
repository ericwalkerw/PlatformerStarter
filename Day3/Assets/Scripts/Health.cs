using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDamageable
{
    public int maxHP, damage;
    public Image hpBar;

    private int currentHP;
    private bool isBuffed = false;
    Animator anim;

    public UnityEvent OnDeath;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        currentHP = maxHP;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.H))
        {
            BuffHealth();
        }
    }
    public void TakeDamage(int damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);
        UpdateHPBar();
        if (currentHP > 0)
        {
            anim.SetTrigger(AnimationString.isHit);
        }
        else
        {
            anim.SetTrigger(AnimationString.isDead);
            OnDeath.Invoke();
        }
    }

    private void UpdateHPBar()
    {
        float fillAmount = (float)currentHP / maxHP;
        hpBar.DOFillAmount(fillAmount, 0.3f);
    }

    public void BuffHealth()
    {
        currentHP = Mathf.Clamp(currentHP + 5, 0, maxHP);
        UpdateHPBar();
    }

    public void BuffDamage()
    {
        if (!isBuffed)
        {
            StartCoroutine(AddDamage());
        }
    }

    private IEnumerator AddDamage()
    {
        isBuffed = true;
        int baseDamge = damage;
        damage += 5;
        yield return new WaitForSeconds(5);

        damage = baseDamge;
        isBuffed = false;
    }
}
