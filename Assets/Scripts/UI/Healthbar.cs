using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.minValue = 0;
        slider.value = maxHealth;
    }

    public void SetCurrentHealth(int currentHealth)
    {
        slider.value = currentHealth;
    }
}
