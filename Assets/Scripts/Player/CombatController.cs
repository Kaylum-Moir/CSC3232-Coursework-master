using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public static Action shoot;
    public static Action reload;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            shoot?.Invoke();
        }
            
        if (Input.GetButtonDown("Reload"))
        {
            reload?.Invoke();
        }
    }
}
