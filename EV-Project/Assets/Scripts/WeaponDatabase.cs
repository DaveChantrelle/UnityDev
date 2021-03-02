using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Weapons", menuName = "Weapon Database")]
public class WeaponDatabase : ScriptableObject
{
    public Weapon[] weapons;

    static protected Dictionary<string, Weapon> _weaponDict;

    public void Load()
    {
        if (_weaponDict == null)
        {
            _weaponDict = new Dictionary<string, Weapon>();

            for (int i = 0; i < weapons.Length; ++i)
            {
                _weaponDict.Add(weapons[i].GetWeaponName(), weapons[i]);
            }
        }
    }

    static public Weapon GetWeapon(string name)
    {
        return _weaponDict.TryGetValue(name, out Weapon w) ? w : null;
    }
    public void Awake()
    {

    }
}
