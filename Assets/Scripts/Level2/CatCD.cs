using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCD : MonoBehaviour
{
    [Header("Essentials")]
    [SerializeField] GameObject interactVisual;
    [Header("Varibles")]

    [SerializeField] string interactibleName;
    [SerializeField] Sprite interactibleSprite;
    bool isItemTaken = false;

    Inventory inventorySc;

    [SerializeField] SpriteRenderer sr;

  [SerializeField]  Collider2D col;

    bool isInInteractRange = false;

    bool isTakeble = false;
    private void Start()
    {
        inventorySc = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        InputController.Instance.onInteractButtonPress += CheckForInteraction;

    }

    void CheckForInteraction()
    {
        if (isTakeble)
            TakeTakeble();
    }

    public void MakeCatCDTakeble()
    {
        isTakeble = true;
    }
    void TakeTakeble()
    {
        if (inventorySc == null) return;

        if (isItemTaken || !inventorySc.CanTakeItem()) return;

        inventorySc.AddItem(interactibleName, interactibleSprite);

        RemoveInteraction();

    }
    void RemoveInteraction()
    {
        isItemTaken = true;
        sr.enabled = false;
        col.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isTakeble)
        {
            interactVisual.SetActive(true);
            isInInteractRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isTakeble)
        {
            interactVisual.SetActive(false);
            isInInteractRange = false;
        }
    }
}//Class
