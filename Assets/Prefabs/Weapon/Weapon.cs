using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] string slotName = "DefaultWeaponSlot";

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
    }

    public void UnEquip()
    {
        gameObject.SetActive(false);

    }
}
