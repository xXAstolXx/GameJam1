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
    private GameObject secondSpell;
    [SerializeField]
    private Image combinedSpell;

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
        if (inputs[0].isActive)
        {
            firstSpellOutline.enabled = true;

        }
        firstSpell.sprite = sprite;

        elementToImage.TryGetValue(inputs[1].element, out sprite);
        
        if (inputs[1].isActive)
        {
            secondSpellOutline.enabled = true;
        }
        secondSpell.GetComponent<Image>().sprite = sprite;

        elementToImage.TryGetValue(inputs[2].element, out sprite);
        if (inputs[2].isActive)
        {
            combinedSpellOutline.enabled = true;

        }
        combinedSpell.sprite = sprite;
    }



}
