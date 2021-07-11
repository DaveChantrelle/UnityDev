using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// AI behaviour controller: this class will handle the decision making to feed into the state machine controller
/// </summary>
public class ShipAI : MonoBehaviour
{
    Rigidbody Rb;
    ModuleManager Mm;
    InputManager Im;
    VehicleController Vc;
    /*Ship stats*/
    string VehicleName;
    string VehicleModel;
    float shipMass = 5f;
    /*Movement Stats*/
    float thrustForce = 150f;
    float maxSpeed = 50f;
    float rotationSpeed = 2f;
    float boostSpeed = 50f;
    float boostThrust = 20f;
    float boostFuelLevel = 10f;
    /*Utility vars*/
    float boostFuelAdjustRate = 0.1f;
    float maxSpeedAdjustRate = 0.05f;
    float targetDeviation = 100f;
    Vector3 dvV = Vector3.zero;
    Transform currentTarget;
    Vector3 adjTarget;
    /*Movement state vars*/
    [SerializeField]
    float currentSpeed;
    bool isBoosting = false;
    bool hasBoost = true;
    /*Module Stats*/

    //Set up the states for the state machine
    // Awareness
    // Neutral / Searching / Alerted
    enum Awareness
    {
        None,
        Neutral,
        Searching,
        Alerted
    }
    // Orders
    // Hold (Point) / Patrol (Waypoints[]) / MoveTo (Point | Target) / Defend (Targets[] <=> Subject) / Engage (Targets[])

    // OrderState
    // Recieved / Acting / Completed

    // Actions
    // Communicate / Attack / Warp / Scan / Move / Idle / Transfer / Repair
    public void Start()
    {
        currentTarget = GameObject.FindGameObjectWithTag("Player").transform;
        Rb = GetComponent<Rigidbody>();
        Debug.Log(currentTarget + "Found");
        transform.LookAt(Vector3.zero);
        dvV = (Random.insideUnitCircle);
        gameObject.tag = "Vehicle";

    }
    public void Update()
    {
        //TEST: MoveTo(<player>)
        
        Vector3 dir;
        dir = MoveToward(currentTarget.position);
        //Vector3 _evadeTarget = TargetDeviation(currentTarget.position, dvV, targetDeviation);
        GameObject[] vehicles = GameObject.FindGameObjectsWithTag("Vehicle");
        if(vehicles.Length > 0)
        {
            foreach (GameObject v in vehicles)
            {
                if (Vector3.Distance(transform.position, v.transform.position) < 50f)
                {
                    if(v != gameObject)
                    {
                        dir = Evade(v.transform.position);
                    }
                    


                }
            }
        }
        else
        {
            Debug.LogWarning("No Vehicles found");
        }
        if (Vector3.Distance(transform.position, currentTarget.position) < 50f)
        {
            
            dir = Evade(currentTarget.position);


        }
        
        
        transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
        ApplyThrust(thrustForce);
        Debug.DrawRay(transform.position, Rb.velocity, Color.blue);
    }
    Vector3 Evade(Vector3 _target)
    {
        //_target = TargetDeviation(_target, dvV, targetDeviation);
        //Get the heading of the move
        Vector3 _heading = GetEvadeTargetVector(transform, _target, Rb.velocity);
        //Calculate the rotation toward the heading per frame
        float _singleStep = rotationSpeed * Time.deltaTime;
        //transform.Rotate(0, 0, _singleStep);
        Vector3 _direction = Vector3.RotateTowards(transform.up, _heading, _singleStep, 0f);
        //Zero out the direction as a precaution
        _direction.z = 0;

        Debug.DrawLine(transform.position, _target, Color.black);
        Debug.DrawRay(transform.position, _heading, Color.yellow);
        return _direction;
    }
    
    Vector3 GetAdjustedTargetVector(Transform t, Vector3 desiredTarget, Vector3 currentVelocity)
    {
        Vector3 _adjustedTargetVector = (desiredTarget - t.position) - currentVelocity;
        return _adjustedTargetVector;
    }
    Vector3 GetEvadeTargetVector(Transform t, Vector3 desiredTarget, Vector3 currentVelocity)
    {
        Vector3 _adjustedTargetVector = -(desiredTarget - t.position) - currentVelocity;
        return _adjustedTargetVector;
    }
    //Base Actions
    Vector3 MoveToward(Vector3 _target)
    {
        
        //Vector3 _moveTarget = TargetDeviation(_target, dvV, targetDeviation);
        //Get the heading of the move
        Vector3 _heading = GetAdjustedTargetVector(transform, _target, Rb.velocity);
        //Calculate the rotation toward the heading per frame
        float _singleStep = rotationSpeed * Time.deltaTime;
        float dot = Vector3.Dot(Rb.velocity.normalized, _heading.normalized);
        if (dot < -0.9f)
        {
            transform.Rotate(0, 0, _singleStep);
        }
        Vector3 _direction = Vector3.RotateTowards(transform.up, _heading, _singleStep, 0f);
        //Zero out the direction as a precaution
        _direction.z = 0;

        Debug.DrawLine(transform.position, _target, Color.red);
        Debug.DrawRay(transform.position, _heading, Color.green);
        return _direction;
    }
    void ApplyThrust(float _thrustForce)
    {
        //TODO: Clean this up
        
        //Get the thrust acceleration by using the thrustForce stat and the ship mass. Allows for cargo to affect the thruster performance
        float _thrust = _thrustForce / shipMass;
        Rb.AddRelativeForce(new Vector3(0, _thrust, 0));
        //For simplicity we cap the speed at maxspeed artificially - more complete speed handling is planned.   
        if (currentSpeed > maxSpeed)
        {
            Vector3 _normVelocity = Rb.velocity.normalized;
            Rb.velocity = _normVelocity * maxSpeed;
        }
        //Update the current speed
        currentSpeed = Rb.velocity.magnitude;

        
    }
    Vector3 TargetDeviation(Vector3 target, Vector3 deviationDir, float deviationMag)
    {
        
        Vector3 new_trgt;
        //Set a ranged amount of deviation to a target
        if ((transform.position - target).magnitude > deviationMag + 5f)
        {
            //While the AI is distant from target pos, calculate deviation on target
            new_trgt = target + ( deviationDir.normalized * deviationMag);
        }
        else
        {
            //As it gets closer, the heading gets accurate to ensure precise location arrival
            new_trgt = target;
            //Setting a new deviation here will not stutter the ships rotation as its not used this close
            dvV = (Random.insideUnitCircle);
        }

        return new_trgt;
    }
}
