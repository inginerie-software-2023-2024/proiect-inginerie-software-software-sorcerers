using System.Collections;
using UnityEngine;

public class ArrowLauncher : MonoBehaviour
{

    [SerializeField] private GameObject _arrow = null;
    [SerializeField] private float launchSpeed = 1.0f;
    [SerializeField] private float arrowSpeed = 10.0f;
    [SerializeField] private float arrowLifeSpan = 1.0f;
    [SerializeField] private bool toLeft = false;
    [SerializeField] private bool toRight = false;
    [SerializeField] private bool toTop = false;
    [SerializeField] private bool toBottom = false;

    private void Awake()
    {
        StartCoroutine(SpawnArrowLeft());
        StartCoroutine(SpawnArrowRight());
        StartCoroutine(SpawnArrowUp());
        StartCoroutine(SpawnArrowDown());
    }

    private IEnumerator SpawnArrowLeft()
    {
        while (true)
        {
            if (toLeft)
            {
                var arrow = Instantiate(_arrow);
                arrow.transform.position = transform.position;
                arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(-arrowSpeed, 0f);
                yield return new WaitForSeconds(arrowLifeSpan);
                if (arrow)
                {
                    Destroy(arrow);
                }
            }
            yield return new WaitForSeconds(launchSpeed);
        }
    }

    private IEnumerator SpawnArrowRight()
    {
        while (true)
        {
            if (toRight)
            {
                var arrow = Instantiate(_arrow);
                arrow.transform.position = transform.position;
                arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(arrowSpeed, 0f);
                yield return new WaitForSeconds(arrowLifeSpan);
                if (arrow)
                    Destroy(arrow);
            }
            yield return new WaitForSeconds(launchSpeed);
        }
    }

    private IEnumerator SpawnArrowUp()
    {
        while (true)
        {
            if (toTop)
            {
                var arrow = Instantiate(_arrow);
                arrow.transform.position = transform.position;
                arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(0, arrowSpeed);
                yield return new WaitForSeconds(arrowLifeSpan);
                if (arrow)
                    Destroy(arrow);
            }
            yield return new WaitForSeconds(launchSpeed);
        }
    }

    private IEnumerator SpawnArrowDown()
    {
        while (true)
        {
            if (toBottom)
            {
                var arrow = Instantiate(_arrow);
                arrow.transform.position = transform.position;
                arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -arrowSpeed);
                yield return new WaitForSeconds(arrowLifeSpan);
                if (arrow)
                    Destroy(arrow);
            }
            yield return new WaitForSeconds(launchSpeed);
        }
    }


}
