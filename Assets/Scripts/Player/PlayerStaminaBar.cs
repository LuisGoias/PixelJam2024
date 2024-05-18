using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaBar : MonoBehaviour
{
    [SerializeField] private Slider slider;


    void Update()
    {
        
    }

    public void UpdateStaminaBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }

    public void RestartStaminaBar()
    {
        slider.value = 50f;
    }
}
