using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOpenUp : MonoBehaviour
{
    [SerializeField] GameObject objectToOpen;
    [SerializeField] string openAnimationName;
    [SerializeField] AnimationPlayer animationPlayerScript;
    [SerializeField] GameObject particleObject;
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
        if (openAnimationName != "")
        {
            animationPlayerScript.ChangeState(openAnimationName);
        }
        timer += Time.deltaTime;
        if (timer >= 0.38f)
        {
            if (objectToOpen != null)
                objectToOpen.SetActive(true);
            if (particleObject != null)
                particleObject.SetActive(false);
            isOpen = true;
        }
    }
}//Class
