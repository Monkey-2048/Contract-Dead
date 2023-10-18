using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieVisuals : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        // Find the player's transform
        player = ZPlayerManager.instance.player.transform;
    }

    public void LookAtPlayer()
    {
        if (player != null)
        {
            // Calculate the direction to the player
            Vector3 directionToPlayer = player.position - transform.position;

            // Calculate the rotation needed to face the player
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);

            // Apply the rotation directly to the zombie's visual object
            transform.rotation = lookRotation;
        }
    }
}
