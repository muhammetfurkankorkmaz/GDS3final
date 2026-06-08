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
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            onInventoryButtonPress?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            onInteractButtonPress?.Invoke();
        }
        if (GameManager.Instance.isGameStopped) return;
        yInput = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0;
        xInput = Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0;

        float joyXInput = Input.GetAxisRaw("Horizontal");
        float joyYInput = Input.GetAxisRaw("Vertical");

        if(Mathf.RoundToInt(joyXInput)!=0)
        {
            XInput = Mathf.RoundToInt(joyXInput);
        }
        else
        {
            XInput = xInput;
        }
        if (Mathf.RoundToInt(joyYInput) != 0)
        {
            YInput = Mathf.RoundToInt(joyYInput);
        }
        else
        {
            YInput = yInput;
        }


        //print(joyXInput);
        //YInput = yInput + Mathf.RoundToInt(joyYInput);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            onJumpButtonPress?.Invoke();
        }

    }
}//Class
