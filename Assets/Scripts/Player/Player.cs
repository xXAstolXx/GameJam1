using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Members
    [Header("Movement" , order = 1)]
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float thickness = 0.1f;
    [SerializeField]
    Color iceColor;
    [SerializeField]
    Color fireColor;
    [SerializeField]
    Color hotWaterColor;
    #endregion

    private Rigidbody2D rb;
    private LineRenderer lineRenderer;

    int collisionCount = 0;
    Vector2 collisionNormal;

    int activeSpell;
    ElementTypes[] spells;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spells = new ElementTypes[2];
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = thickness;
        lineRenderer.enabled = false;
    }

    void FixedUpdate()
    {
        if( collisionCount < 1)
        {
            Movement();

        }
        else
        {
            Bounce();
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            activeSpell = 0;
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            activeSpell = 1;
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            activeSpell = 2;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TryAttack();
        }
    }

    private void TryAttack()
    {
        if (activeSpell == 0)
        {
            if (spells[0] != ElementTypes.NONE)
            {
                Attack(spells[0]);
            }
            spells[0] = spells[1];
            spells[1] = ElementTypes.NONE;
            UpdateSpellUI();
        }
        else if (activeSpell == 1)
        {
            if (spells[1] != ElementTypes.NONE)
            {
                Attack(spells[1]);
                spells[1] = ElementTypes.NONE;
                UpdateSpellUI();
            }

        }
        else if (activeSpell == 2)
        {
            if (spells[0] != ElementTypes.NONE && spells[1] != ElementTypes.NONE)
            {
                Attack(ElementTypes.HOTWATER);
            }
        }
    }

    private void Attack(ElementTypes element)
    {
        //lineRenderer.startColor =
        //return YieldInstruction;
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        rb.transform.Translate(movement * movementSpeed * Time.deltaTime);
    }

    void Bounce()
    {
        rb.transform.Translate(collisionNormal * movementSpeed * Time.deltaTime);
    }

    void ColliderBounce()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.layer != LayerMask.NameToLayer("ElementTile"))
        {
            collisionCount++;
            collisionNormal = collision.contacts[collisionCount-1].normal;
        }
        else
        {
            OnMagicSphereCollide(collision.gameObject.GetComponent<MagicSphere>());
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("ElementTile"))
        {
            collisionCount--;
        }
    }

    private void OnMagicSphereCollide(MagicSphere magicSphere)
    {
        if (spells[0] == ElementTypes.NONE)
        {
            spells[0] = magicSphere.element;
        }
        else if (spells[1] == ElementTypes.NONE)
        {
            spells[1] = magicSphere.element;
        }
        else
        {
            spells[0] = magicSphere.element;
        }
    }
    
    private Dictionary<ElementTypes, bool> UpdateSpellUI()
    {
        Dictionary<ElementTypes, bool> result = new Dictionary<ElementTypes, bool>();
        result.Add(spells[0], activeSpell == 0);
        result.Add(spells[1], activeSpell == 1);
        result.Add(spells[0] != ElementTypes.NONE && spells[1] != ElementTypes.NONE && spells[1] != spells[2] ? 
                   ElementTypes.HOTWATER : ElementTypes.NONE, activeSpell == 2);
        return result;
    }

}
