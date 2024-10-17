using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private float stakeAttackCoolDownTimer;
    [SerializeField] private float currentStakeAttackTime;
    [SerializeField] private bool isStakeAttacking;

    [SerializeField] private float projectileSpeed;
    [SerializeField] private StakeProjectile projectilePrefab;

    [SerializeField] private float garlicCoolDownTimer;
    [SerializeField] private float currentGarlicAttackTime;
    [SerializeField] private bool isGarlicInUse;

    [SerializeField] private GarlicProjectile garlicProjectilePrefab;

    [SerializeField] private float holyWaterCoolDownTimer;
    [SerializeField] private float currentHolyWaterAttackTime;
    [SerializeField] private bool isHolyWaterInUse;

    [SerializeField] private float holyWaterProjectileSpeed;
    [SerializeField] private HolyWaterProjectile holyWaterProjectilePrefab;

    void Start()
    {
        isStakeAttacking = false;
        isGarlicInUse = false;
        currentHolyWaterAttackTime = holyWaterCoolDownTimer;
        isHolyWaterInUse = true;
    }

    void Update()
    {
        StakeAttackController();
        GarlicAttackController();
        HolyWaterAttackController();
    }

    private void StakeAttackController()
    {
        if (!isStakeAttacking)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isStakeAttacking = true;
                StakeAttack();
                currentStakeAttackTime = stakeAttackCoolDownTimer;
            }
        }

        else
        {
            if (currentStakeAttackTime > 0)
            {
                currentStakeAttackTime -= Time.deltaTime;
            }
            else
            {
                isStakeAttacking = false;
            }
        }
    }

    private void StakeAttack()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        var position = transform.position + transform.forward;
        float lookAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.Euler(0, 0, lookAngle);

        var projectile = Instantiate(projectilePrefab, position, rotation);
        projectile.Fire(projectileSpeed, dir);
    }

    private void GarlicAttackController()
    {
        if (!isGarlicInUse)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                isGarlicInUse = true;
                GarlicAttack();
                currentGarlicAttackTime = garlicCoolDownTimer;
            }
        }
        else
        {
            if (currentGarlicAttackTime > 0)
            {
                currentGarlicAttackTime -= Time.deltaTime;
            }
            else
            {
                isGarlicInUse = false;
            }
        }
    }

    private void GarlicAttack()
    {
        var position = transform.position + transform.forward;

        var projectile = Instantiate(garlicProjectilePrefab, position, transform.rotation);
    }

    private void HolyWaterAttackController()
    {
        if (!isHolyWaterInUse)
        {
            isHolyWaterInUse = true;
            HolyWaterAttack();
            currentHolyWaterAttackTime = holyWaterCoolDownTimer;
        }
        else
        {
            if (currentHolyWaterAttackTime > 0)
            {
                currentHolyWaterAttackTime -= Time.deltaTime;
            }
            else
            {
                isHolyWaterInUse = false;
            }
        }
    }

    private void HolyWaterAttack()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        var position = transform.position + transform.forward;

        var projectile = Instantiate(holyWaterProjectilePrefab, position, transform.rotation);
        projectile.Fire(holyWaterProjectileSpeed, dir);
    }
}
