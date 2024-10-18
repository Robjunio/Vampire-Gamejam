using System;
using UnityEngine;

[Serializable]
public class GarlicStats
{
    public float Damage;
    public float Radius;
}

[CreateAssetMenu(menuName = "Weapon/New Garlic", fileName = "Garlic_")]
public class GarlicInformation : ScriptableObject
{
    public GarlicStats[] StatsByLevel;
}

