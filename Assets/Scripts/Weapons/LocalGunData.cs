using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalGunData : MonoBehaviour
{
    public Transform muzzle;

    public AudioSource audioSource;

    public GunData gunData;

    public int currentAmmo;

    public bool isReloading;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = gunData.maxAmmo;
    }
}
