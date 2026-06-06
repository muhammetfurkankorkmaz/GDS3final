using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCCase : MonoBehaviour
{
    [Header("Essentials")]
    [SerializeField] GameObject interactVisual;
    [SerializeField] Cat catScript;

    bool isInInteractRange = false;
    Inventory inventoryScript;

    void Start()
    {
        inventoryScript = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        InputController.Instance.onInteractButtonPress += CheckForInteraction;


    }

    // Update is called once per frame
    void Update()
    {

    }

    void CheckForInteraction()
    {
        if (inventoryScript.CheckIfPlayerHasCatCD() && isInInteractRange)
        {
            //Starts the game
            print("PC game started");
            catScript.ChangeToSitAnimation();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && inventoryScript.CheckIfPlayerHasCatCD())
        {
            interactVisual.SetActive(true);
            isInInteractRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && inventoryScript.CheckIfPlayerHasCatCD())
        {
            interactVisual.SetActive(false);
            isInInteractRange = false;
        }
    }
}//Class
