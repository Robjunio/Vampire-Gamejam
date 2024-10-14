using System;
using UnityEngine;

[Serializable]
public class Stats
{
    public float ProbabilityToBeSpecial;
    public float Health;
    public float Damage;
    public float Speed;
}

[CreateAssetMenu(menuName = "Enemies/New Enemy", fileName = "Enemy_")]
public class EnemyInformation : ScriptableObject
{
    public Stats[] StatsByHour;
    public Animator Animator;
}
