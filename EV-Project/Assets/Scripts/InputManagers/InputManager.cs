using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is the InputManager base clase that will handle input. 
/// It will have a feature that will trigger different uses for the buttons at different game states. 
/// </summary>

public abstract class InputManager : MonoBehaviour
{
    
    protected float _throttleAxis;
    protected float _rotationAxis;
    protected bool _boost;
    protected bool _primaryFire;
    protected bool _secondaryFire;
    protected bool _auxillaryFire;
    protected bool _inventoryAccess;
    protected bool _open;
    protected bool _action;
    protected bool _pause;

    //Access Methods
    public abstract float Throttle();
    public abstract float Reverse();
    public abstract float Rotation();
    public abstract bool Boost();
    public abstract bool PrimaryFire();
    public abstract bool SecondaryFire();
    public abstract bool AuxillaryFire();
    public abstract bool Pause();
}
