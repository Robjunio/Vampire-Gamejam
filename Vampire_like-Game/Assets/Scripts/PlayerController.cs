using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float MaxHealth;

    private float currentHealth;
    private float xp;
    private bool invunerable;

    private void Start()
    {
        xp = 0f;
        currentHealth = MaxHealth;
    }

    private void GetXP(GameObject obj)
    {
        xp += 10;
    }

    private void GetHit(float damage)
    {
        if (invunerable)
        {
            return;
        }

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, MaxHealth);

        if(currentHealth == 0) {
            // Die
        }
        else
        {
            invunerable = true;
            Tweener tweener = Camera.main.transform.DOShakePosition(0.5f).OnComplete(() => { invunerable = false; });
            UIManager.Instance.PlayerGotHit();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.TryGetComponent(out Enemy enemy);
            GetHit(enemy.damage);
        }
    }

    private void OnEnable()
    {
        XP.Collected += GetXP;
    }

    private void OnDisable()
    {
        XP.Collected -= GetXP;
    }
}
