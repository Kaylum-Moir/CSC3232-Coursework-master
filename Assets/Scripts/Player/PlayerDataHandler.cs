using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDataHandler : MonoBehaviour
{
    public PlayerDefaultData defaultData;

    [SerializeField]
    private GameObject deathScreen;
    
    public int
    ammoStash,
    health;

    [SerializeField]
    private TextMeshProUGUI 
    ammoCounter,
    healthCounter;

    void Start()
    {
        ammoStash = defaultData.ammoStash;
        health = defaultData.maxHealth;

        AgentGunController.damagePlayer += ApplyDamage;
        ammoCounter.text = ammoStash.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ApplyDamage(int damagePoints)
    {
        health -= damagePoints;
        // Do something to visualise taking hit
        Debug.Log("Player took "+damagePoints+"hp");

        healthCounter.text = Mathf.RoundToInt(((float)health / (float)defaultData.maxHealth) * 100).ToString() + "%";

        if (health <= 0)
        {
            Die();
        }
    }

    public void RemoveAmmo(int ammo)
    {
        ammoStash -= ammo;
        ammoCounter.text = ammoStash.ToString();
    }

    private void Die()
    {
        deathScreen.SetActive(true);
        StartCoroutine(DeathRestart());
    }

    private IEnumerator DeathRestart()
    {
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnDestroy()
        {
            AgentGunController.damagePlayer -= ApplyDamage;
        }
}
