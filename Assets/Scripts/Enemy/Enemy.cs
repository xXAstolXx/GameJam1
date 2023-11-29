using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private int MaxHealth = 10;
    private int currentHp;

    private Transform playerTransform;

    private Rigidbody2D rb;

    private SightRange sightRange;

    private Healthbar healthbar;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        sightRange = GetComponentInChildren<SightRange>();
        healthbar = GetComponentInChildren<Healthbar>();
        healthbar.SetMaxHealth(MaxHealth);

    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (playerTransform != null && sightRange.playerIsInRange) 
        {
            Vector2 direction = playerTransform.position - transform.position;

            direction.Normalize();

            rb.velocity = direction * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void TakeDamage(int damage)
    {
        healthbar.SetCurrentHealth(currentHp - damage);
    }
    
}
