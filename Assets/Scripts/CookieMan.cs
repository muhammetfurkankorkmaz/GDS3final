using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieMan : MonoBehaviour
{
    Catapult catapultObject;

    [Header("Jump Settings")]
    public float jumpHeight = 2f;
    public float jumpDuration = 1f;

    [Header("Walk Settings")]
    public float walkSpeed = 2f;
    public float stopDistance = 0.1f;
    [SerializeField] Vector3 extraDistance = new Vector3(2f, 0, 0);

    void Start()
    {
        catapultObject = GameObject.FindGameObjectWithTag("Catapult").GetComponent<Catapult>();
        StartCoroutine(CookieManStartAnimation());

    }

    void Update()
    {

    }

    public void PlayCookieManCatapultAnimation()
    {
        StartCoroutine(CookieManCatapultAnimation());
    }

    IEnumerator CookieManCatapultAnimation()
    {
        Vector3 startPos = transform.position;

        Vector3 jumpTarget =
            catapultObject.transform.position + new Vector3(0.5f, 0.6f, 0);

        float timer = 0f;
        float hopDuration = 0.5f;

        while (timer < hopDuration)
        {
            float t = timer / hopDuration;

            Vector3 currentPos = Vector3.Lerp(startPos, jumpTarget, t);

            currentPos.y += Mathf.Sin(t * Mathf.PI) * jumpHeight;

            transform.position = currentPos;

            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = jumpTarget;

        yield return new WaitForSeconds(0.1f);

        // RETURN TO WAIT POSITION
        timer = 0f;

        while (timer < jumpDuration)
        {
            float t = timer / jumpDuration;

            Vector3 currentPos = Vector3.Lerp(jumpTarget, startPos, t);

            //currentPos.y += Mathf.Sin(t * Mathf.PI) * (jumpHeight * 0.6f);

            transform.position = currentPos;

            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = startPos;

    }

    IEnumerator CookieManStartAnimation()
    {
        //Jumps to right 
        //Goes near the catapult and waits
        Vector3 startPos = transform.position;

        // Landing position after jump
        Vector3 endPos = new Vector3(1.9f, 1.75f, 0);

        float timer = 0f;

        // JUMP
        while (timer < jumpDuration)
        {
            float t = timer / jumpDuration;

            // Smooth horizontal movement
            Vector3 currentPos = Vector3.Lerp(startPos, endPos, t);

            // Arc movement
            currentPos.y += Mathf.Sin(t * Mathf.PI) * jumpHeight;

            transform.position = currentPos;

            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure exact landing
        transform.position = endPos;

        // Small pause after landing
        yield return new WaitForSeconds(0.2f);

        Vector3 catapultEndPos = new Vector3(catapultObject.transform.position.x + extraDistance.x, endPos.y, 0);

        // MOVE TOWARD CATAPULT
        while (Vector3.Distance(transform.position, catapultEndPos) > stopDistance)
        {
            Vector3 direction = (catapultEndPos - transform.position).normalized;

            transform.position += direction * walkSpeed * Time.deltaTime;

            // Optional: face catapult
            //transform.forward = direction;

            yield return null;
        }
        catapultObject.UpdateCookieManInfo(this);
        Debug.Log("CookieMan reached the catapult.");
    }

}//Class
