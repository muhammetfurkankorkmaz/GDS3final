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
    [SerializeField] AudioClip openSound;

    float timer;

    float hue;

    bool isOpen = false;
    bool isSoundPlayed = false;
    void Start()
    {
        platformScript = GetComponent<Platform>();
        sr = GetComponent<SpriteRenderer>();
    }

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
        if (!isSoundPlayed)
        {
            isSoundPlayed = true;
            SoundManager.Instance.PlaySoundEffect(openSound, 0.1f);
        }
        if (openAnimationName != "")
        {
            animationPlayerScript.ChangeState(openAnimationName);
        }

        timer += Time.deltaTime;
        if (timer >= 0.3f)
        {
            if (objectToOpen != null)
                objectToOpen.SetActive(true);
            if (particleObject != null)
                particleObject.SetActive(false);
            isOpen = true;
        }
    }
}//Class
