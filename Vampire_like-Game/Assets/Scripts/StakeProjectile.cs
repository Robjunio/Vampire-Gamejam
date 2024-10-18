using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StakeProjectile : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float stakeAutoDestroyTimer;
    [SerializeField] private float currentStakeTime;
    [SerializeField] private bool hasHit;
    private int currentNumHit;

    // --- --- ---
    private StakeInformation Info;
    private int level;

    public int maxNumHit;
    public float damage;
    public float speedMultiplier;
    // --- --- ---

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentStakeTime = stakeAutoDestroyTimer;
        hasHit = false;

        currentNumHit = 0;
    }

    void Update()
    {
        if (currentStakeTime > 0)
        {
            currentStakeTime -= Time.deltaTime;
        }
        else
        {
            AutoDestroy();
        }

        // TO DO
        if (hasHit)
        {
            currentNumHit++;
            if (currentNumHit < maxNumHit)
            {
                hasHit = false;
            }
            else
            {
                AutoDestroy();
            }
        }
    }

    public void Fire(float speed, Vector3 direction)
    {
        rb.velocity = direction * speed * Time.deltaTime * 4f * speedMultiplier;
    }

    private void AutoDestroy()
    {
        Destroy(gameObject);
    }

    public void HitController()
    {
        hasHit = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            HitController();
        }
    }

    // --- --- ---

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public void SetInfo(StakeInformation info)
    {
        if (info != null)
        {
            Info = info;
            SetStats();
        }
    }

    private void SetStats()
    {
        maxNumHit = Info.StatsByLevel[level].MaxNumHit;
        damage = Info.StatsByLevel[level].Damage;
        speedMultiplier = Info.StatsByLevel[level].Speed;
    }
}
