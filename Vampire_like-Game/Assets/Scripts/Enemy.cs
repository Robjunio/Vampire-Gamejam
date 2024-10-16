using DG.Tweening;
using System.Collections.Generic;
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
    private CircleCollider2D circleCollider;

    private ObjectPool smallHit;
    private List<GameObject> smallHits;
    private ObjectPool largeHit;
    private List<GameObject> largeHits;

    private ObjectPool drops;
    private GameObject drop;

    public float damage;
    float life;
    float speed;

    bool dead;

    Tween alpha;
    
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
        TryGetComponent(out circleCollider);

        smallHits = new List<GameObject>();
        largeHits = new List<GameObject>();
    }

    private void Update()
    {
        if (dead) return;

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

    public void SetInfo(EnemyInformation info, ObjectPool big, ObjectPool small, ObjectPool drops)
    {
        dead = false;

        largeHit = big;
        smallHit = small;
        this.drops = drops;

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

        // Tween the alpha of a color called myColor to 0 in 1 second
        alpha = DOTween.ToAlpha(() => BehindMaskRenderer.color, x => BehindMaskRenderer.color = x, 0, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dead) return;
        
        if (collision.CompareTag("Weapon"))
        {
            GetHit(collision);
        }
    }

    private void GetHit(Collider2D collision)
    {
        life -= 10;
        if (life > 0)
        {
            var obj = smallHit.GetFreeObject();
            obj.transform.position = collision.gameObject.transform.position;
            obj.SetActive(true);
            smallHits.Add(obj);
        }
        else
        {
            Dead();
            var obj = largeHit.GetFreeObject();
            obj.transform.position = collision.gameObject.transform.position;
            obj.SetActive(true);
            largeHits.Add(obj);
        }
    }

    private void Dead()
    {
        drop = drops.GetFreeObject();
        drop.transform.position = transform.position;
        drop.SetActive(true);

        dead = true;
        BehindMaskRenderer.enabled = false;
        MaskRenderer.enabled = false;
        circleCollider.enabled = false;

        alpha.Complete();

        Invoke("Deactivate", 0.7f);
    }

    private void Deactivate()
    {
        this.gameObject.SetActive(false);
        OnDie();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (dead) return;
        if (collision.CompareTag("Mask"))
        {
            BehindMaskRenderer.enabled = false;
            MaskRenderer.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (dead) return;

        alpha.Complete();

        if (collision.CompareTag("Mask"))
        {
            BehindMaskRenderer.enabled = true;
            MaskRenderer.enabled = false;
        }

        // Tween the alpha of a color called myColor to 0 in 1 second
        alpha = DOTween.ToAlpha(() => BehindMaskRenderer.color, x => BehindMaskRenderer.color = x, 1, 0.5f).OnComplete(() => DOTween.ToAlpha(() => BehindMaskRenderer.color, x => BehindMaskRenderer.color = x, 0, 1));
    }

    private void OnDisable()
    {
        if(smallHits != null) {
            foreach (var obj in smallHits)
            {
                smallHit.ReturnObject(obj);
            }
            smallHits.Clear();
        }

        if (largeHits != null)
        {
            foreach (var obj in largeHits)
            {
                largeHit.ReturnObject(obj);
            }

            largeHits.Clear();
        }

        specialEffect.SetActive(false);
        BehindMaskRenderer.enabled = true;
        MaskRenderer.enabled = false;
        if(circleCollider != null)
        {
            circleCollider.enabled = true;
        }
    }
}
