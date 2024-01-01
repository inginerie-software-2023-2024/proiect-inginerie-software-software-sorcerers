using System.Collections;
using UnityEngine;

public class ArrowLauncher : MonoBehaviour
{
    [SerializeField] private GameObject _arrow = null;
    [SerializeField] private float launchSpeed = 1.5f;
    [SerializeField] private float arrowSpeed = 10.0f;
    [SerializeField] private float arrowLifeSpan = 1.0f;
    [SerializeField] private bool toLeft = false;
    [SerializeField] private bool toRight = false;
    [SerializeField] private bool toTop = false;
    [SerializeField] private bool toBottom = false;

    private void Awake()
    {
        StartCoroutine(SpawnArrows());
    }

    private IEnumerator SpawnArrows()
    {
        while (true)
        {
            if (toLeft) SpawnArrow(Vector2.left * arrowSpeed);
            if (toRight) SpawnArrow(Vector2.right * arrowSpeed);
            if (toTop) SpawnArrow(Vector2.up * arrowSpeed);
            if (toBottom) SpawnArrow(Vector2.down * arrowSpeed);

            yield return new WaitForSeconds(launchSpeed);
        }
    }

    private void SpawnArrow(Vector2 direction)
    {
        var arrow = Instantiate(_arrow);
        arrow.transform.position = transform.position;
        arrow.GetComponent<Rigidbody2D>().velocity = direction;
        StartCoroutine(DestroyArrowAfterDelay(arrow, arrowLifeSpan));
    }

    private IEnumerator DestroyArrowAfterDelay(GameObject arrow, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (arrow != null)
        {
            Destroy(arrow);
        }
    }
}