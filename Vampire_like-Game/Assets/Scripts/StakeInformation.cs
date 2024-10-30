using System;
using UnityEngine;

[Serializable]
public class StakeStats
{
    public int MaxNumHit;
    public float Damage;
    public float Speed;
}

[CreateAssetMenu(menuName = "Weapon/New Stake", fileName = "Stake_")]
public class StakeInformation : ScriptableObject
{
    public StakeStats[] StatsByLevel;
}
