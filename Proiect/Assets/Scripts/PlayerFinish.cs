using UnityEngine;

public class PlayerFinish : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PlayerMovement mov;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        mov = GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Finish")
        {
            mov.enabled = false;
            anim.SetTrigger("finish");
            Debug.Log("idle");
        }
    }
}
