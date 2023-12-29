using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private float deathTreshold = -7.0f;
    private Rigidbody2D rb;
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }

    // RestartLevel may appear as unused, but it is
    // called at the end of the death animation, so
    // do NOT delete it.
    private void RestartLevel()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        Checkpoint.MoveToSpawnpoint();
    }


}