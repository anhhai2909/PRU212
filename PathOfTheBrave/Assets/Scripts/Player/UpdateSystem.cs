using CoreSystem;
using ProjectileSystem.Components;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Weapons;
using Weapons.Components;

public class UpdateSystem : CoreComponent
{
    private WeaponInventory weaponInventory;
    private Stats stats;
    private PlayerData playerData;

    protected override void Awake()
    {
        base.Awake();
        weaponInventory =  core.GetCoreComponent<WeaponInventory>();
        stats = core.GetCoreComponent<Stats>();
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            playerData = player.playerData;
        }

        else
        {
            Debug.LogError("Player object not found in the scene!");
            return;
        }
    }

    private void Update()
    {
        
    }

    public void updateWeapon(int index, float damage)
    {
        if (weaponInventory.TryGetWeapon(index, out var data))
        {
            Debug.Log("Update weapon \"" + data.Name + "\": +" + damage + " damage");
            data.AddAddDamage(damage);
        }
    }

    public void setWeaponToDefault()
    {
        foreach (var item in weaponInventory.weaponData)
        {
            if(item is null)
            {
                continue;
            }
            Debug.Log("Reset weapon \"" + item.Name);
            item.SetAddDamage(0);
        }
    }

    public void updateHealth(float health)
    {
        stats.Health.Update(health);
    }

    public void updateSpeed(float speed)
    {
        if(!(playerData is null))
        {
            playerData.movementVelocity += speed;
        }
    }

    public void updateMana(float mana)
    {
        stats.Mana.Update(mana);
    }

}
