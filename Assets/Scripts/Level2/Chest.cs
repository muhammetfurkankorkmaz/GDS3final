using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Essentials")]
    [SerializeField] GameObject interactVisual;
    [SerializeField] GameObject needleObject;

    [SerializeField] GameObject shineParticle;

    [SerializeField] AnimationPlayer animPlayerScript;
    [SerializeField] AudioClip openSound;

    bool isInInteractRange = false;

    bool isTakeble = false;
    bool isItemtaken = false;
    void Start()
    {
        InputController.Instance.onInteractButtonPress += CheckForInteraction;
    }

    void CheckForInteraction()
    {
        if (!isTakeble || !isInInteractRange || isItemtaken) return;
        isItemtaken = true;
        animPlayerScript.ChangeState("BoxOpen");
        interactVisual.SetActive(false);
        StartCoroutine(SetNeedleActive());
        shineParticle.SetActive(false);
        SoundManager.Instance.PlaySoundEffect(openSound, 0.5f);


    }

    IEnumerator SetNeedleActive()
    {
        yield return new WaitForSeconds(0.4f);
        needleObject.SetActive(true);

    }
    public void MakeChestOpenable()
    {
        isTakeble = true;
        shineParticle.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isTakeble && !isItemtaken)
        {
            interactVisual.SetActive(true);
            isInInteractRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isTakeble && !isItemtaken)
        {
            interactVisual.SetActive(false);
            isInInteractRange = false;
        }
    }
}//Class
