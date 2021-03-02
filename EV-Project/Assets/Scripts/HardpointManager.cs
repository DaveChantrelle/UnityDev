using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///<summary>
/// This Manager handles the functionality of all of the current ships weapon firing and reloading etc.
/// Attached to the ship prefab so hardpoint definitions and assignment is handled in the editor.
/// </summary>
[Serializable]
public class HardpointManager : MonoBehaviour
{
    InputManager Im;
    [SerializeField]
    private WeaponDatabase Wd;
    public List<HardPoint> primaryWeapons;
    public List<HardPoint> secondaryWeapons;
    public List<HardPoint> auxillaryWeapons;
    private void Awake()
    {
        
    }
    void Start()
    {
        Im = GetComponentInParent<InputManager>();
        Wd.Load();
        bool installed = primaryWeapons[0].InstallWeapon(WeaponDatabase.GetWeapon("Laser Cannon"));
        if (installed)
            Debug.Log("Installed ");
    }

    // Update is called once per frame, so weapon firing will happen everyframe the button is held down. 
    //Other logic will determine if the weapon actually fires that frame.
    //This will allow for beam type weapons to be drawn each frame from the same fire logic.
    //Note: as this makes a coroutine call everyframe is likely very inefficient.
    void Update()
    {
        if (Im.PrimaryFire())
        {
            StartCoroutine(FireWeapons(primaryWeapons));
        }
        if (Im.SecondaryFire())
        {
            StartCoroutine(FireWeapons(secondaryWeapons));
        }
        if (Im.AuxillaryFire())
        {
            StartCoroutine(FireWeapons(auxillaryWeapons));
        }
    }
    IEnumerator FireWeapons(List<HardPoint> _hp)
    {
        
        foreach (HardPoint h in _hp)
        {
            if (h.IsActive())
            {
                bool f = h.FireWeapon();
                Debug.Log(h + " has fired "+ f);
            }
            
            yield return null;

        }
        
    }
}
