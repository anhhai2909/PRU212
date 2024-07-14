using CoreSystem;
using ProjectileSystem.Components;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        loadWeaponInventory();
        loadDamage();
       // updatePlayerInformationBasedOnLevel();
        loadDamage();
        playerData.movementVelocity = 10.5f;
    }

    private void Update()
    {
       updatePlayerInformationBasedOnLevel();
      // Debug.Log(stats.Health.CurrentValue + " " + stats.Health.MaxValue);
      //  Debug.Log(stats.Mana.CurrentValue + " " + stats.Health.MaxValue);
    }

    public void updateReducePercentDamage(float percent)
    {
        LoadDataScript.reduceDamage = percent;
    }

    public void setDefaultWeaponInventory()
    {
        WeaponDataSO[] weapons = new WeaponDataSO[1];
        weapons[0] = WeaponDataLoader.GetWeaponDataByName("Arm Attack");
        weaponInventory.SetWeaponInventory(weapons);
    }

    public void updateWeapon(int index, float damage)
    {
        if (weaponInventory.TryGetWeapon(index, out var data))
        {
           // Debug.Log("Update weapon \"" + data.Name + "\": +" + damage + " damage");
            data.AddAddDamage(damage);
           // Debug.Log("Update weapon \"" + data.Name + "\": +" + damage + " damage");
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

    public void loadDamage()
    {
        if (GetAllWeapons() != null)
        {
            for (int i = 0; i < GetAllWeapons().Length; i++)
            {
                if (GetAllWeapons()[i] != null)
                {
                    if (GetAllWeapons()[i].Name == "Bardent's Sword")
                    {
                        updateWeapon(i, 10 * LoadDataScript.playerSdLevel);
                    }
                    else if (GetAllWeapons()[i].Name == "Wooden Bow")
                    {
                        updateWeapon(i, 10 * LoadDataScript.playerBdLevel);
                    }
                    else if (GetAllWeapons()[i].Name == "Magic")
                    {
                        updateWeapon(i, 10 * LoadDataScript.playerMdLevel);
                    }
                }
            }
        }
    }

    public void updateSwordDamage()
    {
        if (GetAllWeapons() != null)
        {
            for (int i = 0; i < GetAllWeapons().Length; i++)
            {
                if (GetAllWeapons()[i] != null)
                {
                    if (GetAllWeapons()[i].Name == "Bardent's Sword")
                    {
                        updateWeapon(i, 10 );
                    }

                }
            }
        }
    }

    public void updateBowDamage()
    {
        if (GetAllWeapons() != null)
        {
            for (int i = 0; i < GetAllWeapons().Length; i++)
            {
                if (GetAllWeapons()[i] != null)
                {
                    if (GetAllWeapons()[i].Name == "Wooden Bow")
                    {
                        updateWeapon(i, 10);
                    }
                }
            }
        }
    }

    public void updateMagicDamage()
    {
        if (GetAllWeapons() != null)
        {
            for (int i = 0; i < GetAllWeapons().Length; i++)
            {
                if (GetAllWeapons()[i] != null)
                {
                    if (GetAllWeapons()[i].Name == "Magic")
                    {
                        updateWeapon(i, 10);
                    }
                }
            }
        }
    }

    public void loadWeaponInventory()
    {
        if (LoadDataScript.LoadPlayerWeaponInventory() == null)
        {
            WeaponDataSO[] weapons = new WeaponDataSO[1];
            weapons[0] = WeaponDataLoader.GetWeaponDataByName("Arm Attack");
            weaponInventory.SetWeaponInventory(weapons);
        }
        else
        {
            string[] weaponsName = LoadDataScript.LoadPlayerWeaponInventory();
            WeaponDataSO[] weapons = new WeaponDataSO[weaponsName.Length];
            for (int i = 0; i < weaponsName.Length; i++)
            {
                weapons[i] = WeaponDataLoader.GetWeaponDataByName(weaponsName[i]);
            }
            weaponInventory.SetWeaponInventory(weapons);
        }
        if (GetAllWeapons() != null)
        {
            for (int i = 0; i < GetAllWeapons().Length; i++)
            {
                if (GetAllWeapons()[i] != null)
                {
                    if (GetAllWeapons()[i].GetAddDamage() != 0)
                        updateWeapon(i, -GetAllWeapons()[i].GetAddDamage());
                    //Debug.Log(GetAllWeapons()[i].Name + " " + GetAllWeapons()[i].GetAddDamage());

                }
            }
        }
    }

    public bool checkForSword()
    {
        if (GetAllWeapons() != null)
        {
            for (int i = 0; i < GetAllWeapons().Length; i++)
            {
                if (GetAllWeapons()[i] != null)
                {
                    if (GetAllWeapons()[i].Name == "Bardent's Sword")
                    {
                        return true;
                    }

                }
            }
        }
        return false;
    }

    public bool checkForBow()
    {
        if (GetAllWeapons() != null)
        {
            for (int i = 0; i < GetAllWeapons().Length; i++)
            {
                if (GetAllWeapons()[i] != null)
                {
                    if (GetAllWeapons()[i].Name == "Wooden Bow")
                    {
                        return true;
                    }

                }
            }
        }
        return false;
    }

    public bool checkForMagic()
    {
        if (GetAllWeapons() != null)
        {
            for (int i = 0; i < GetAllWeapons().Length; i++)
            {
                if (GetAllWeapons()[i] != null)
                {
                    if (GetAllWeapons()[i].Name == "Magic")
                    {
                        return true;
                    }

                }
            }
        }
        return false;
    }

    public void setDefaultWeaponStat()
    {
        if (GetAllWeapons() != null)
        {
            for (int i = 0; i < GetAllWeapons().Length; i++)
            {
                if (GetAllWeapons()[i] != null)
                {
                    if (GetAllWeapons()[i].GetAddDamage() != 0)
                        updateWeapon(i, -GetAllWeapons()[i].GetAddDamage());

                }
            }
        }
    }

    public void updatePlayerInformationBasedOnLevel()
    {
        LoadDataScript.LoadPlayerData();
        updateMaxHealth(100 * LoadDataScript.playerHealthLevel);
        updateMaxMana(100 * LoadDataScript.playerManaLevel);
      //  Debug.Log("Health: " + stats.Health.CurrentValue + " " + stats.Health.MaxValue);
      //  Debug.Log("Mana: " + stats.Mana.CurrentValue + " " + stats.Mana.MaxValue);
        if (GetAllWeapons() != null)
        {
            for (int i = 0; i < GetAllWeapons().Length; i++)
            {
                if (GetAllWeapons()[i] != null)
                {
                    
                   // Debug.Log(GetAllWeapons()[i].Name + " " + GetAllWeapons()[i].GetAddDamage());
                    
                }
            }
        }


    }

}
