using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSettings : MonoBehaviour
{
    static ElementSettings instance;
    public static ElementSettings Instance
    {
        get
        {
            return instance;
        }
    }

    public List<Element> elements;

    public Dictionary<ElementTransform, ElementTypes> transformElements = new Dictionary<ElementTransform, ElementTypes>()
    { { new ElementTransform(ElementTypes.FIRE, ElementTypes.ICE), ElementTypes.WATER
},
        { new ElementTransform(ElementTypes.FIRE, ElementTypes.WATER), ElementTypes.STEAM},
        { new ElementTransform(ElementTypes.WATER, ElementTypes.FIRE), ElementTypes.STEAM},
        { new ElementTransform(ElementTypes.ICE, ElementTypes.FIRE), ElementTypes.WATER},
        { new ElementTransform(ElementTypes.FIRE, ElementTypes.STEAM), ElementTypes.FIRE},
        { new ElementTransform(ElementTypes.STEAM, ElementTypes.FIRE), ElementTypes.FIRE},
        { new ElementTransform(ElementTypes.ICE, ElementTypes.WATER), ElementTypes.ICE},
        { new ElementTransform(ElementTypes.WATER, ElementTypes.ICE), ElementTypes.ICE},
        { new ElementTransform(ElementTypes.ICE, ElementTypes.STEAM), ElementTypes.STEAM},
        { new ElementTransform(ElementTypes.STEAM, ElementTypes.ICE), ElementTypes.STEAM},
        { new ElementTransform(ElementTypes.WATER, ElementTypes.STEAM), ElementTypes.STEAM},
        { new ElementTransform(ElementTypes.STEAM, ElementTypes.WATER), ElementTypes.STEAM},
    };

    private void Start()
    {
        instance = GetComponent<ElementSettings>();
    }

    public Element GetElementSettings(ElementTypes element){
        foreach(Element obj in elements)
        {
            if (obj.element == element)
            {
                return obj;
            }
        }
        return null;
    }
}

public struct ElementTransform
{
    ElementTypes attackType;
    ElementTypes targetType;

    public ElementTransform(ElementTypes attackType, ElementTypes targetType)
    {
        this.attackType = attackType;
        this.targetType = targetType;
    }
}
