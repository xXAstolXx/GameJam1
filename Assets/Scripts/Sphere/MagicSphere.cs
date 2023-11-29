using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSphere : MonoBehaviour
{
    //[SerializeField]
    float initialForce = 1;
    [SerializeField]
    float constantVelocity;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    public ElementTypes element;
    [SerializeField]
    Color colorIce;
    [SerializeField]
    Color colorFire;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Random.insideUnitCircle * initialForce, ForceMode2D.Impulse);

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (element == ElementTypes.ICE)
        {
            spriteRenderer.color = colorIce;
        }
        else if (element == ElementTypes.FIRE)
        {
            spriteRenderer.color = colorFire;
        }
    }

    private void Update()
    {
        if (rb.velocity.magnitude < constantVelocity)
        {
            rb.AddForce(rb.velocity.normalized* Time.fixedDeltaTime * (rb.mass/(Time.fixedDeltaTime*rb.velocity.magnitude)), ForceMode2D.Impulse);
        }
    }
}
