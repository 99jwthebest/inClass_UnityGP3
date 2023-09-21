using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    [SerializeField] Weapon[] initialWeaponPrefabs;
    [SerializeField] Transform[] weaponSlots;
    [SerializeField] Transform defaultWeaponSlot;

    private List<Weapon> weapons = new List<Weapon>();

    int currentWeaponIndex = -1; // negative value means something does not exist.

    private void Awake()
    {
        InitializeWeapons();
    }

    private void InitializeWeapons()
    {
        foreach(Weapon weaponPrefab in initialWeaponPrefabs)
        {
            Transform weaponSlot = defaultWeaponSlot;
            foreach(Transform slot in weaponSlots)
            {
                if(slot.name == weaponPrefab.GetSlotName())
                {
                    weaponSlot = slot;
                    break;
                }
            }

            Weapon newWeapon = Instantiate<Weapon>(weaponPrefab, weaponSlot);
            weapons.Add(newWeapon);
            newWeapon.UnEquip();
        }

        NextWeapon();
    }

    public void NextWeapon()
    {
        int nextIndex = currentWeaponIndex + 1;
        if(nextIndex >= weapons.Count)
        {
            nextIndex = 0;
        }

        EquipWeapon(nextIndex);
    }

    private void EquipWeapon(int weaponIndex)
    {
        if( weaponIndex < 0 || weaponIndex >= weapons.Count)
        {
            return; // index out of range
        }

        if(currentWeaponIndex != -1)
        {
            weapons[currentWeaponIndex].UnEquip(); // unequip current weapon if holding any
        }

        currentWeaponIndex = weaponIndex;
        weapons[currentWeaponIndex].Equip(); // equip the new weapon.
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
