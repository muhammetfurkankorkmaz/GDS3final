using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    GameObject ch;
    [SerializeField] float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        ch = GameObject.FindGameObjectWithTag("Player");
    }

    void LateUpdate()
    {
        if (ch == null) return;

        Vector3 targetPosition = ch.transform.position;
        float clampedY = targetPosition.y;
        clampedY = Mathf.Clamp(clampedY, -2f, 100f);

        targetPosition = new Vector3(targetPosition.x, clampedY, -10);

        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        //transform.position = smoothedPosition;
    }

}//Class
