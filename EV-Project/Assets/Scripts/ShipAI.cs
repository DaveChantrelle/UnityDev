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
    [SerializeField]
    float thrustForce = 100f;
    [SerializeField]
    float maxSpeed = 50f;
    [SerializeField]
    float rotationSpeed = 0.3f;
    float boostSpeed = 50f;
    float boostThrust = 20f;
    [SerializeField]
    float boostFuelLevel = 10f;
    /*Utility vars*/
    float boostFuelAdjustRate = 0.1f;
    float maxSpeedAdjustRate = 0.05f;
    float targetDeviation = 100f;
    Vector3 dvV;
    Vector3 currentTarget;
    /*Movement state vars*/
    [SerializeField]
    float currentSpeed;
    bool isBoosting = false;
    bool hasBoost = true;

    float _lastMove = 0;
    Vector3 _moveTarget;
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
        Vc = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<VehicleController>();
        Rb = GetComponent<Rigidbody>();
        Debug.Log(Vc + "Found");
        transform.LookAt(Vector3.zero);
        dvV = (Random.insideUnitCircle * targetDeviation);

    }
    public void Update()
    {
        //TEST: MoveTo(<player>)
        _moveTarget = Vc.transform.position; 
        MoveTo(_moveTarget);
    }

    void UpdateAwareness(Awareness _forcedAwareness = 0)
    {

    }

    //Base Actions
    void MoveTo(Vector3 _target)
    {
        //Set a small amount of deviation on the move target
        if((transform.position - _target).magnitude > dvV.magnitude)
        {
            //While the AI is distant from target pos, calculate deviation on target
            currentTarget = _target + (2*dvV);
        }
        else
        {
            //As it gets closer, the heading gets accurate to ensure precise location arrival
            currentTarget = _target;
            //Setting a new deviation here will not stutter the ships rotation as its not used this close
            dvV = (Random.insideUnitCircle * targetDeviation);
        }
        
        //Get the heading of the move
        Vector3 _heading = (currentTarget - transform.position).normalized;

        //Calculate the rotation toward the heading per frame
        float _singleStep = 5 * rotationSpeed * Time.deltaTime;
        Vector3 _direction = Vector3.RotateTowards(transform.up, _heading, _singleStep, 0f);
        //Zero out the direction as a precaution
        _direction.z = 0;

        //Course adjust logic
        if(Vector3.Dot(Rb.velocity.normalized, _heading) < -0.9)
        {
            //If the velocity and heading are close to opposite adjust the course by a single step to let the RotateToward function to work.
            transform.Rotate(0, 0, _singleStep);
        }
        if (Vector3.Dot(Rb.velocity.normalized, _heading) < 0.7 && currentSpeed > 5)
        {
            //While moving faster than 5m/s and the velocity and the heading dont match
            Vector3 calcDir; //New heading vector
            if (Vector3.SignedAngle(Rb.velocity, _heading,Vector3.forward) > 10)
            {
                //If the difference in angle of the velocity and heading is a positive angle
                //Course correct 90 degrees
                calcDir = new Vector3(-_heading.y, _heading.x, 0);
            }
            else if(Vector3.SignedAngle(Rb.velocity, _heading, Vector3.forward) < -10)
            {
                //Course correct in the opposite direction when the angle is negative
                calcDir = new Vector3(_heading.y, -_heading.x, 0);
            }else
            {
                //If the angles are with 10/-10 continue with the original heading
                calcDir = _heading;
            }
            
            //If the heading is behind the Ship set a new heading for 180 degrees, only when the distance is 100m or more
            if(180 - Vector3.Angle(Rb.velocity, _heading) < 5 || (Vector3.Distance(_target, transform.position) > 100f))
            {
                calcDir = new Vector3(-Rb.velocity.x, -Rb.velocity.y, 0);
                Debug.Log("Target distance is " + Vector3.Distance(_target, transform.position) + " " + Time.time );
            }
            //Calulate the new step rotation and apply it
            Vector3 _adjDir = Vector3.RotateTowards(transform.up, calcDir, _singleStep, 0f);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _adjDir);
        }
        else
        {
            //Use the original direction if the ship is already on target
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _direction);
        }
        //Get the thrust acceleration by using the thrustForce stat and the ship mass. Allows for cargo to affect the thruster performance
        float _thrust = thrustForce / shipMass;
        float d = Vector3.Distance(_target, transform.position);
        if(d > 50f)
        {
            //Add thrust when further than 50m
            Rb.AddRelativeForce(new Vector3(0, _thrust, 0));
        }
        else
        {
            if(d < 25)
            {
                //For simplicity its cleaner to just slow the ship down artificially at close proximity
                //Although setting the velocity of a rigidbody isnt good practise
                Vector3 _normVelocity = Rb.velocity.normalized;
                Rb.velocity -= _normVelocity;
            }
            
        }
        
        //Again for simplicity we cap the speed at maxspeed artificially   
        if (currentSpeed > maxSpeed)
        {
            Vector3 _normVelocity = Rb.velocity.normalized;
            Rb.velocity = _normVelocity * maxSpeed;   
        }
        //Update the current speed
        currentSpeed = Rb.velocity.magnitude;
 
    }
    void Idle()
    {

    }
    void Attack(GameObject _target)
    {

    }
    void Scan()
    {

    }
    void Communicate(GameObject _target, string _message)
    {

    }
    void Warp()
    {

    }
    void Transfer()
    {

    }
    void Repair()
    {

    }
}
