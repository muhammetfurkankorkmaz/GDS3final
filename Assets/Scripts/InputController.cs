using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private static InputController _instance;

    public static InputController Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Input Controller is empty!!!");

            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }

    private int xInput;
    private int yInput;

    public int XInput { get; private set; }
    public int YInput { get; private set; }

    public event Action onJumpButtonPress;
    public event Action onInventoryButtonPress;
    public event Action onInteractButtonPress;

    void Update()
    {
        KeyboardInputManager();
    }
    void KeyboardInputManager()
    {
        if (GameManager.Instance.isGameStopped) return;
        yInput = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0;
        xInput = Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0;
        XInput = xInput;
        YInput = yInput;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            onJumpButtonPress?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            onInteractButtonPress?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            onInventoryButtonPress?.Invoke();
        }
    }
}//Class
