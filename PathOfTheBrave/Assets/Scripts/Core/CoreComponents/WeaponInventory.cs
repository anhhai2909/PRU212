using System;
using UnityEngine;
using Weapons;

namespace CoreSystem
{
    public class WeaponInventory : CoreComponent
    {
        public event Action<int, WeaponDataSO> OnWeaponDataChanged;

        [field: SerializeField] public WeaponDataSO[] weaponData { get; private set; }

        public bool TrySetWeapon(WeaponDataSO newData, int index, out WeaponDataSO oldData)
        {
            if (index >= weaponData.Length)
            {
                oldData = null;
                return false;
            }

            oldData = weaponData[index];
            weaponData[index] = newData;
            if (weaponData[index].GetAddDamage() != 0)
            {
                weaponData[index].AddAddDamage(-weaponData[index].GetAddDamage());
            }
            OnWeaponDataChanged?.Invoke(index, newData);

        

            LoadDataScript.SavePlayerWeaponInventory(weaponData);
            return true;
        }

        public void SetWeaponInventory(WeaponDataSO[] _weaponData)
        {
            weaponData = _weaponData;
        }

        public bool TryGetWeapon(int index, out WeaponDataSO data)
        {
            if (index >= weaponData.Length)
            {
                data = null;
                return false;
            }
            if (weaponData[index] is null)
            {
                data = null;
                return false;
            }

            data = weaponData[index];
            return true;
        }

        public bool TryGetEmptyIndex(out int index)
        {
            for (var i = 0; i < weaponData.Length; i++)
            {
                if (weaponData[i] is not null)
                    continue;
                Debug.Log("Hhi");
                index = i;
                return true;
            }
            Debug.Log("Hha");
            index = -1;
            return false;
        }

        public void AddEmptyPosition()
        {
            Debug.Log("4");
            // Create a new array with one additional slot
            WeaponDataSO[] newWeaponData = new WeaponDataSO[weaponData.Length + 1];

            // Copy existing elements to the new array
            for (int i = 0; i < weaponData.Length; i++)
            {
                newWeaponData[i] = weaponData[i];
            }

            // Set the new array as the weaponData array
            weaponData = newWeaponData;
        }

        public WeaponSwapChoice[] GetWeaponSwapChoices()
        {
            Debug.Log("5");
            var choices = new WeaponSwapChoice[weaponData.Length];

            for (var i = 0; i < weaponData.Length; i++)
            {
                var data = weaponData[i];

                choices[i] = new WeaponSwapChoice(data, i);
            }

            return choices;
        }
    }
}