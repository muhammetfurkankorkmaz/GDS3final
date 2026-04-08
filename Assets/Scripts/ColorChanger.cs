using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    Platform platformScript;
    SpriteRenderer sr;

    float hue;
    void Start()
    {
        platformScript = GetComponent<Platform>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (platformScript.isOpening)
        {
            ColorChange();
        }
    }
    void ColorChange()
    {
        hue += Time.deltaTime; // speed of change
        if (hue > 1f) hue = 0f;

        sr.color = Color.HSVToRGB(hue, 1f, 1f);
    }
}//Class
