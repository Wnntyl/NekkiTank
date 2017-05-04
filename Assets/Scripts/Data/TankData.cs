using System;
using UnityEngine;

[Serializable]
public class TankData: EntityData
{
    public string[] weapons;

    private WeaponData[] _weapons;

    public WeaponData GetWeapon(int index)
    {
        LoadWeapons();

        if (index < 0 || index >= weapons.Length)
            return null;

        return _weapons[index];
    }

    private void LoadWeapons()
    {
        if (_weapons != null)
            return;

        _weapons = new WeaponData[weapons.Length];
        for (var i = 0; i < weapons.Length; i++)
        {
            var textAsset = Resources.Load("Data/Weapons/" + weapons[i]) as TextAsset;
            if (textAsset != null)
                _weapons[i] = JsonUtility.FromJson<WeaponData>(textAsset.text);
        }
    }
}