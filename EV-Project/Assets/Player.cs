using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InputManager IM;
    // Start is called before the first frame update
    void Start()
    {
        IM = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}