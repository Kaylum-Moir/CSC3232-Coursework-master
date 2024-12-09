using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class EnemyData : ScriptableObject
{
    public string enemyType;

    public float
    detectionRange,
    speed,
    aggressiveness,
    roamingRange,
    broadcastRange; // Broadcast where the player is to other agents

    public int
    maxHealth;
}
