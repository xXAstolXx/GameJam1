using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellUI : MonoBehaviour
{
    #region Singleton
    private static SpellUI instance = null;
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
        elementToImage = new Dictionary<ElementTypes, Sprite>();
        elementToImage.Add(ElementTypes.ICE, iceSprite);
        elementToImage.Add(ElementTypes.FIRE, fireSprite);
        elementToImage.Add(ElementTypes.HOTWATER, hotWaterSprite);
        elementToImage.Add(ElementTypes.NONE, noSprite);
    }

    private SpellUI()
    {

    }

    public static SpellUI Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new SpellUI();
            }
            return instance;
        }
    }
    #endregion

    public void UpdateState(Dictionary<ElementTypes, bool> inputs)
    {
        Sprite sprite;
        foreach (var input in inputs)
        {
            elementToImage.TryGetValue(input.Key, out sprite);
            if (input.Value)
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
        }
    }

}
