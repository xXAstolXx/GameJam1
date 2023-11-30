using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    private float startSpeed;

    [SerializeField]
    private float maxHp = 10;
    private float currentHP;

    [SerializeField]
    private float damage;

    private Transform playerTransform;

    private Rigidbody2D rb;

    private SightRange sightRange;

    private Healthbar healthbar;

    private void Start()
    {
        startSpeed = movementSpeed;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        sightRange = GetComponentInChildren<SightRange>();
        healthbar = GetComponentInChildren<Healthbar>();
        healthbar.SetMaxHealth(maxHp);

    }

    private void FixedUpdate()
    {
        Movement();
        UpdatePos();
    }

    private void Movement()
    {
        if (playerTransform != null && sightRange.playerIsInRange) 
        {
            Vector2 direction = playerTransform.position - transform.position;
            if (direction.magnitude < 1f)
            {
                playerTransform.gameObject.GetComponent<Player>().TakeDamage(damage);
            }

            direction.Normalize();

            rb.velocity = direction * movementSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHP = currentHP - damage * Time.fixedDeltaTime;
        if (currentHP < 0)
        {
            Destroy(gameObject);
        }
        healthbar.SetCurrentHealth(currentHP);
    }

    private void UpdatePos()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y - 1f, 0);
        Vector3Int position = new Vector3Int(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y), 0);
        ElementTypes element = CustomGrid.Instance.GetElement(position);
        Debug.Log(element);
        Element settings;

        if (element == ElementTypes.NONE)
        {
            movementSpeed = startSpeed;
        }
        else
        {
            settings = ElementSettings.Instance.GetElementSettings(element);
            TakeDamage(settings.damage);

            movementSpeed = settings.speedModifier * startSpeed;
        }
    }
}
