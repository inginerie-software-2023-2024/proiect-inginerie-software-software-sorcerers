using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private static Animator animCheckpoint = null;
    private static Rigidbody2D rb;
    [SerializeField] private static Vector2 spawnPoint;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spawnPoint = rb.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            animCheckpoint = collision.GetComponent<Animator>();
            if (animCheckpoint.GetBool("isActive") == false)
            {
                spawnPoint = collision.transform.position;
                animCheckpoint.SetBool("isActive", true);
            }
        }
    }

    public static void UpdateCheckpoint(Vector2 posCheckpoint)
    {
        spawnPoint = posCheckpoint;
    }

    public static void MoveToSpawnpoint()
    {
        rb.transform.position = spawnPoint;
    }
}
