using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    Player P;
    [SerializeField]
    float camOffset = 80f;
    // Start is called before the first frame update
    void Start()
    {
        P = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _targetPos = P.GetComponentInChildren<VehicleController>().transform.position;
        this.transform.position = new Vector3(_targetPos.x, _targetPos.y, -camOffset);
    }
}
