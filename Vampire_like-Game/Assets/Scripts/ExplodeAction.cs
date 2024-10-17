using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeAction : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Debug.Log("Enemy was damaged.");
        }

        GetComponentInParent<HolyWaterProjectile>().AutoDestroy();
    }
}
