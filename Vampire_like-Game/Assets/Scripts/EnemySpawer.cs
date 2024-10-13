using UnityEngine;

public class EnemyCounter
{
    public int type1Enemy;
    public int type2Enemy;
    public int type3Enemy;
}

public class EnemySpawer : MonoBehaviour
{
    public static EnemySpawer instance;

    [SerializeField] 
    private Transform target;

    [SerializeField] 
    private LayerMask layerEnemyCannotSpawn;

    [SerializeField] 
    private Collider2D collider2d;

    [SerializeField] 
    EnemyPool pool;

    [SerializeField]
    EnemyInformation[] enemiesInfo;

    [SerializeField] 
    EnemySpawnTable enemySpawn;

    private EnemyCounter enemiesCreated = new EnemyCounter();
    private EnemyCounter enemiesMark = new EnemyCounter();

    private int enemyPool;

    private bool canCreate = true;

    private void Awake()
    {
        instance = this;
    }

    private void SpawnEnemies(int enemyQnt, EnemyInformation enemyToBeSpawned, int hour)
    {
        if (canCreate == false) return;

        for(int i = 0; i < enemyQnt; i++) {
            Vector2 spawnPos = GetRandomSpawnPosition();

            if (spawnPos == Vector2.zero)
            {
                break;
            }

            var baseEnemy = pool.GetFreeObject();
            var script = baseEnemy.GetComponent<Enemy>();
            script.SetLevel(hour);
            script.SetTarget(target);
            script.SetInfo(enemyToBeSpawned);

            baseEnemy.transform.position = spawnPos;
            baseEnemy.SetActive(true);
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        Vector2 spawnPosition = Vector2.zero;
        bool isSpawnValid = false;

        int attemptCount = 0;
        int maxAttempt = 200;

        while(!isSpawnValid && attemptCount < maxAttempt ) {
            spawnPosition = GetRandomPointInCollider(collider2d);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, 0.5f);

            bool isInvalidCollision = false;
            foreach(Collider2D collider in colliders)
            {
                if(((1 << collider.gameObject.layer) & layerEnemyCannotSpawn) != 0)
                {
                    isInvalidCollision = true;
                    break;
                }
            }

            if (!isInvalidCollision)
            {
                isSpawnValid = true;
            }

            attemptCount++;
        }

        if (!isSpawnValid)
        {
            Debug.LogWarning("Couldn't find a valid point");
        }

        return spawnPosition;
    }

    private Vector2 GetRandomPointInCollider(Collider2D collider, float offset = 1f)
    {
        Bounds collBounds = collider.bounds;

        Vector2 minBounds = new Vector2(collBounds.min.x + offset, collBounds.min.y + offset);
        Vector2 maxBounds = new Vector2(collBounds.max.x - offset, collBounds.max.y - offset);

        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);

        return new Vector2(randomX, randomY);
    }

    private void SetSpawn(int hour)
    {
        SpawnInfo spawnInfo = enemySpawn.table[hour];
        enemyPool = spawnInfo.enemyPool;

        if (spawnInfo != null)
        {
            enemiesMark.type1Enemy = Mathf.CeilToInt(enemyPool * spawnInfo.probability_enemy_1 / 100);
            enemiesMark.type2Enemy = Mathf.FloorToInt(enemyPool * spawnInfo.probability_enemy_2 / 100);
            enemiesMark.type3Enemy = Mathf.FloorToInt(enemyPool * spawnInfo.probability_enemy_3 / 100);
        }

        if(enemiesCreated.type1Enemy < enemiesMark.type1Enemy)
        {
            SpawnEnemies(enemiesMark.type1Enemy - enemiesCreated.type1Enemy, enemiesInfo[0], hour);
        }
        if(enemiesCreated.type2Enemy < enemiesMark.type2Enemy)
        {
            SpawnEnemies(enemiesMark.type2Enemy - enemiesCreated.type2Enemy, enemiesInfo[1], hour);
        }
        if (enemiesCreated.type3Enemy < enemiesMark.type3Enemy)
        {
            SpawnEnemies(enemiesMark.type3Enemy - enemiesCreated.type3Enemy, enemiesInfo[2], hour);
        }
    }

    private void DisableSpawn(int hour)
    {
        canCreate = false;
    }

    private void OnEnable()
    {
        GameManager.HourPassed += SetSpawn;
        GameManager.LastHour += DisableSpawn;
    }

    private void OnDisable()
    {
        GameManager.HourPassed -= SetSpawn;
        GameManager.LastHour -= DisableSpawn;
    }
}
