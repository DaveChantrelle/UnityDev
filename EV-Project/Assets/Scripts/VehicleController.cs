using System.Collections;
using UnityEngine;
/// <summary>
/// This handles the movement controls of the currently used ship.
/// It is attached to the ship prefab and set up in the editor.
/// It is planned to have a player controller that will handle any currently used ship. 
/// </summary>
public class VehicleController : MonoBehaviour
{
    Player P;
    Rigidbody Rb;
    ModuleManager Mm;
    HardpointManager Hm;
    InputManager Im;
    /*Ship stats*/
    string VehicleName;
    string VehicleModel;
    float shipMass = 5f;
    /*Movement Stats*/
    [SerializeField]
    float thrustForce = 20f;
    [SerializeField]
    float maxSpeed = 100f;
    [SerializeField]
    float rotationSpeed = 0.5f;
    float boostSpeed = 50f;
    float boostThrust = 20f;
    [SerializeField]
    float boostFuelLevel = 10f;
    /*Utility vars*/
    float boostFuelAdjustRate = 0.1f;
    float maxSpeedAdjustRate = 0.05f;
    /*Movement state vars*/
    [SerializeField]
    float currentSpeed;
    bool isBoosting = false;
    bool hasBoost = true;
    /*Module Stats*/


    void Start()
    {
        P = GetComponentInParent<Player>();
        Im = GetComponentInParent<InputManager>();
        Rb = GetComponent<Rigidbody>();
        Mm = GetComponent<ModuleManager>();
        Hm = GetComponent<HardpointManager>();

    }
    
    void Update()
    {
        ShipMovement();
    }
    void FixedUpdate()
    {
        
    }
    /*ShipMovement*/
    #region ShipMovement
    void ShipMovement()
    {
        /*Reverse Heading*/
        if(Im.Reverse() > 0 )
        {
            //Check alignment of reverse heading in editor
            //Debug.DrawRay(this.transform.position, - Rb.velocity);

            // Set step size to a rotation speed times frame time.
            float _singleStep = Im.Reverse() * (10*rotationSpeed) * Time.deltaTime;
            
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
        /*Boost*/
        //Activate Boost state on input
        if (Im.Boost() && (Im.Throttle() > 0) && !isBoosting && hasBoost)
        {
            //Use a coroutine to allow for code loops and timed delay actions
            StartCoroutine(BoostedThrust());
        }
        /*Thrust*/
        //store requested thrust of the engine thrust and ship mass
        float _thrust = Im.Throttle() * thrustForce / shipMass;
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

        /*Rotation*/
        //get the requested rotation
        float _rotation = -Im.Rotation() * rotationSpeed;
        //apply the rotation if not Boosting
        if (!Im.Boost())
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
            while (Im.Boost() && hasBoost)
            {

                //This loop should contain a yield WaitForSeconds statement to allow incremental adjustment of fuel level
                yield return new WaitForSeconds(0.1f);
                boostFuelLevel -= boostFuelAdjustRate;
                if (boostFuelLevel <= 0)
                {
                    hasBoost = false;
                    boostFuelLevel = 0;
                }
            }
            //apply base values when boost is finished
            thrustForce = _t;
            //reduce maxSpeed over time
            while (maxSpeed > _s)
            {
                yield return maxSpeed -= maxSpeedAdjustRate;

            }
            maxSpeed = _s;
            isBoosting = false;
        
    }
    //**END ShipMovement**//
    #endregion
    
}

