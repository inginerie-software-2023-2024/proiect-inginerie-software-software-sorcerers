using UnityEngine;

public class WallJump : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("WallJump"))
        {
            PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();

            if (playerMovement != null)
            {
                playerMovement.unlockedWallJump = true;
            }
            Destroy(collision.gameObject);
        }
    }
}
