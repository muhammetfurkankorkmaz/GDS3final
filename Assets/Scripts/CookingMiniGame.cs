using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingMiniGame : MonoBehaviour
{
    [Header("Essentials")]
    [SerializeField] GameObject slider;
    [SerializeField] GameObject stopPoint;
    [SerializeField] float edgePoint;

    [Header("Attributes")]
    [SerializeField] float originalSliderSpeed;
    [SerializeField] float successRange = 0.5f;
    [Header("CookieMan")]
    [SerializeField] GameObject cookieMan;
    [SerializeField] Transform cookieManSpawnPoint;
    public bool isGameActive { get; private set; } = false;

    bool isGameWon = false;

    float sliderSpeed;

    int direction = 1;

    int winCount = 0;
    void Start()
    {

    }

    void Update()
    {
        if (isGameActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CheckSlider();
            }
            MoveSlider();
        }
    }
    public void StartMiniGame()
    {
        if (isGameWon) return;
        sliderSpeed = originalSliderSpeed;
        isGameActive = true;
        winCount = 0;

        slider.transform.localPosition = new Vector3(-edgePoint, slider.transform.localPosition.y, 0f);

        // Random stop point position

        RandomizeStopPosition();
    }

    void MoveSlider()
    {
        Vector3 pos = slider.transform.localPosition;

        pos.x += sliderSpeed * direction * Time.deltaTime;

        // Bounce between edges
        if (pos.x >= edgePoint)
        {
            pos.x = edgePoint;
            direction = -1;
        }
        else if (pos.x <= -edgePoint)
        {
            pos.x = -edgePoint;
            direction = 1;
        }

        slider.transform.localPosition = pos;

    }
    void CheckSlider()
    {
        float distance = Mathf.Abs(
            slider.transform.localPosition.x -
            stopPoint.transform.localPosition.x
        );

        print(slider.transform.localPosition.x + " is the slider point");
        print(stopPoint.transform.localPosition.x + " is the stoppoint stop point");

        if (slider.transform.localPosition.x <= stopPoint.transform.localPosition.x + successRange && slider.transform.localPosition.x > stopPoint.transform.localPosition.x - successRange)
        {
            winCount++;
            if (winCount < 1)
            {
                RandomizeStopPosition();
            }
            else
            {
                WinMiniGame();
            }
        }
        else
        {
            LoseMiniGame();
        }
    }
    void RandomizeStopPosition()
    {
        print("Step randomized");
        float randomX = Random.Range(-edgePoint, edgePoint);

        stopPoint.transform.localPosition = new Vector3(randomX, stopPoint.transform.localPosition.y, 0f);
    }
    void WinMiniGame()
    {
        print("Mini game won");
        //It closes the object
        Instantiate(cookieMan, cookieManSpawnPoint.position, Quaternion.identity);

        isGameActive = false;
        isGameWon = true;
        gameObject.SetActive(false);

        //It spawns the cookie man
    }

    void LoseMiniGame()
    {
        //It closes the object
        print("mini game lost YOU LOST");
        winCount = 0;
        isGameActive = false;
        //It resets the objects
        gameObject.SetActive(false);

    }
    void OnDrawGizmos()
    {
        if (stopPoint == null)
            return;

        Gizmos.color = Color.red;

        Vector3 center = stopPoint.transform.position;

        // Left boundary
        Vector3 left = center + Vector3.left * successRange;

        // Right boundary
        Vector3 right = center + Vector3.right * successRange;

        // Draw success area line
        Gizmos.DrawLine(left, right);

        // Optional vertical markers
        Gizmos.DrawLine(left + Vector3.up * 0.5f, left + Vector3.down * 0.5f);
        Gizmos.DrawLine(right + Vector3.up * 0.5f, right + Vector3.down * 0.5f);
    }
}//Class
