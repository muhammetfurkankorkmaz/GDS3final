using System;
using UnityEngine;
using UnityEngine.UI;

public class Interactible : MonoBehaviour
{

    [Header("Essentials")]
    [SerializeField] GameObject interactVisual;

    bool isInInteractRange = false;

    public event Action onInteract;

    public bool canPlayerInteract = true;

    Collider2D col;
    SpriteRenderer sr;


    void Start()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponentInChildren<SpriteRenderer>();


        InputController.Instance.onInteractButtonPress += CheckForInteraction;
    }
    void CheckForInteraction()
    {
        if (isInInteractRange && canPlayerInteract)
        {
            onInteract?.Invoke();
        }
    }

    public void MakeObjectInteractible()
    {
        canPlayerInteract = true;
        sr.enabled = true;
        col.enabled = true;
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
