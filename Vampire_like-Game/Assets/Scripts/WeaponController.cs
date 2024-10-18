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

    public bool canUseGarlic;
    public bool canUseHolyWater;

    public int stakeLevel;
    public int garlicLevel;
    public int holyWaterLevel;

    public int maxLevel;

    [SerializeField] private StakeInformation stakeInfo;
    [SerializeField] private GarlicInformation garlicInfo;
    [SerializeField] private HolyWaterInformation holyWaterInfo;

    void Start()
    {
        isStakeAttacking = false;
        isGarlicInUse = false;
        currentHolyWaterAttackTime = holyWaterCoolDownTimer;
        isHolyWaterInUse = true;

        canUseGarlic = false;
        canUseHolyWater = false;

        stakeLevel = 0;
        garlicLevel = 0;
        holyWaterLevel = 0;
        maxLevel = 2;
    }

void Update()
    {
        // These commented lines are for testing purposes only.
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpgradeStake();
            if (!canUseGarlic || !canUseHolyWater)
            {
                EquipGarlic();
                EquipHolyWater();
            }
            else
            {
                UpgradeGarlic();
                UpgradeHolyWater();
            }
        }
        */

        StakeAttackController();

        if (canUseGarlic)
        {
            GarlicAttackController();
        }
        if (canUseHolyWater)
        {
            HolyWaterAttackController();
        }
    }

    private void StakeAttackController()
    {
        if (!isStakeAttacking)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
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
        projectile.SetLevel(stakeLevel);
        projectile.SetInfo(stakeInfo);
        projectile.Fire(projectileSpeed, dir.normalized);
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
        projectile.SetLevel(garlicLevel);
        projectile.SetInfo(garlicInfo);
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
        projectile.SetLevel(holyWaterLevel);
        projectile.SetInfo(holyWaterInfo);
        projectile.Fire(holyWaterProjectileSpeed, dir);
    }

    public int UpgradeWeaponLevel(int level)
    {
        if (level < maxLevel)
        {
            return level + 1;
        }
        else
        {
            return level;
        }
    }

    public void UpgradeStake()
    {
        stakeLevel = UpgradeWeaponLevel(stakeLevel);
        Debug.Log("The stakes leveled up.");
    }
    public void UpgradeGarlic()
    {
        garlicLevel = UpgradeWeaponLevel(garlicLevel);
        Debug.Log("The garlic leveled up.");
    }
    public void UpgradeHolyWater()
    {
        holyWaterLevel = UpgradeWeaponLevel(holyWaterLevel);
    }

    public void EquipGarlic()
    {
        canUseGarlic = true;
    }

    public void EquipHolyWater()
    {
        canUseHolyWater = true;
    }
}
