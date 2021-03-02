using UnityEngine;
/// <summary>
/// Base class for all fireable weapons
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    //Declarations
    public enum AmmoType
    {
        Laser,
        Kenetic,
        Plasma,
        Electric

    };
    [SerializeField]
    protected AmmoDatabase Ad;
    public Sprite icon;
    public GameObject model;
    //Value Fields
    protected int _ammoCount;
    protected int _ammoCapacity;
    protected float _range;
    protected float _muzzleVelocity;
    protected float _firerate;
    protected float _damage;
    //Utility Fields
    [SerializeField]
    protected float _firedTime;
    protected bool _canFire;
    
    //abstract Base Fields
    protected float _baseRange;
    protected int _baseCapacity;
    protected float _baseFirerate;
    protected float _baseDamage;
    //Context Methods
    public abstract string GetWeaponName();
    public abstract string GetWeaponDescription();
    public abstract AmmoType GetAmmoType();
    //Properties
    public float BaseRange { get => _baseRange; set => _baseRange = value; }
    public int BaseCapacity { get => _baseCapacity; set => _baseCapacity = value; }
    public float BaseFirerate { get => _baseFirerate; set => _baseFirerate = value; }
    public float BaseDamage { get => _baseDamage; set => _baseDamage = value; }
    public virtual int AmmoCount { get => _ammoCount; set => _ammoCount = Mathf.Min(_ammoCapacity, value); }
    public virtual int AmmoCapacity { get => _ammoCapacity; set => _ammoCapacity = value; }
    public virtual float MuzzleVelocity { get => _muzzleVelocity; set => _muzzleVelocity = value; }
    public virtual float Firerate { get => _firerate; set => _firerate = value; }
    public virtual float Range { get => _range; set => _range = value; }
    //Updaters
    public virtual int UpdateAmmoCapacity(int _modifiedCapacity = 0) { return _ammoCapacity = _baseCapacity + _modifiedCapacity; }
    public virtual float UpdateFirerate(float _modifiedFirerate = 0) { return _firerate = _baseFirerate + _modifiedFirerate; }
    public virtual float UpdateRange(float _modifiedRange = 0) { return _range = _baseRange + _modifiedRange; }
    public virtual float UpdateDamage(float _modifiedDamage = 0) { return _damage = _baseDamage + _modifiedDamage; }
    //Methods
    public virtual void SetBaseValues(float r, int c, float f, float d )
    {
        BaseRange = r;
        BaseCapacity = c;
        BaseFirerate = f;
        BaseDamage = d;
        GetMuzzleVelocity();
        UpdateAmmoCapacity();
        UpdateDamage();
        UpdateFirerate();
        UpdateRange();
        Ad.Load();
    }
    public virtual float GetMuzzleVelocity() 
    { 
        switch (GetAmmoType())
        {
            case AmmoType.Laser:
                {
                    _muzzleVelocity = 0f;

                    break;
                }
            case AmmoType.Kenetic:
                {
                    _muzzleVelocity = 10f;
                    break;
                }
            case AmmoType.Plasma:
                {
                    _muzzleVelocity = 20f;
                    break;
                }
            case AmmoType.Electric:
                {
                    _muzzleVelocity = 0f;
                    break;
                }
        }
        return _muzzleVelocity;
    }
    //Weapon contract to be able to fire
    public abstract bool Fire();
    

}
