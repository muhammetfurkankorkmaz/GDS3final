using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixer : MonoBehaviour
{
    [Header("Essentials")]
    [SerializeField] GameObject interactVisual;

    [SerializeField] GameObject cookiesParent;

    bool isInInteractRange = false;
    [SerializeField] GameObject cookingMinigame;

    //Interactible interactibleSc;
    Inventory inventorySc;

    void Start()
    {
        //interactibleSc = GetComponent<Interactible>();
        //if (interactibleSc != null)
        //    interactibleSc.onInteract += MixerInteract;

        inventorySc = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

        InputController.Instance.onInteractButtonPress += MixerInteract;

    }

    void Update()
    {

    }
    void MixerInteract()
    {
        if (!isInInteractRange) return;
        CookingMiniGame cMG = cookingMinigame.GetComponent<CookingMiniGame>();
        if (cMG.isGameActive || cMG.isGameWon) return;
        print("Game started");
        interactVisual.SetActive(false);
        cookiesParent.SetActive(true);

        //if (inventorySc.CheckItemAmount())
        //{
        cookingMinigame.SetActive(true);
        cMG.StartMiniGame();
        //}
        //If not working checks for all items 
        //If all items are ready it starts the mixer 
        //If it started it stops the mixer 
        //If it is the right amount it will spawn a cookie man
        //If it is the wrong amount it will burn and reset the objects
    }
    public void OpenInteractImage()
    {
        if (cookingMinigame.GetComponent<CookingMiniGame>().isGameWon || !isInInteractRange) return;
        interactVisual.SetActive(true);
        cookiesParent.SetActive(false);


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInInteractRange = true;
            if (cookingMinigame.GetComponent<CookingMiniGame>().isGameWon || cookingMinigame.GetComponent<CookingMiniGame>().isGameActive) return;
            interactVisual.SetActive(true);
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
