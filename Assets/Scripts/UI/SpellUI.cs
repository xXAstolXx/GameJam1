using UnityEngine;

public class SpellUI : MonoBehaviour
{
    #region Singleton
    private static SpellUI instance = null;

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


}
