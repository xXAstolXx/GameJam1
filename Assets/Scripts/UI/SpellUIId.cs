using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellUIId
{
    public ElementTypes element;
    public bool isActive;

    public SpellUIId(ElementTypes element, bool isActive)
    {
        this.element = element;
        this.isActive = isActive;
    }
}
