using System;
using UnityEngine;

public class LaserCannon : Weapon
{
    //Instance Fields
    string weaponName = "Laser Cannon";
    string weaponDescription = "A cannon that fires lasers ?? !";
    string weaponAmmo = "Laser Burst";
    float range = 200;
    int capacity = 1000;
    float firerate = 3;
    float damage = 30;
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
        //Check if the weapon can fire
        if(t > _firedTime + 1/ Firerate && AmmoCount > 0)
        {
            //Spawn the ammo
            Ammo a = Instantiate(AmmoDatabase.GetAmmo(weaponAmmo));
            //force ammo to position / rotation of weapon
            a.transform.position = transform.position;
            a.transform.rotation = transform.rotation;
            a.GetComponentInChildren<TrailRenderer>().startColor = new Color(1,0,0,0.1f);
            //Cast Ray to check hit on target
            if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Range))
            {
                Debug.Log("Target Hit!!");
                //Declare the damagable ref var
                IDamagable targetHit;
                //if the hit tartget is damagable
                if ((targetHit = hit.collider.gameObject.GetComponent<IDamagable>()) != null)
                {
                    //invoke the Damage method on the target
                    targetHit.Damage(Damage/Firerate);
                }
                //On hit set the ammo impact point to he hit point
                a.Impact = hit.point;
            }
            else
            {
                //no hit registered reset the impact point and set the range value
                a.Impact = Vector3.zero;
                a.Range = Range;
            }
            //Weapon has fired so deplete some ammo
            UpdateAmmoCount(-1);
            _firedTime = t;
            return true;
        }else
        {
            //Weapon did not fire
            return false;
        }
    }
    //Methods

    //Monobehaviour
    void Awake()
    {
        //Sets up the weapon from the derived class values
        SetBaseValues(range, capacity, firerate, damage);
    }
}
