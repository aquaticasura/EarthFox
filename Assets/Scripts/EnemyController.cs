using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private float currentHealth;
    public Enemy enemyData;
    private EnemyGunScript enemyGunScript;
    private bool isAgressive;
    [SerializeField] private float aggroRange = 5f;
    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        currentHealth = enemyData.maxHealth;
        enemyGunScript = GetComponentInChildren<EnemyGunScript>();
        isAgressive = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerMovement == null)
        {
            return; //put logics n stuff that requiere the player beyonder ts point
        }

        float distanceToPlayer = Vector2.Distance(transform.position, playerMovement.transform.position);
        isAgressive = distanceToPlayer <= aggroRange;

        enemyGunScript.isAgressive = isAgressive;

        if (!isAgressive)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        //float dir = Mathf.Sign(playerMovement.transform.position.x - transform.position.x);
        //rb.linearVelocity = new Vector2(dir * enemyData.moveSpeed, rb.linearVelocity.y);
    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    public void GetRecoiled(Vector2 direction)
    {
        rb.AddForce(direction, ForceMode2D.Impulse);
    }

}
