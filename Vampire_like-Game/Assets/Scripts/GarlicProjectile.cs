using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicProjectile : MonoBehaviour
{
    [SerializeField] private float garlicAutoDestroyTimer;
    [SerializeField] private float currentGarlicTime;

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
            Debug.Log("Enemy was damaged.");
        }
    }
}
