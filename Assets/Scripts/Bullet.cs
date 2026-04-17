using UnityEngine;
using System.Collections;
public class Bullet : MonoBehaviour
{
    private float damage = 10f;
    private Collider2D bulletCollider;
    public enum ShooterType
{
    Player,
    Enemy
}

private ShooterType shooter;
    public void SetShooter(ShooterType type)
{
    shooter = type;
}

    void Awake()
    {
        bulletCollider = GetComponent<Collider2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D collision)
{
    EnemyMovement enemy = collision.collider.GetComponentInParent<EnemyMovement>();
    PlayerMovement player = collision.collider.GetComponentInParent<PlayerMovement>();

    if (enemy == null)
        enemy = collision.collider.GetComponent<EnemyMovement>();

    if (enemy == null)
        enemy = collision.collider.GetComponentInChildren<EnemyMovement>();

    if (player == null)
        player = collision.collider.GetComponent<PlayerMovement>();

    if (player == null)
        player = collision.collider.GetComponentInChildren<PlayerMovement>();


    if (shooter == ShooterType.Player && enemy != null)
    {
        enemy.TakeDamage(damage);
        Destroy(gameObject);
        return;
    }

    if (shooter == ShooterType.Enemy && player != null)
    {
        player.TakeDamage(damage);
        Destroy(gameObject);
        return;
    }
    Destroy(gameObject);
}

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    public void IgnoreShooterCollider(Collider2D shooterCollider)
    {
     

        Physics2D.IgnoreCollision(bulletCollider, shooterCollider, true);
        StartCoroutine(Collisionize(shooterCollider));
    }

    private IEnumerator Collisionize(Collider2D shooterCollider)
    {
        yield return new WaitForSeconds(0.2f);
        
        Physics2D.IgnoreCollision(bulletCollider, shooterCollider, false);
        
    }
}
