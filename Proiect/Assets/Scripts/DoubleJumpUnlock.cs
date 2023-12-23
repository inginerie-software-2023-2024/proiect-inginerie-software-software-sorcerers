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
            }
            Destroy(collision.gameObject);
        }
    }
}
