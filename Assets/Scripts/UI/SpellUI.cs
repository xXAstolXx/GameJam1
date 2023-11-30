using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellUI : MonoBehaviour
{
    #region Singleton
    private static SpellUI instance;
    Dictionary<ElementTypes, Sprite> elementToImage;

    [SerializeField]
    Sprite fireSprite;
    [SerializeField]
    Sprite iceSprite;
    [SerializeField]
    Sprite hotWaterSprite;
    [SerializeField]
    Sprite noSprite;

    [SerializeField]
    private Image firstSpell;
    [SerializeField]
    private Image notActiveImage1;
    [SerializeField]
    private GameObject secondSpell;
    [SerializeField]
    private Image notActiveImage2;
    [SerializeField]
    private Image combinedSpell;
    [SerializeField]
    private Image notActiveImage3;

    private Outline firstSpellOutline;
    private Outline secondSpellOutline;
    private Outline combinedSpellOutline;

    private void Awake()
    {
        instance = GetComponent<SpellUI>();
        elementToImage = new Dictionary<ElementTypes, Sprite>();
        elementToImage.Add(ElementTypes.ICE, iceSprite);
        elementToImage.Add(ElementTypes.FIRE, fireSprite);
        elementToImage.Add(ElementTypes.WATER, hotWaterSprite);
        elementToImage.Add(ElementTypes.NONE, noSprite);
        firstSpellOutline = firstSpell.GetComponent<Outline>();
        secondSpellOutline = secondSpell.GetComponent<Outline>();
        combinedSpellOutline = combinedSpell.GetComponent<Outline>();
    }

    public static SpellUI Instance
    {
        get
        {
            if(instance == null)
            {
                //instance = this;
            }
            return instance;
        }
    }
    #endregion

    public void UpdateState(List<SpellUIId> inputs)
    {
        Sprite sprite;
        elementToImage.TryGetValue(inputs[0].element, out sprite);
        if (!inputs[0].isActive && inputs[0].element != ElementTypes.NONE && inputs[0] != null)
        {
            notActiveImage1.enabled = false;

        }
        else
        {
            notActiveImage1.enabled = true;
            firstSpellOutline.enabled = false;
        }
        if (inputs[0].isActive)
        {
            firstSpellOutline.enabled = true;
          
            if (inputs[0].element == ElementTypes.NONE)
            {
                notActiveImage1.enabled = true;
            }
            else
            {
                notActiveImage1.enabled = false;
            }

        }
        firstSpell.sprite = sprite;

        elementToImage.TryGetValue(inputs[1].element, out sprite);
        
        if (!inputs[1].isActive && inputs[1].element != ElementTypes.NONE && inputs[1] != null)
        {
            notActiveImage2.enabled = false;
            
        }
        else
        {
            notActiveImage2.enabled = true;
            secondSpellOutline.enabled = false;
        }
        if (inputs[1].isActive)
        {
            secondSpellOutline.enabled = true;
            if (inputs[1].element == ElementTypes.NONE)
            {
                notActiveImage2.enabled = true;
            }
            else
            {
                notActiveImage2.enabled = false;
            }
        }
        secondSpell.GetComponent<Image>().sprite = sprite;

        elementToImage.TryGetValue(inputs[2].element, out sprite);
        if (!inputs[2].isActive && inputs[2].element != ElementTypes.NONE && inputs[2] != null)
        {
            notActiveImage2.enabled = false;

        }
        else
        {
            notActiveImage2.enabled = true;
            combinedSpellOutline.enabled = false;
        }
        if (inputs[2].isActive)
        {
            combinedSpellOutline.enabled = true;
            if (inputs[2].element == ElementTypes.NONE)
            {
                notActiveImage3.enabled = true;
            }
            else
            {
                notActiveImage3.enabled = false;
            }

        }
        combinedSpell.sprite = sprite;
    }



}
