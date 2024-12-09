using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.iOS;

public class GunController : MonoBehaviour
{
    private LocalGunData localGunData;

    private GunData gunData;

    private float shotCooldown;

    PlayerDataHandler playerData;

    [SerializeField]
    private TextMeshProUGUI ammoCount;


    // Start is called before the first frame update
    void Start()
    {
        localGunData = transform.GetComponentInChildren<LocalGunData>();
        gunData = localGunData.gunData;
        playerData = transform.parent.parent.transform.GetComponent<PlayerDataHandler>();
    }

    public void Shoot()
    {
        //Debug.Log("Player Fired");
        if (localGunData.currentAmmo > 0f && !localGunData.isReloading && shotCooldown > 1f / (gunData.fireRate / 60f)) // fireRate is maximum shots per minute
        {
            if (Physics.Raycast(transform.parent.position, transform.parent.forward, out RaycastHit hit, gunData.maxRange))
            {
                //Debug.Log("Hit "+ hit.transform.name);
                hit.transform.GetComponent<HitBox>()?.OnRaycastHit(CalculateDamage(hit), transform.forward);
            }
            SFX(gunData.fireSounds);
            Debug.DrawRay(transform.position, transform.forward * 50f, Color.red, duration: 1f);
            localGunData.currentAmmo --;
            ammoCount.text = localGunData.currentAmmo.ToString();
            shotCooldown = 0;
        }
    }

    public void StartReload() 
    {
        if (!localGunData.isReloading && playerData.ammoStash > 0f)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload() 
    {
        localGunData.isReloading = true;
        SFX(gunData.reloadSounds);
        yield return new WaitForSeconds(gunData.reloadTime);

        if ((float)playerData.ammoStash / (float)gunData.maxAmmo > 0)
        {
            localGunData.currentAmmo = gunData.maxAmmo;
            playerData.RemoveAmmo(gunData.maxAmmo);
        }
        else
        {
            localGunData.currentAmmo = playerData.ammoStash;
            playerData.RemoveAmmo(playerData.ammoStash);
        }
        ammoCount.text = localGunData.currentAmmo.ToString();
        localGunData.isReloading = false;
    }

    private void SFX(AudioClip[] audios)
    {
        localGunData.audioSource.PlayOneShot(audios[UnityEngine.Random.Range(0, audios.Length)]);
    }

    private float CalculateDamage(RaycastHit hit) {
        if (hit.distance <= gunData.maxRange / 2)
        {
            return gunData.baseDamage;
        }
        else if (hit.distance > gunData.maxRange)
        {
            return 0f;
        }
        else
        {
            float normalizedDistance = (hit.distance - gunData.maxRange/2) / (gunData.maxRange/2); // Gets distance over damage dropoff thrshold (gunData.maxRange/2) as a value from 0 to 1
            return Mathf.RoundToInt(gunData.baseDamage * (1 - normalizedDistance * normalizedDistance)); // Applies damage after falloff to a quadratic curve and rounds it to an integer
        }
    }

    private void Update()
    {
        shotCooldown += Time.deltaTime;

        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
            
        if (Input.GetButtonDown("Reload"))
        {
            StartReload();
        }
    }
}