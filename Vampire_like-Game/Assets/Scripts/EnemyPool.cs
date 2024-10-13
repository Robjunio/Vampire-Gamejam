
using UnityEngine;

public class EnemyPool : ObjectPool
{
    private void ReturnEnemy(GameObject gameObject, EnemyInformation info)
    {
        if (gameObject == null)
        {
            ReturnObject(gameObject);
        }
    }

    private void OnEnable()
    {
        Enemy.Died += ReturnEnemy;
    }

    private void OnDisable()
    {
        
    }
}
