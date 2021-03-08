using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InputManager IM;
    // Start is called before the first frame update
    void Start()
    {
        IM = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IM.Pause())
        {
            Application.Quit();
        }
    }
}
