using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GarlicProjectile : MonoBehaviour
{
    [SerializeField] private float garlicAutoDestroyTimer;
    [SerializeField] private float currentGarlicTime;

    // --- --- ---
    private GarlicInformation Info;
    private int level;

    public float damage;
    public float radius;
    // --- --- ---

    private void Start()
    {
        currentGarlicTime = garlicAutoDestroyTimer;
    }

    void Update()
    {
        if (currentGarlicTime > 0)
        {
            currentGarlicTime -= Time.deltaTime;
        }
        else
        {
            AutoDestroy();
        }
    }

    private void AutoDestroy()
    {
        Destroy(gameObject);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            //Debug.Log("Enemy was damaged.");
        }
    }

    // --- --- ---

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public void SetInfo(GarlicInformation info)
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
        gameObject.GetComponent<CircleCollider2D>().radius = radius;
        gameObject.GetComponentInChildren<Light2D>().transform.localScale = new Vector3(radius * 2, radius * 2, 1);
    }
}
