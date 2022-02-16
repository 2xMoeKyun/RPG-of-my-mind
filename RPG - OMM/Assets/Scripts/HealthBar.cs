using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Health playerHealth; //скрипт health, где берутся переменные и функции

    private void Update()
    {
        SetMaxHelth(playerHealth.maxHealth);
        SetHelth(playerHealth.health);
    }

    void SetMaxHelth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    void SetHelth(int health)
    {
        slider.value = health;
    }
}
