using CoreSystem;
using ProjectileSystem.Components;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Weapons;
using Weapons.Components;

public class UpdateWeapon : CoreComponent
{
    private WeaponInventory weaponInventory;

    protected override void Awake()
    {
        base.Awake();
        weaponInventory =  core.GetCoreComponent<WeaponInventory>();
    }

    private void Update()
    {
        
    }

    public void updateWeapon(int index, float damage)
    {
        if (weaponInventory.TryGetWeapon(index, out var data))
        {
            data.AddAddDamage(damage);
        }
    }
}
