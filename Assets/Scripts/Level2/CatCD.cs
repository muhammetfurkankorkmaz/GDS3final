using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCD : MonoBehaviour
{
    [Header("Essentials")]
    [SerializeField] GameObject interactVisual;

    bool isInInteractRange = false;

    bool isTakeble = false;
    void Start()
    {

    }

    void Update()
    {

    }
    public void MakeCatCDTakeble()
    {
        isTakeble = true;
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
