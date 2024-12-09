using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 4f))
            {
                if (hit.transform.CompareTag("Weapon_Item"))
                {
                    Debug.Log("Weapon Found");
                }
                else if (hit.transform.name == "Extract Button")
                {
                    hit.collider.GetComponent<ButtonScript>().Invoke("StartWave", 0f);
                    
                }
                else
                {
                    Debug.Log("No Weapon Found");
                }
            }
        }
    }
}
