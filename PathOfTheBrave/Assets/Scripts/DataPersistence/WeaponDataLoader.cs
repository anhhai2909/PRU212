using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Weapons;

public static class WeaponDataLoader 
{
    // Start is called before the first frame update
    public static WeaponDataSO GetWeaponDataByName(string name)
    {
        string[] guids = AssetDatabase.FindAssets("t:WeaponDataSO", new[] { "Assets/Data/WeaponLoad" });

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            WeaponDataSO weaponData = AssetDatabase.LoadAssetAtPath<WeaponDataSO>(path);

            if (weaponData != null && weaponData.Name == name)
            {
                return weaponData;
            }
        }

        return null;
    }
}
