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
        updatePlayerInformationBasedOnLevel();
        playerData.movementVelocity = 10.5f;
    }

    private void Update()
    {
       Debug.Log(stats.Health.CurrentValue + " " + stats.Health.MaxValue);
        Debug.Log(stats.Mana.CurrentValue + " " + stats.Health.MaxValue);
    }

    public void updateReducePercentDamage(float percent)
    {
        LoadDataScript.reduceDamage = percent;
    }

    public void updateWeapon(int index, float damage)
    {
        if (weaponInventory.TryGetWeapon(index, out var data))
        {
            Debug.Log("Update weapon \"" + data.Name + "\": +" + damage + " damage");
            data.AddAddDamage(damage);
            Debug.Log("Update weapon \"" + data.Name + "\": +" + damage + " damage");
        }
    }

    public float getWeaponDamage(int index)
    {
        return GetAllWeapons()[index].GetAddDamage();
    }

    public WeaponDataSO[] GetAllWeapons()
    {
        return weaponInventory.weaponData;
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

    public void updateMaxHealth(float health)
    {
        stats.Health.UpdateMax(health);
    }

    public void updateMaxMana(float mana)
    {
        stats.Mana.UpdateMax(mana);
    }

    public void updateHealth(float health)
    {
        stats.Health.Update(health);
    }

    public float getMaxHealth()
    {
        return stats.Health.MaxValue;
    }

    public float getMaxMana()
    {
        return stats.Mana.MaxValue;
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

    public float getCurrentMana()
    {
        return stats.Mana.CurrentValue;
    }

    public float getCurrentSpeed()
    {
        return playerData.movementVelocity;
    }

    public void updatePlayerInformationBasedOnLevel()
    {
        LoadDataScript.LoadPlayerData();
        switch(LoadDataScript.playerHealthLevel)
        {
            case 1:
                {
                    updateMaxHealth(100);
                    break;
                }
            case 2:
                {
                    updateMaxHealth(100);
                    break;
                }
            case 3:
                {
                    updateMaxHealth(100);
                    break;
                }
            case 4:
                {
                    updateMaxHealth(100);
                    break;
                }
        }

        switch (LoadDataScript.playerManaLevel)
        {
            case 1:
                {
                    updateMaxMana(10);
                    break;
                }
            case 2:
                {
                    updateMaxMana(10);
                    break;
                }
            case 3:
                {
                    updateMaxMana(10);
                    break;
                }
            case 4:
                {
                    updateMaxMana(10);
                    break;
                }
        }

        switch (LoadDataScript.playerBdLevel)
        {
            case 1:
                {
                    updateMaxMana(10);
                    break;
                }
            case 2:
                {
                    updateMaxMana(10);
                    break;
                }
            case 3:
                {
                    updateMaxMana(10);
                    break;
                }
            case 4:
                {
                    updateMaxMana(10);
                    break;
                }
        }

        switch (LoadDataScript.playerSdLevel)
        {
            case 1:
                {
                    updateMaxMana(10);
                    break;
                }
            case 2:
                {
                    updateMaxMana(10);
                    break;
                }
            case 3:
                {
                    updateMaxMana(10);
                    break;
                }
            case 4:
                {
                    updateMaxMana(10);
                    break;
                }
        }

        switch (LoadDataScript.playerMdLevel)
        {
            case 1:
                {
                    updateMaxMana(10);
                    break;
                }
            case 2:
                {
                    updateMaxMana(10);
                    break;
                }
            case 3:
                {
                    updateMaxMana(10);
                    break;
                }
            case 4:
                {
                    updateMaxMana(10);
                    break;
                }
        }
    }

}
