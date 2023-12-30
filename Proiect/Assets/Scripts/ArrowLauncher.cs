using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
        StartCoroutine(SpawnArrow());
    }

    private void Update()
    {

    }
    private IEnumerator SpawnArrow()
    {
        while (true)
        {
            if (toLeft)
            {
                var arrow = Instantiate(_arrow);
                arrow.transform.position = transform.position;
                arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(-arrowSpeed, 0f);
                Destroy(arrow, arrowLifeSpan);
            }
            if (toRight)
            {
                var arrow = Instantiate(_arrow);
                arrow.transform.position = transform.position;
                arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(arrowSpeed, 0f);
                Destroy(arrow, arrowLifeSpan);
            }
            if (toTop)
            {
                var arrow = Instantiate(_arrow);
                arrow.transform.position = transform.position;
                arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(0, arrowSpeed);
                Destroy(arrow, arrowLifeSpan);
            }
            if (toBottom)
            {
                var arrow = Instantiate(_arrow);
                arrow.transform.position = transform.position;
                arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -arrowSpeed);
                Destroy(arrow, arrowLifeSpan);
            }
            yield return new WaitForSeconds(launchSpeed);
        }
    }


}
