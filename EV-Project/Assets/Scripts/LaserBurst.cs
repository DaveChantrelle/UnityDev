using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBurst : Ammo
{
    string ammoName = "Laser Burst";
    public override string GetAmmoName()
    {
        return ammoName;
    }
    public void Start()
    {
        StartCoroutine(ApplyVelocity());
    }
    IEnumerator ApplyVelocity()
    {
        yield return new WaitForEndOfFrame();
        if (Impact.magnitude != 0)
        {
            transform.position = Impact;
        }
        else
        {
            transform.position += (transform.forward * Range);
        }
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);

    }
       

}
