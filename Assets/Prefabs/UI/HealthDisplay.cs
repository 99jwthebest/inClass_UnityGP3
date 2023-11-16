using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] HealthComponent healthComponent;
    PlayerValueGauge valueGauge;

    private void Start()
    {
        GameObject playerGameObject = GameplayStatics.GetPlayerGameObject();
        healthComponent = playerGameObject.GetComponent<HealthComponent>();

        valueGauge = GetComponent<PlayerValueGauge>();
        valueGauge.SetValue(healthComponent.GetHealth(), healthComponent.GetMaxHealth());
        healthComponent.onHealthChanged += UpdateValue;
    }

    private void UpdateValue(float currentHealth, float delta, float maxHealth)
    {
        valueGauge.SetValue(currentHealth, maxHealth);
    }
}
