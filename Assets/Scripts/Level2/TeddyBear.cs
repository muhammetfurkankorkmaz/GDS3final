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
    [SerializeField] GameObject shineParticle;

    [SerializeField] CatCD catCDSCript;

    [SerializeField] SpriteRenderer sr;

    [SerializeField] Chest chestScript;

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
            if (inventoryScript.CheckIfPlayerHasNeedle() && !isFixed)
            {
                //Fixes teddy bear 
                FixTeddyBear();
            }
            else
            {
                //Opens dialogue
                OpenFirstDialogue();
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
        chestScript.MakeChestOpenable();
    }

    void FixTeddyBear()
    {
        //Spawns cloud effect
        if (isFixed) return;
        sr.sprite = fixedSprite;
        if (smokeParticle != null)
            Instantiate(smokeParticle, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
        upShelf.SetActive(true);
        catCDSCript.MakeCatCDTakeble();
        interactVisual.SetActive(false);
        bearText.GetComponent<TextMeshProUGUI>().text = "THANKS! I THINK I SAW A CD UP THERE";
        bearText.SetActive(true);
        isFixed = true;
        shineParticle.SetActive(false);
    }
    void OpenThankYouDialogue()
    {
        interactVisual.SetActive(false);
        bearText.GetComponent<TextMeshProUGUI>().text = "THANKS! I THINK I SAW A CD UP THERE";
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
            bearText.SetActive(false);

        }
    }
}//Class
