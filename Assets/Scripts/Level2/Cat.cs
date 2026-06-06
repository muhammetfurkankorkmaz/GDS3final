using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    AnimationPlayer animPlayerScript;
    [Header("Jump Settings")]
    [SerializeField] float jumpHeight = 0.25f;
    [SerializeField] float jumpDuration = 0.5f;
    float walkSpeed = 2;
    Vector3 fallPosition;
    [SerializeField] Transform endPosition;
    void Start()
    {
        animPlayerScript = GetComponent<AnimationPlayer>();
    }

    void Update()
    {

    }
    public void ChangeToSitAnimation()
    {
        animPlayerScript.ChangeState("CatSit");
    }
    public void CatJump()
    {
        StartCoroutine(CatJumpAnimation());
    }
    IEnumerator CatJumpAnimation()
    {
        Vector3 startPos = transform.position;
        animPlayerScript.ChangeState("CatJump");

        // Landing position after jump
        Vector3 endPos = startPos + new Vector3(-1.75f, -2.75f, 0);

        float timer = 0f;
        transform.localEulerAngles = new Vector3(0, 180, 0);

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
        animPlayerScript.ChangeState("CatRun");

        Vector3 catEndPos = endPosition.transform.localPosition;

        // MOVE TOWARD CATAPULT
        while (Vector3.Distance(transform.position, catEndPos) > 0.01f)
        {
            Vector3 direction = (catEndPos - transform.position).normalized;

            transform.position += direction * walkSpeed * 1.5f * Time.deltaTime;

            // Optional: face catapult
            //transform.forward = direction;

            yield return null;
        }
        transform.eulerAngles = new Vector3(0, 180, 0);
        animPlayerScript.ChangeState("CatSit2");

    }
}//Class
