using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    Player P;
    Rigidbody Rb;
    //Ship stats
    float shipMass = 1f;
    //Movement Stats
    [SerializeField]
    float thrustForce = 2f;
    [SerializeField]
    float maxSpeed = 30f;
    [SerializeField]
    float rotationSpeed = 0.5f;
    float boostSpeed = 50f;
    float boostThrust = 5f;
    [SerializeField]
    float boostFuelLevel = 10f;
    //Movement state vars
    [SerializeField]
    float currentSpeed;
    bool isBoosting = false;
    bool hasBoost = true;
    //Weapon Stats



    void Start()
    {
        P = GetComponentInParent<Player>();
        Rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ShipMovement();
    }
    //ShipMovement
    #region ShipMovement
    void ShipMovement()
    {
        //Reverse Heading
        if(P.IM.Reverse() > 0 )
        {
            Debug.DrawRay(this.transform.position, - Rb.velocity);
            // Set step size to a rotation speed times frame time.
            float _singleStep = (10*rotationSpeed) * Time.deltaTime;

            // Rotate the up vector towards the target direction by one step
            Vector3 _direction = Vector3.RotateTowards(transform.up, -Rb.velocity, _singleStep, 0f);
            //zero out the z value
            _direction.z = 0;

            // set the transform look rotation
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _direction);
            //if reverse is activated and speed falls below parking speed (10% of maxspeed), reduce speed to 0
            if(currentSpeed < (maxSpeed * 0.1f))
            {
                Rb.velocity = Vector3.zero;
            }
        }
        //Activate Boost state on input
        if (P.IM.Boost() && (P.IM.Throttle() > 0) && !isBoosting && hasBoost)
        {
            StartCoroutine("BoostedThrust");
        }
        //store requested thrust of the engine thrust and ship mass
        float _thrust = P.IM.Throttle() * thrustForce / shipMass;
        //apply requested force as a relative vector3
        Rb.AddRelativeForce(new Vector3(0, _thrust, 0));
        //control the maximum speed
        if (currentSpeed > maxSpeed)
        {
            //store the velocity as a vector with a magnatude of 1
            Vector3 _normVelocity = Rb.velocity.normalized; 
            //set that vector to maxspeed limit
            Rb.velocity = _normVelocity * maxSpeed;
        }
        //update the current speed
        currentSpeed = Rb.velocity.magnitude;

        //get the requested rotation
        float _rotation = -P.IM.Rotation() * rotationSpeed;
        //apply the rotation if not Boosting
        if (!P.IM.Boost())
        {
            this.transform.Rotate(0, 0, _rotation);
        }
    }
    //Boosting Coroutine
    IEnumerator BoostedThrust()
    {
        //store the speed and thrust
        float _s = maxSpeed;
        float _t = thrustForce;
        //Apply the boost values
        maxSpeed += boostSpeed;
        thrustForce += boostThrust;
        isBoosting = true;
        
        //Keep boosting for duration of input and fuel level
        while (P.IM.Boost() && hasBoost)
        {
            
            yield return new WaitForSeconds(0.1f);
            boostFuelLevel  = boostFuelLevel -0.1f;
            if(boostFuelLevel < 0)
            {
                hasBoost = false;
                boostFuelLevel = 0;
            }
        }
        //apply base values
        thrustForce = _t;
        //reduce maxSpeed over time
        while(maxSpeed > _s)
        {
            yield return maxSpeed-= 0.05f;
            
        }
        maxSpeed = _s;
        isBoosting = false;
    }
    //**END ShipMovement**//
    #endregion
}
