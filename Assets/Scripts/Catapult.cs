using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    [Header("Essentials")]
    [SerializeField] GameObject interactVisual;

    [SerializeField] Vector2 launchForce = new Vector2(10f, 15f);

    bool isInInteractRange = false;

    bool isCookieManCreated = false;

    CookieMan cookieManCH;

    Coroutine throwPlayerCoroutine;

    GameObject playerObject;
    Rigidbody2D playerRb;

    bool isZoomHappened = false;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerRb = playerObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInInteractRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                print("hoho");
                throwPlayerCoroutine = StartCoroutine(LaunchPlayer());
                //if (!isZoomHappened)
                //{

                    StartCoroutine(CameraZoomOut());
    
            }
        }
    }

    IEnumerator CameraZoomOut()
    {
        isZoomHappened = true;
        yield return new WaitForSeconds(0.25f);
        float zoomStart = Camera.main.orthographicSize;

        float zoomEnd = 4.75f;

        float elapsed = 0;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            float size = Mathf.Lerp(zoomStart, zoomEnd, t);
            Camera.main.orthographicSize = size;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator LaunchPlayer()
    {
        // Reset old velocity first
        //playerRb.linearVelocity = Vector2.zero;
        cookieManCH.PlayCookieManCatapultAnimation();

        yield return new WaitForSeconds(0.5f);
        // Add launch force
        playerRb.AddForce(launchForce, ForceMode2D.Impulse);
        playerObject.GetComponent<CharacterController>().StartJumpAnimation();
    }
    public void UpdateCookieManInfo(CookieMan _cookimanCH)
    {
        cookieManCH = _cookimanCH;
        isCookieManCreated = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isCookieManCreated)
        {
            interactVisual.SetActive(true);
            isInInteractRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isCookieManCreated)
        {
            interactVisual.SetActive(false);
            isInInteractRange = false;
        }
    }
}//Class
