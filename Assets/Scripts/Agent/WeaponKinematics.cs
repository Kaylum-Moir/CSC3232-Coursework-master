using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponKinematics : MonoBehaviour
{
    public GameObject target;
    private AISensor sensor;

    void Start()
    {
        sensor = transform.GetComponent<AISensor>();
    }

    void LateUpdate()
    {
        if (sensor.playerVisible)
        {
            transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
        }
    }
}
