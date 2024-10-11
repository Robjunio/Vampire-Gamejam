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

    void Start()
    {
        isStakeAttacking = false;
    }

    void Update()
    {
        StakeAttackController();
    }

    /*
    private GameObject FindChildWithTag(GameObject parent, string tag)
    {
        GameObject child = null;

        foreach (Transform transform in parent.transform)
        {
            if (transform.CompareTag(tag))
            {
                child = transform.gameObject;
                break;
            }
        }

        return child;
    }
    */

    private void StakeAttackController()
    {
        if (!isStakeAttacking)
        {
            if (Input.GetMouseButtonDown(0))
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
        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        var position = transform.position + transform.forward;
        var rotation = transform.rotation;
        var projectile = Instantiate(projectilePrefab, position, rotation);
        projectile.Fire(projectileSpeed, dir);
    }
}
