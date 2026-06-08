using System.Collections;
using UnityEngine;

public class DamageIndicatorAnim : MonoBehaviour
{
    [SerializeField] float spinSpeed = 360f;
    [SerializeField] float scaleAmount = 0.2f;
    [SerializeField] float scaleSpeed = 6f;

    Vector3 startScale;

    void Start()
    {
    }
   void OnEnable()
    {
        startScale = transform.localScale;
        StopAllCoroutines();
        StartCoroutine(Animate());

    }
    IEnumerator Animate()
    {
        float t = 0f;

        while (true)
        {
            // SPIN
            transform.Rotate(0f, 0f, spinSpeed * Time.deltaTime);

            // PULSE SCALE (grow/shrink)
            t += Time.deltaTime * scaleSpeed;

            float scaleOffset = 1f + Mathf.Sin(t) * scaleAmount;

            transform.localScale = startScale * scaleOffset;

            yield return null;
        }
    }
}