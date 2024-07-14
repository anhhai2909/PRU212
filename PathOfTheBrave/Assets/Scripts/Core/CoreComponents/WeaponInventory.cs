using System;
using System.Reflection;
using UnityEngine;
using Weapons;

namespace CoreSystem
{
    public class WeaponInventory : CoreComponent
    {
        public event Action<int, WeaponDataSO> OnWeaponDataChanged;
        public event Action<bool> OnWeaponChanged;
        public int currentIndex { get; private set; }

        [field: SerializeField] public WeaponDataSO[] weaponData { get; private set; }

        private void Start()
        {
            RemoveEmptyPositions();
            currentIndex = 0;
        }

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

        public void TryChangeWeapon(WeaponDataSO newData)
        {
            OnWeaponDataChanged?.Invoke(0, newData);
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

                index = i;
                return true;
            }

            index = -1;
            return false;
        }

        public void TryChangeIndexWeapon(bool next)
        {
            if (weaponData.Length == 0) return;
            if (next)
            {
                currentIndex = (currentIndex + 1) % weaponData.Length;
            }
            else
            {
                currentIndex = (currentIndex + weaponData.Length - 1) % weaponData.Length;
            }
        }

        public void AddEmptyPosition()
        {
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

        public void RemoveEmptyPositions()
        {
            if (weaponData == null || weaponData.Length == 0)
            {
                return;
            }

            int nonNullCount = 0;
            foreach (var item in weaponData)
            {
                if (item != null)
                {
                    nonNullCount++;
                }
            }

            WeaponDataSO[] newWeaponData = new WeaponDataSO[nonNullCount];
            int newIndex = 0;
            for (int i = 0; i < weaponData.Length; i++)
            {
                if (weaponData[i] != null)
                {
                    newWeaponData[newIndex] = weaponData[i];
                    newIndex++;
                }
            }

            weaponData = newWeaponData;
        }

        public WeaponSwapChoice[] GetWeaponSwapChoices()
        {
            var choices = new WeaponSwapChoice[weaponData.Length];

            for (var i = 0; i < weaponData.Length; i++)
            {
                var data = weaponData[i];

                choices[i] = new WeaponSwapChoice(data, i);
            }

            return choices;
        }

        public void ChangeWeapon(bool c)
        {
            OnWeaponChanged.Invoke(c);
        }
    }
}