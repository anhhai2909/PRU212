using CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using Weapons.Components;

public class TestUpdateWeapon : WeaponComponent<DamageOnHitBoxActionData, AttackDamage>
{
    private WeaponInventory weaponInventory;

    protected override void Awake()
    {
        base.Awake();
        weaponInventory = GetComponent<WeaponInventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            WeaponDataSO weapon = weaponInventory.weaponData[0];
            Debug.Log("Update");
        }
    }

}
