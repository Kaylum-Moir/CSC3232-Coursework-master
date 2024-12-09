using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.iOS;

public class AgentGunController : MonoBehaviour
{
    private LocalGunData localGunData;

    private GunData gunData;

    private Transform parent;

    private float shotCooldown;

    public static System.Action<int> damagePlayer;


    // Start is called before the first frame update
    void Start()
    {
        localGunData = transform.GetComponentInChildren<LocalGunData>();
        gunData = localGunData.gunData;

        parent = transform.parent.transform.parent.transform;
    }

    public void Shoot()
    {
        Debug.Log("AGENT TRIED TO SHOOT");
        if (localGunData.currentAmmo > 0f && !localGunData.isReloading && shotCooldown > 1f / (gunData.fireRate / 60f)) // fireRate is maximum shots per minute
        {
            Debug.DrawRay(parent.transform.position, parent.transform.forward * 50f, Color.red, duration: 1f);
            if (Physics.Raycast(parent.transform.position, parent.transform.forward, out RaycastHit hit, gunData.maxRange))
            {
                Debug.Log("Hit "+ hit.transform.name);
                if (hit.transform.GetComponent<Collider>().tag == "Player")
                {
                    Debug.Log("PLAYER DAMAGED");
                    damagePlayer?.Invoke(CalculateDamage(hit));
                }
            }
            SFX(gunData.fireSounds);
            localGunData.currentAmmo --;
            shotCooldown = 0;
        }
        else if (localGunData.currentAmmo == 0f)
        {
            StartReload();
        }
        else
        {
            Debug.Log("AGENT FAILED TO SHOOT");
            Debug.Log("Ammo Count: "+localGunData.currentAmmo.ToString());
            Debug.Log("Is Reloading: "+localGunData.isReloading);
            Debug.Log("Shot Cooldown: "+(shotCooldown > 1f / (gunData.fireRate / 60f)).ToString());
        }
    }

    public void StartReload() 
    {
        if (!localGunData.isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload() 
    {
        localGunData.isReloading = true;
        SFX(gunData.reloadSounds);
        yield return new WaitForSeconds(gunData.reloadTime);
        localGunData.currentAmmo = gunData.maxAmmo;
        localGunData.isReloading = false;
    }

    private void SFX(AudioClip[] audios)
    {
        localGunData.audioSource.PlayOneShot(audios[Random.Range(0, audios.Length)]);
    }

    private int CalculateDamage(RaycastHit hit) {
        if (hit.distance <= gunData.maxRange / 2)
        {
            return Mathf.RoundToInt(gunData.baseDamage);
        }
        else if (hit.distance > gunData.maxRange)
        {
            return 0;
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
    }
}

