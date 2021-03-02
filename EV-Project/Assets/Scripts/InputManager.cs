using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is the Player InputManager that will handle all of the input by the player. 
/// It will have a feature that will trigger different uses for the buttons at different game states. 
/// </summary>

public class InputManager : MonoBehaviour
{
    float _throttleAxis;
    float _rotationAxis;
    bool _boost;
    bool _primaryFire;
    bool _secondaryFire;
    bool _auxillaryFire;
    bool _inventoryAccess;
    bool _open;
    bool _action;
    bool _pause;

 
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
    public float Throttle()
    {
        if (_throttleAxis > 0)
        {
            return _throttleAxis;
        }
        else return 0;
    }
    public float Reverse()
    {
        if (_throttleAxis < 0)
        {
            return -_throttleAxis;
        }
        else return 0;
    }
    public float Rotation()
    {
        return _rotationAxis;
    }
    public bool Boost()
    {
        return _boost;
    }
    public bool PrimaryFire()
    {
        return _primaryFire;
    }
    public bool SecondaryFire()
    {
        return _secondaryFire;
    }
    public bool AuxillaryFire()
    {
        return _auxillaryFire;
    }
}
