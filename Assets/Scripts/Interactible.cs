using System;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    [SerializeField] GameObject interactVisual;

    bool isInInteractRange = false;

    public event Action onInteract;

    void Start()
    {
        InputController.Instance.onInteractButtonPress += CheckForInteraction;
    }
    void CheckForInteraction()
    {
        if (isInInteractRange)
        {
            onInteract?.Invoke();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactVisual.SetActive(true);
            isInInteractRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactVisual.SetActive(false);
            isInInteractRange = false;
        }
    }
}//Class
