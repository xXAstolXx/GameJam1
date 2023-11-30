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
    private Image secondSpell;
    [SerializeField]
    private Image notActiveImage2;
    [SerializeField]
    private Image combinedSpell;
    [SerializeField]
    private Image notActiveImage3;

    private void Awake()
    {
        instance = GetComponent<SpellUI>();
        elementToImage = new Dictionary<ElementTypes, Sprite>();
        elementToImage.Add(ElementTypes.ICE, iceSprite);
        elementToImage.Add(ElementTypes.FIRE, fireSprite);
        elementToImage.Add(ElementTypes.WATER, hotWaterSprite);
        elementToImage.Add(ElementTypes.NONE, noSprite);
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
        if (inputs[0].isActive)
        {
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
        if (inputs[1].isActive)
        {
            if (inputs[1].element == ElementTypes.NONE)
            {
                notActiveImage2.enabled = true;
            }
            else
            {
                notActiveImage2.enabled = false;
            }
        }
        secondSpell.sprite = sprite;

        elementToImage.TryGetValue(inputs[2].element, out sprite);
        if (inputs[2].isActive)
        {
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
