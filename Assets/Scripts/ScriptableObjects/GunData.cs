using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapons/Gun")]
public class GunData : ScriptableObject
{
    public new string name;

    public float 
    baseDamage,
    maxRange,
    fireRate,
    reloadTime,
    bulletSpread;

    public int 
    maxAmmo;

    public bool isReloading;

    public AudioClip[] 
    fireSounds,
    reloadSounds;
}
