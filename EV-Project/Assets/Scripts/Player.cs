using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Handles player data and tracks player state changes.
/// </summary>
public class Player : MonoBehaviour
{
    public InputManager Im;
    // Start is called before the first frame update
    void Start()
    {
        Im = GetComponent<InputManager>();
    }

    
}
