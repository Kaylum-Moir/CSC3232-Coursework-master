using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Player")]
public class PlayerDefaultData : ScriptableObject
{
    public new string name;

    public int 
    ammoStash,
    maxHealth;
}
