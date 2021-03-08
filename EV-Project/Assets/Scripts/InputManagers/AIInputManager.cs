using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInputManager : InputManager
{
    ShipAI AI;
    
    public override float Throttle()
    {
        throw new System.NotImplementedException();
    }
   public override float Reverse()
    {
        throw new System.NotImplementedException();
    }
    public override float Rotation()
    {
        throw new System.NotImplementedException();
    }
    public override bool Boost()
    {
        throw new System.NotImplementedException();
    }
     public override bool PrimaryFire()
    {
        throw new System.NotImplementedException();
    }
   public override bool SecondaryFire()
    {
        throw new System.NotImplementedException();
    }
    public override bool AuxillaryFire()
    {
        throw new System.NotImplementedException();
    }
    public override bool Pause()
    {
        return _pause;
    }

    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
}
