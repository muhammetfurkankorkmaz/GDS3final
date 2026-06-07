using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject creditsTAB;
    [SerializeField] GameObject howToPlayTAB;

  

    public void StartButton()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void CreditsButton()
    {
        howToPlayTAB.SetActive(false);
        creditsTAB.SetActive(true);
    }
    public void HowToPlayButton()
    {
        howToPlayTAB.SetActive(true);
        creditsTAB.SetActive(false);

    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
