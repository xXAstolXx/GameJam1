using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D;

public class MagicSphere : MonoBehaviour
{
    //[SerializeField]
    float initialForce = 1;
    [SerializeField]
    float constantVelocity;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Light2D light2D;

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
        light2D = GetComponent<Light2D>();
    }

    private void Start()
    {
        if (element == ElementTypes.ICE)
        {
            spriteRenderer.color = colorIce;
            light2D.color = colorIce;
        }
        else if (element == ElementTypes.FIRE)
        {
            spriteRenderer.color = colorFire;
            light2D.color = colorFire;
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
