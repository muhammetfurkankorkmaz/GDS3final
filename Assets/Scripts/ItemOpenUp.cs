using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOpenUp : MonoBehaviour
{
    [SerializeField] GameObject objectToOpen;
    Platform platformScript;
    SpriteRenderer sr;

    float timer;

    float hue;

    bool isOpen = false;
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
            OpenItem();
        }
    }
    void OpenItem()
    {
        if (isOpen) return;
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            if (objectToOpen != null)
                objectToOpen.SetActive(true);
            isOpen = true;
        }
    }
}//Class
