using UnityEngine;

public class FinishScript : MonoBehaviour
{
    private new AudioSource audio;
    private Animator anim;
    private bool finish = false;

    private void Start()
    {
        audio = GetComponent<AudioSource>();    
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && finish == false)
        {
            audio.Play();
            Debug.Log("Level Finished");
            anim.SetTrigger("finish");
            finish = true;
        }
    }
}
