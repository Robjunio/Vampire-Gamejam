using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWaterProjectile : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float bottleAutoDestroyTimer;
    [SerializeField] private float currentBottleTime;

    private GameObject explodeArea;

    // --- --- ---
    private HolyWaterInformation Info;
    private int level;

    public float damage;
    public float radius;
    // --- --- ---

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        explodeArea = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        currentBottleTime = bottleAutoDestroyTimer;
        explodeArea.SetActive(false);
    }

    void Update()
    {
        if (currentBottleTime > 0)
        {
            currentBottleTime -= Time.deltaTime;
        }
        else
        {
            Explode();
        }
    }

    public void AutoDestroy()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Debug.Log("Enemy was damaged.");
            Explode();
        }
    }

    public void Fire(float speed, Vector3 direction)
    {
        rb.velocity = direction * speed * Time.deltaTime;
    }

    private void Explode()
    {
        explodeArea.SetActive(true);
    }

    // --- --- ---

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public void SetInfo(HolyWaterInformation info)
    {
        if (info != null)
        {
            Info = info;
            SetStats();
        }
    }

    private void SetStats()
    {
        damage = Info.StatsByLevel[level].Damage;
        radius = Info.StatsByLevel[level].Radius;
        SetRadius();
    }

    private void SetRadius()
    {
        explodeArea.transform.localScale = new Vector3(radius * 2, radius * 2, 1);
    }
}
