using UnityEngine;

public class Dash : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dash"))
        {
            PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();

            if (playerMovement != null)
            {
                playerMovement.unlockedDash = true;
            }
            Destroy(collision.gameObject);
        }
    }
}
