using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public WaveController waveController;

    public void StartWave()
    {
        transform.position += Vector3.down * 10;
        waveController.StartWaves();
    }
}
