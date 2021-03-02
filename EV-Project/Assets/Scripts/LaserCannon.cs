using System;
using UnityEngine;

public class LaserCannon : Weapon
{
    //Instance Fields
    string weaponName = "Laser Cannon";
    string weaponDescription = "A cannon that fires lasers ?? !";
    float range = 3000;
    int capacity = 1000;
    float firerate = 10;
    float damage = 300;
    //Context Methods
    public override string GetWeaponName()
    {
        return weaponName;
    }
    public override string GetWeaponDescription()
    {
        return weaponDescription;
    }
    public override AmmoType GetAmmoType()
    {
        return AmmoType.Laser;
    }
    public override bool Fire()
    {
        float t = Time.time;
        if(t > _firedTime + 1/ Firerate)
        {
            Ammo a = Instantiate(AmmoDatabase.GetAmmo("Laser Burst"));
            a.transform.position = transform.position;
            a.transform.rotation = transform.rotation;
            return true;
        }else
        {
            return false;
        }
    }
    //Methods

    //Monobehaviour
    void Awake()
    {
        SetBaseValues(range, capacity, firerate, damage);
    }
    private void Start()
    {
        
    }
}
