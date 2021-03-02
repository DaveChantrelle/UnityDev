using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ammo", menuName = "Ammo Database")]
public class AmmoDatabase : ScriptableObject
{
    public Ammo[] ammo;

    static protected Dictionary<string, Ammo> _ammoDict;

    public void Load()
    {
        if (_ammoDict == null)
        {
            _ammoDict = new Dictionary<string, Ammo>();

            for (int i = 0; i < ammo.Length; ++i)
            {
                _ammoDict.Add(ammo[i].GetAmmoName(), ammo[i]);
            }
        }
    }

    static public Ammo GetAmmo(string name)
    {
        return _ammoDict.TryGetValue(name, out Ammo w) ? w : null;
    }
    public void Awake()
    {

    }
}
