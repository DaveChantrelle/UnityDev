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
        return AI.PrimaryTrigger;
        //throw new System.NotImplementedException();
    }
   public override bool SecondaryFire()
    {
        return false;
        //throw new System.NotImplementedException();
    }
    public override bool AuxillaryFire()
    {
        return false;
        //throw new System.NotImplementedException();
    }
    public override bool Pause()
    {
        return _pause;
    }

    // Start is called before the first frame update
    public void Awake()
    {
        AI = GetComponent<ShipAI>();
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
}
