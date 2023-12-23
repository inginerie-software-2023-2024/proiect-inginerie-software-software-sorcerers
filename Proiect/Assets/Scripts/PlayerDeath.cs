using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private float deathTreshold = -7.0f;
    private Rigidbody2D rb;
    private Animator anim;
    private Animator animCheckpoint = null;

    [SerializeField]
    private Vector2 spawnPoint = new Vector2(-15, 1);

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb.transform.position = spawnPoint;
    }

    private void Update()
    {
        if (transform.position.y < deathTreshold
            && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(animCheckpoint != null)
        {
            animCheckpoint.SetBool("isActive", false);
        }
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            spawnPoint = collision.transform.position;
            animCheckpoint = collision.GetComponent<Animator>();
            animCheckpoint.SetBool("isActive", true);
        }
    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }

    private void RestartLevel()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.transform.position = spawnPoint;
    }

    public void UpdateCheckpoint(Vector2 posCheckpoint)
    {
        spawnPoint = posCheckpoint;
    }
}