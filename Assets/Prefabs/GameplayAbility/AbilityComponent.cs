using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class AbilityComponent : MonoBehaviour
{
    [SerializeField] Ability[] initialAbilities;
    [SerializeField] float stamina = 100;
    [SerializeField] float maxStamina = 100;

    List<Ability> abilities = new List<Ability>();

    public event Action<Ability> onNewAbilityAdded;
    public event Action<float, float> onStaminaChanged;

    private void Start()
    {
        foreach(Ability ability in initialAbilities)
        {
            GiveAbility(ability);
        }
    }

    private void GiveAbility(Ability ability)
    {
        Ability newAbility = Instantiate(ability);
        newAbility.Init(this);
        abilities.Add(newAbility);
        onNewAbilityAdded?.Invoke(newAbility);
    }

    internal bool TryConsumeStamina(float staminaCost)
    {
        if(stamina >= staminaCost)
        {
            stamina -= staminaCost;
            onStaminaChanged?.Invoke(stamina, maxStamina);
            return true;
        }

        return false;
    }

    internal float GetMaxStamina()
    {
        return maxStamina;
    }

    internal float GetStamina()
    {
        return stamina;
    }

    internal void TryActivateAbility(Ability abilityToCast)
    {
        foreach(Ability ability in abilities)
        {
            if(ability.GetType() == abilityToCast.GetType())
            {
                ability.ActivateAbility();
            }
        }
    }
}
