using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Game Manager is empty!!!");

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public bool isGameStopped { get; private set; }

    bool isGameLost = false;
    [SerializeField] GameObject loseScreen;
    [SerializeField] GameObject winObjects;
    void Start()
    {
        InputController.Instance.onInteractButtonPress += ReloadScene;

    }

    void Update()
    {

    }
    void ReloadScene()
    {
        if (!isGameLost) return;

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void StopGame()
    {
        Time.timeScale = 0;
        isGameStopped = true;
    }

    public void HitStop()
    {
        StartCoroutine(HitStopCoroutine());
    }
    IEnumerator HitStopCoroutine()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.25f);
        Time.timeScale = 1;
    }
    public void StartGame()
    {
        Time.timeScale = 1;
        isGameStopped = false;

    }
    public void LoseGame()
    {
        isGameLost = true;
        isGameStopped = true;
        if (loseScreen != null)
            loseScreen.SetActive(true);
    }
    public void WinGame()
    {
        winObjects.SetActive(true);
    }
}//Class
