using System;
using UnityEngine;

[Serializable]
public class HolyWaterStats
{
    public float Damage;
    public float Radius;
}

[CreateAssetMenu(menuName = "Weapon/New HolyWaterBottle", fileName = "HolyWater_")]
public class HolyWaterInformation : ScriptableObject
{
    public HolyWaterStats[] StatsByLevel;
}
