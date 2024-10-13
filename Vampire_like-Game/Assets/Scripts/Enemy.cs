using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void Die(GameObject gameObject, EnemyInformation enemyInformation);
    public static event Die Died;

    [SerializeField] private GameObject specialEffect;
    [SerializeField]private SpriteRenderer BehindMaskRenderer;
    [SerializeField] private SpriteRenderer MaskRenderer;

    private EnemyInformation Info;
    private int level;

    private Animator animator;
    private Rigidbody2D rb;
    private Transform target;

    float damage;
    float life;
    float speed;
    
    private void OnDie()
    {
        if (Info != null)
        {
            Died?.Invoke(gameObject, Info);
        }
    }

    private void Start()
    {
        TryGetComponent(out animator);
        TryGetComponent(out rb);
    }

    private void Update()
    {
        Vector2 dir = target.position - transform.position;
        dir = dir.normalized;
        animator.SetFloat("X", dir.x);
        animator.SetFloat("Y", dir.y);

        rb.velocity = dir * speed * 140 * Time.deltaTime;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public void SetInfo(EnemyInformation info)
    {
        if (info != null)
        {
            Info = info;
            SetStats();
        }
    }

    private void SetStats()
    {
        float powerMultiplier = 1;

        if(Random.Range(0,100) >= 100 - Info.StatsByHour[level].ProbabilityToBeSpecial)
        {
            specialEffect.SetActive(true);
            powerMultiplier = 1.5f;
        }

        damage = Info.StatsByHour[level].Damage * powerMultiplier;
        life = Info.StatsByHour[level].Health * powerMultiplier;
        speed = Info.StatsByHour[level].Speed * powerMultiplier;

        if(Info.Animator != null)
        {
            animator = Info.Animator;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mask"))
        {
            BehindMaskRenderer.enabled = false;
            MaskRenderer.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Mask"))
        {
            BehindMaskRenderer.enabled = true;
            MaskRenderer.enabled = false;
        }
    }

    private void OnDisable()
    {
        specialEffect.SetActive(false);
        BehindMaskRenderer.enabled = true;
        MaskRenderer.enabled = false;
    }
}
