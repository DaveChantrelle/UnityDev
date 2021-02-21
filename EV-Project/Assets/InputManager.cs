using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    float _throttleAxis;
    float _rotationAxis;
    bool _boost;
    bool _primaryFire;
    bool _secondaryFire;
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
        _primaryFire = Input.GetButton("Fire1");
        _secondaryFire = Input.GetButton("Fire2");
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
}
