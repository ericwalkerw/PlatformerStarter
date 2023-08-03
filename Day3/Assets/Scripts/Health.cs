using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDamageable
{
    public int maxHP, damage;
    public Image hpBar;

    private int currentHP;
    private void Start()
    {
        currentHP = maxHP;
    }
    public void TakeDamage(int damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);
        Debug.Log($"{transform.name} : {currentHP}");
        UpdateHPBar();
        if (currentHP > 0)
        {

        }
        else
        {

        }
    }

    private void UpdateHPBar()
    {
        float fillAmount = (float)currentHP / maxHP;
        hpBar.DOFillAmount(fillAmount, 0.3f);

    }
}
