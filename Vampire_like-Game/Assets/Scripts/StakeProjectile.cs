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
    [SerializeField] private int MaxNumHit;
    private int currentNumHit;

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
            AutoDestroy();
        }
    }

    public void Fire(float speed, Vector3 direction)
    {
        rb.velocity = direction * speed * Time.deltaTime * 2;
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
            Debug.Log("Enemy was damaged.");
            HitController();
        }
    }
}
