using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaDisplay : MonoBehaviour
{
    PlayerValueGauge valueGauge;

    void Start()
    {
        GameObject playerGameObject = GameplayStatics.GetPlayerGameObject();
        AbilityComponent abilityComp = playerGameObject.GetComponent<AbilityComponent>();
        abilityComp.onStaminaChanged += StaminaChange;
        valueGauge = GetComponent<PlayerValueGauge>();
        valueGauge.SetValue(abilityComp.GetStamina(), abilityComp.GetMaxStamina());
    }

    private void StaminaChange(float currentValue, float maxValue)
    {
        valueGauge.SetValue(currentValue, maxValue);
    }
}
