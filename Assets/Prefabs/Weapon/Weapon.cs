using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] string slotName = "DefaultWeaponSlot";
    [SerializeField] AnimatorOverrideController animationOverride;

    public string GetSlotName()
    {
        return slotName;
    }
    public GameObject Owner
    {
        get;
        private set;
    }

    public void Initialize(GameObject owner)
    {
        Owner = owner;
    }

    public void Equip()
    {
        gameObject.SetActive(true);
        // This applies the override to the animator.
        Owner.GetComponent<Animator>().runtimeAnimatorController = animationOverride;
    }

    public void UnEquip()
    {
        gameObject.SetActive(false);

    }

    public abstract void Attack();

    protected void DamageGameObject(GameObject objectToDamage, float damageAmt)
    {
        Debug.Log("weapon attack point");
        objectToDamage.GetComponent<HealthComponent>()?.ChangeHealth(-damageAmt, Owner);
    }
}
