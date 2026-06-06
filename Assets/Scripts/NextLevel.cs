using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [Header("Essentials")]
    [SerializeField] GameObject interactVisual;

    [SerializeField] string nextLevelName;

    bool isInInteractRange = false;

    public bool canPlayerInteract = true;


    void Start()
    {
        InputController.Instance.onInteractButtonPress += CheckForInteraction;
    }


    void CheckForInteraction()
    {
        if (isInInteractRange && canPlayerInteract)
        {
            SceneManager.LoadScene(nextLevelName);
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
}
