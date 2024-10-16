using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float xp;
    
    private void GetXP(GameObject obj)
    {
        xp += 10;
    }

    private void OnEnable()
    {
        XP.Collected += GetXP;
    }

    private void OnDisable()
    {
        XP.Collected -= GetXP;
    }
}
