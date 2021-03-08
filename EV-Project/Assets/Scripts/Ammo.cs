using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ammo : MonoBehaviour
{
    public abstract string GetAmmoName();
    protected Vector3 impact = Vector3.zero;
    float _range;

    public float Range { get => _range; set => _range = value; }

    public Vector3 Impact { get => impact; set => impact = value; }
}
