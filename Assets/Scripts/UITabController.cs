using UnityEngine;

public class UITabController : MonoBehaviour
{
    bool isUIOpen = false;

    [SerializeField] GameObject UITab;


    void Start()
    {
        InputController.Instance.onInventoryButtonPress += ControlUI;
    }
    void ControlUI()
    {
        if (UITab == null) return;
        if (!isUIOpen)//Opens UI
        {
            isUIOpen = true;
            OpenUI();
        }
        else if (isUIOpen) //Closes UI
        {
            isUIOpen = false;
            CloseUI();
        }
    }

    void OpenUI()
    {
        UITab.SetActive(true);
        GameManager.Instance.StopGame();
    }
    void CloseUI()
    {
        UITab.SetActive(false);
        GameManager.Instance.StartGame();
    }
}//Class
