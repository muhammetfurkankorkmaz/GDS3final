using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Start()
    {

    }

    void Update()
    {

    }
    public void StopGame()
    {
        Time.timeScale = 0;
        isGameStopped = true;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        isGameStopped = false;

    }
}//Class
