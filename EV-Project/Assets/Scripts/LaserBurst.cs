using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBurst : Ammo
{
    string ammoName = "Laser Burst";
    Vector3 _origin;
    public override string GetAmmoName()
    {
        return ammoName;
    }
    public void Start()
    {
        _origin = transform.position;
    }
    public void Update()
    {
        float offset = transform.position.z;
        transform.Translate(Vector3.forward * 50, Space.Self);
        if ((transform.position + _origin).magnitude > 1000)
        {
            Destroy(this.gameObject);
        }
    }

}
