using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    #region Members
    [Header("Movement" , order = 1)]
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float lineThickness = 0.1f;
    [SerializeField]
    Color iceColor;
    [SerializeField]
    Color fireColor;
    [SerializeField]
    Color hotWaterColor;
    #endregion

    [SerializeField]
    GameObject ice;
    [SerializeField]
    GameObject fire;
    [SerializeField]
    GameObject hotWater;

    [SerializeField]
    List<GameObject> graphicObjects;

    int activeGraphic = 1;


    [SerializeField]
    Tilemap elementTileMap;
    List<GameObject> elementTiles;

    

    private Rigidbody2D rb;
    private LineRenderer lineRenderer;

    int collisionCount = 0;
    Vector2 collisionNormal;

    public int activeSpell;
    public ElementTypes[] spells;

    bool isAttacking = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spells = new ElementTypes[2];
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        spells[0] = ElementTypes.NONE;
        spells[1] = ElementTypes.NONE;
        for (int i = 0; i < 4; i++) 
        {
            if (activeGraphic == i)
            {
                graphicObjects[i].SetActive(true);
            }
            else
            {
                graphicObjects[i].SetActive(false);
            }
        }     
    }


    private void Start()
    {

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
        //if (spells[0] != ElementTypes.NONE && spells[1] != ElementTypes.NONE)
        //{
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            activeSpell = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            activeSpell = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            activeSpell = 2;
        }
        //}
        //else if(spells[1] == ElementTypes.NONE)
        //{
        //    activeSpell = 0;
        //}

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            TryAttack();
        }

        UpdateSpellUI();
        AttackUpdate();
        UpdateGraphic();
    }

    private void UpdateGraphic()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;

        if (movement.magnitude == 0f)
        {
            return;
        }
        float angle = Mathf.Rad2Deg * Mathf.Atan2(movement.x, movement.y);
        if (angle > 45 && angle < 135)
        {
            activeGraphic = 0;
        }
        else if (angle >= 135 && angle < 225)
        {
            activeGraphic = 1;
        }
        else if (angle >= 225 && angle < 315)
        {
            activeGraphic = 2;
        }
        else
        {
            activeGraphic = 3;
        }
        //Debug.Log(angle);
        for (int i = 0; i < 4; i++)
        {
            if (activeGraphic == i)
            {
                graphicObjects[i].SetActive(true);
            }
            else
            {
                graphicObjects[i].SetActive(false);
            }
        }
        if (activeSpell == 0)
        {
            graphicObjects[activeGraphic].GetComponent<Animator>().SetBool("Fire", spells[0] == ElementTypes.FIRE);
            graphicObjects[activeGraphic].GetComponent<Animator>().SetBool("Ice", spells[0] == ElementTypes.ICE);
        }
        else if (activeSpell == 1)
        {
            graphicObjects[activeGraphic].GetComponent<Animator>().SetBool("Fire", spells[1] == ElementTypes.FIRE);
            graphicObjects[activeGraphic].GetComponent<Animator>().SetBool("Ice", spells[1] == ElementTypes.ICE);
        }
        else
        {
            graphicObjects[activeGraphic].GetComponent<Animator>().SetBool("Fire", false);
            graphicObjects[activeGraphic].GetComponent<Animator>().SetBool("Ice", false);
        }
    }
        


    private void TryAttack()
    {
        //if (activeSpell == 0)
        //{
        //    if (spells[0] != ElementTypes.NONE)
        //    {
        //        Attack(spells[0]);
        //    }
        //    spells[0] = spells[1];
        //    spells[1] = ElementTypes.NONE;
        //}
        //else if (activeSpell == 1)
        //{
        //    if (spells[1] != ElementTypes.NONE)
        //    {
        //        Attack(spells[1]);
        //        spells[1] = ElementTypes.NONE;
        //    }

        //}
        //else if (activeSpell == 2)
        //{
        //    if (spells[0] != ElementTypes.NONE && spells[1] != ElementTypes.NONE)
        //    {
        //        Attack(ElementTypes.WATER);
        //        spells[0] = ElementTypes.NONE;
        //        spells[1] = ElementTypes.NONE;
        //    }
        //}

        if (activeSpell == 0)
        {
            Attack(ElementTypes.FIRE);
        }
        else if (activeSpell == 1)
        {
            Attack(ElementTypes.ICE);

        }
        else if (activeSpell == 2)
        {
            Attack(ElementTypes.WATER);
        }
    }

    private void Attack(ElementTypes element)
    {
        ElementTypes attackType = ElementTypes.ICE;
        Color c = hotWaterColor;
        if (activeSpell == 0)
        {
            c = fireColor;
            attackType = ElementTypes.FIRE;  
        }
        else if ( activeSpell == 1)
        {
            c = iceColor;
            attackType = ElementTypes.ICE;

        }
        else if (activeSpell == 2)
        {
            c = hotWaterColor;
            attackType = ElementTypes.WATER;
        }
        lineRenderer.material.SetColor("_Color", c);
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.SetPosition(1, targetPos);
        lineRenderer.widthMultiplier = lineThickness;
        lineRenderer.enabled = true;
        isAttacking = true;
        StartCoroutine(WaitAttack(attackType, targetPos));
    }

    private void AttackUpdate()
    {
        if (isAttacking)
        {
            lineRenderer.SetPosition(0, transform.position);
        }
    }

    IEnumerator WaitAttack(ElementTypes element, Vector3 pos)
    {
        yield return new WaitForSeconds(0.3f);
        lineRenderer.enabled = false;
        isAttacking = false;
        
        Vector3Int position = new Vector3Int(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y), 0);
        CustomGrid.Instance.AttackWith(position, element);
        
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            collisionCount++;
            collisionNormal = collision.contacts[collisionCount-1].normal;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Magic Sphere"))
        {
            OnMagicSphereEnter(collision);
        }
    }

    public void OnMagicSphereEnter(Collision2D collision)
    {
        OnMagicSphereCollide(collision.gameObject.GetComponent<MagicSphere>());
        Destroy(collision.gameObject);
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
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
    
    private void UpdateSpellUI()
    {
        //List<SpellUIId> result = new List<SpellUIId>();
        //result.Add(new SpellUIId(spells[0], activeSpell == 0));
        //result.Add(new SpellUIId(spells[1], activeSpell == 1));
        //result.Add(new SpellUIId(spells[0] != ElementTypes.NONE && spells[1] != ElementTypes.NONE && spells[0] != spells[1] ? 
        //           ElementTypes.WATER : ElementTypes.NONE, activeSpell == 2));

        List<SpellUIId> result = new List<SpellUIId>();
        result.Add(new SpellUIId(ElementTypes.FIRE, activeSpell == 0));
        result.Add(new SpellUIId(ElementTypes.ICE, activeSpell == 1));
        result.Add(new SpellUIId(ElementTypes.WATER, activeSpell == 2));

        SpellUI.Instance.UpdateState(result);
    }

    private void AddElementTile(Vector2 position, ElementTypes element)
    {

    }
}
