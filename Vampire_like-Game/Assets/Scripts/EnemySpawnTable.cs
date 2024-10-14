using System;
using UnityEngine;

[Serializable]
public class SpawnInfo
{
    public int enemyPool;
    public float probability_enemy_1;
    public float probability_enemy_2;
    public float probability_enemy_3;
}

[CreateAssetMenu(menuName = "Enemies/New Spawn Table", fileName = "SpawnTable_")]
public class EnemySpawnTable : ScriptableObject
{
    public SpawnInfo[] table;
}
