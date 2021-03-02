using System;
using UnityEngine;
/// <summary>
/// This is a base hardpoint for a ship prefab, it will have its own GO on the prefab which will double as the weapon mount point.
/// </summary>
public class HardPoint : MonoBehaviour
{
    [SerializeField]
    Weapon _weapon = null;
    bool _isActive = true;

    public Weapon Weapon { get => _weapon; set => _weapon = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool IsActive()
    {
        
        _isActive = this.gameObject.activeInHierarchy;
        return _isActive;
    }
    public bool InstallWeapon(Weapon w)
    {
        bool _intalled = false;
        Weapon = Instantiate(w, transform);
        Debug.Log(w);
        if(Weapon != null)
        {
            _intalled = true;
        }
        return _intalled;
    }
    public bool FireWeapon()
    {
        bool fired = Weapon.Fire();
        Debug.Log(this + " has fired = " + fired);
        return fired;

    }
}
