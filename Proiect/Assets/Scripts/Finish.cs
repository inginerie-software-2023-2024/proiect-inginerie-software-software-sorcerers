using System.Collections;
using UnityEngine;

public class FinishScript : MonoBehaviour
{
    private new AudioSource audio;
    private Animator anim;
    private bool finish = false;
    [SerializeField] private GameObject LevelCompletedCanvas = null;
    private GameObject gm = null;

    private void Start()
    {
        audio = GetComponent<AudioSource>();    
        anim = GetComponent<Animator>();
        gm = GameObject.Find("GameManager");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && finish == false)
        {
            audio.Play();
            anim.SetTrigger("finish");
            finish = true;
            gm.SetActive(false);
            StartCoroutine(ActivateCanvasWithDelay());
        }
    }
    IEnumerator ActivateCanvasWithDelay()
    {
        yield return new WaitForSeconds(2f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        LevelCompletedCanvas.SetActive(true);
    }
}
