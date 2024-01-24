using System.Collections;
using UnityEngine;

public class FinishScript : MonoBehaviour
{
    private new AudioSource audio;
    private Animator anim;
    private bool finish = false;
    [SerializeField] private GameObject LevelCompletedCanvas = null;

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
            anim.SetTrigger("finish");
            finish = true;
            StartCoroutine(ActivateCanvasWithDelay());
        }
    }
    IEnumerator ActivateCanvasWithDelay()
    {
        yield return new WaitForSeconds(2f);

        LevelCompletedCanvas.SetActive(true);
    }
}
