using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Transform playerTransform;

    private Rigidbody2D rb;

    private SightRange sightRange; 

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        sightRange = GetComponentInChildren<SightRange>();
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
    
}
