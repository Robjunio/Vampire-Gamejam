using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float MaxHealth;

    private int level;
    private float currentHealth;
    private float xp;
    private bool invunerable;

    float divisorXP = 8.0f;  // Pode ajustar o divisor de XP
    float multiplicador = 1f;  // Ajusta a curva de progressão
    int nivelBase = 1;  // Nível base inicial

    public int CalcularNivel(int xp)
    {
        // Fórmula para calcular o nível baseado em XP
        int nivel = Mathf.FloorToInt(1 + Mathf.Sqrt(xp / divisorXP) * multiplicador);

        return nivel;
    }

    public int HowManyToNextLevel(int level)
    {
        // Fórmula para calcular o nível baseado em XP level - nivelbase
        int xp = Mathf.FloorToInt(((Mathf.Pow(level, 2) - Mathf.Pow(1, 2))/Mathf.Pow(multiplicador,2)) * divisorXP/2);

        return xp;
    } 

    private void Start()
    {
        xp = 0f;
        currentHealth = MaxHealth;

        UIManager.Instance.UpdateXP(HowManyToNextLevel(nivelBase + 1), (int)xp, nivelBase);
    }

    private void GetOrbe(GameObject obj)
    {
        xp += 5;
        currentHealth += 2;

        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
        UIManager.Instance.UpdateLife(MaxHealth, currentHealth);

        AudioManager.Instance.PlaySound(AudioManager.Sound.Potion, transform.position);

        CalculateXP();
    }

    private void GetKillXP(GameObject obj, EnemyInformation enemy)
    {
        xp++; 
        CalculateXP();
    }

    private void CalculateXP()
    {
        var lvl = CalcularNivel((int)xp);
        if (nivelBase < lvl)
        {
            xp -= HowManyToNextLevel(lvl);
            nivelBase = lvl;

            MaxHealth += 20;

            currentHealth += 20;

            currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
            UIManager.Instance.SetPlayerCauntion(currentHealth <= MaxHealth * 0.2f);

            UIManager.Instance.UpdateLife(MaxHealth, currentHealth);

            LevelUpManager.Instance.LevelUPAvalible();
        }

        UIManager.Instance.UpdateXP(HowManyToNextLevel(nivelBase + 1), (int)xp, nivelBase);
    }

    private void GetHit(float damage)
    {
        if (invunerable)
        {
            return;
        }

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, MaxHealth);

        UIManager.Instance.UpdateLife(MaxHealth, currentHealth);

        if(currentHealth == 0) {
            UIManager.Instance.ActivateDefeatPanel();
            AudioManager.Instance.PlaySound(AudioManager.Sound.HeroDying, transform.position);
        }
        else
        {
            invunerable = true;
            Tweener tweener = Camera.main.transform.DOShakePosition(0.5f, 0.5f, 10).OnComplete(() => { invunerable = false; });
            UIManager.Instance.PlayerGotHit();
            UIManager.Instance.SetPlayerCauntion(currentHealth <= MaxHealth * 0.2f);
            AudioManager.Instance.PlaySound(AudioManager.Sound.PlayerHitEffect, transform.position);
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
        XP.Collected += GetOrbe;
        Enemy.Died += GetKillXP;
    }

    private void OnDisable()
    {
        XP.Collected -= GetOrbe;
        Enemy.Died -= GetKillXP;
    }
}
