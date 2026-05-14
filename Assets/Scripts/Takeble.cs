using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Takeble : MonoBehaviour
{
    [Header("Varibles")]

    [SerializeField] string interactibleName;
    [SerializeField] Sprite interactibleSprite;

    bool isItemTaken = false;

    Interactible interactibleSc;
    Inventory inventorySc;

    SpriteRenderer sr;

    Collider2D col;

    void Start()
    {
        interactibleSc = GetComponent<Interactible>();
        sr = GetComponentInChildren<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        inventorySc = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

        if (interactibleSc != null)
            interactibleSc.onInteract += TakeTakeble;

    }

    void Update()
    {

    }

    void TakeTakeble()
    {
        if (interactibleSc == null || inventorySc == null) return;

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

    public void OpenInteraction()
    {
        isItemTaken = false;
        sr.enabled = true;
    }

}//Class
