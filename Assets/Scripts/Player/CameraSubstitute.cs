using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSubstitute : MonoBehaviour
{
    // Attachesd camera to CameraSub to assist with rigidbody interactions and taking control over player cam
    [SerializeField]
    private Transform cameraPos;

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPos.position;
    }
}
