using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : InputManager
{
    
    void Update()
    {
        //Vehicle Controls
        _throttleAxis = Input.GetAxis("Vertical");
        _rotationAxis = Input.GetAxis("Horizontal");
        _boost = Input.GetButton("Boost");
        _primaryFire = Input.GetButtonDown("Fire1");
        _secondaryFire = Input.GetButtonDown("Fire2");
        _auxillaryFire = Input.GetButtonDown("Fire3");
        _open = Input.GetButtonDown("Open");
        _action = Input.GetButtonDown("Action");
        //Suit Controls

        //Menu Controls

        //Global Controls
        _pause = Input.GetButtonDown("Pause");

    }

    //Access Methods
    public override float Throttle()
    {
        if (_throttleAxis > 0)
        {
            return _throttleAxis;
        }
        else return 0;
    }
    public override float Reverse()
    {
        if (_throttleAxis < 0)
        {
            return -_throttleAxis;
        }
        else return 0;
    }
    public override float Rotation()
    {
        return _rotationAxis;
    }
    public override bool Boost()
    {
        return _boost;
    }
    public override bool PrimaryFire()
    {
        return _primaryFire;
    }
    public override bool SecondaryFire()
    {
        return _secondaryFire;
    }
    public override bool AuxillaryFire()
    {
        return _auxillaryFire;
    }
    public override bool Pause()
    {
        return _pause;
    }
    
}
