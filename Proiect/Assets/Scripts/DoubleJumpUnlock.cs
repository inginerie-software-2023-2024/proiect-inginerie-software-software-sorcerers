using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpUnlock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("DoubleJump"))
        {
            PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();

            if(playerMovement != null) 
            {
                playerMovement.unlockedDoubleJump = true;
                Debug.Log("test");
            }
            Destroy(collision.gameObject);
        }
    }
}
