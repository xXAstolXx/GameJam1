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
    private Image secondSpell;
    [SerializeField]
    private Image combinedSpell;

    private void Awake()
    {
        instance = GetComponent<SpellUI>();
        elementToImage = new Dictionary<ElementTypes, Sprite>();
        elementToImage.Add(ElementTypes.ICE, iceSprite);
        elementToImage.Add(ElementTypes.FIRE, fireSprite);
        elementToImage.Add(ElementTypes.HOTWATER, hotWaterSprite);
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
            var color = firstSpell.color.a;
            color = 1f;
        }
        else
        {
            var color = firstSpell.color.a;
            color = 0.5f;
        }
        firstSpell.sprite = sprite;

        elementToImage.TryGetValue(inputs[1].element, out sprite);
        if (inputs[1].isActive)
        {
            var color = secondSpell.color.a;
            color = 1f;
        }
        else
        {
            var color = secondSpell.color.a;
            color = 0.5f;
        }
        secondSpell.sprite = sprite;

        elementToImage.TryGetValue(inputs[2].element, out sprite);
        if (inputs[2].isActive)
        {
            var color = combinedSpell.color.a;
            color = 1f;
        }
        else
        {
            var color = combinedSpell.color.a;
            color = 0.5f;
        }
        combinedSpell.sprite = sprite;
    }

}
