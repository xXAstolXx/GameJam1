using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementTile : MonoBehaviour
{
    [HideInInspector]
    public ElementTypes element;
    protected BoxCollider2D collider;
    protected SpriteRenderer spriteRenderer;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


}
