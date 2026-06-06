using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TeddyBear : MonoBehaviour
{
    [SerializeField] GameObject bearText;
    [SerializeField] GameObject upShelf;
    [SerializeField] Sprite fixedSprite;
    [SerializeField] GameObject smokeParticle;

    [SerializeField] CatCD catCDSCript;

    [SerializeField] SpriteRenderer sr;

    [Header("Essentials")]
    [SerializeField] GameObject interactVisual;

    bool isInInteractRange = false;

    Inventory inventoryScript;


    bool isFixed = false;
    void Start()
    {
        inventoryScript = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        InputController.Instance.onInteractButtonPress += CheckForInteraction;
    }

    void Update()
    {

    }
    void CheckForInteraction()
    {
        if (!isInInteractRange) return;
        if (!isFixed)
        {
            //If not fixed it gives dialogue 
            //checks if it is async fix interaction
            if (inventoryScript.CheckIfPlayerHasNeedle())
            {
                //Fixes teddy bear 
                OpenFirstDialogue();
            }
            else
            {
                //Opens dialogue
                FixTeddyBear();
            }
        }
        else
        {
            //Opens another dialogue
            OpenThankYouDialogue();
        }
    }
    void OpenFirstDialogue()
    {
        interactVisual.SetActive(false);
        bearText.SetActive(true);
    }

    void FixTeddyBear()
    {
        //Spawns cloud effect
        sr.sprite = fixedSprite;
        if (smokeParticle != null)
            Instantiate(smokeParticle, transform.position, Quaternion.identity);
        upShelf.SetActive(true);
        catCDSCript.MakeCatCDTakeble();
        interactVisual.SetActive(false);
        bearText.GetComponent<TextMeshProUGUI>().text = "THANKS! NOW YOU CAN JUMP TO THE TOP";
        bearText.SetActive(true);
    }
    void OpenThankYouDialogue()
    {
        interactVisual.SetActive(false);
        bearText.GetComponent<TextMeshProUGUI>().text = "THANKS! NOW YOU CAN JUMP TO THE TOP";
        bearText.SetActive(true);
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
