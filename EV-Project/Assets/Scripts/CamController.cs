using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    Player P;
    [SerializeField]
    float camOffset = 250f;
    Vector3 _targetPos;
    // Start is called before the first frame update
    void Start()
    {
        P = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Make the getcomponent call happen on awake and then only again when vehilce changes.
        //--Still error check before assigning
        if (P.GetComponentInChildren<VehicleController>() != null)
        {
            _targetPos = P.GetComponentInChildren<VehicleController>().transform.position;
            transform.position = new Vector3(_targetPos.x, _targetPos.y, -camOffset);
        }
    }
    
}
