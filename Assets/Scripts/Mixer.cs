using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixer : MonoBehaviour
{
    [SerializeField] GameObject cookingMinigame;

    Interactible interactibleSc;
    Inventory inventorySc;

    void Start()
    {
        interactibleSc = GetComponent<Interactible>();
        if (interactibleSc != null)
            interactibleSc.onInteract += MixerInteract;

        inventorySc = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    void Update()
    {

    }
    void MixerInteract()
    {
        CookingMiniGame cMG = cookingMinigame.GetComponent<CookingMiniGame>();
        if (cMG.isGameActive) return;
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
}//Class
