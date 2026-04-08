using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public bool isOpening { get; private set; } = false;

    bool isPlayerHanginFromPlatform;
    void Start()
    {

    }

    void Update()
    {
        //This interaction should only happen if player is hanging to this platform
        //if (!isPlayerHanginFromPlatform) return;
        if (InputController.Instance.YInput < 0 && isPlayerHanginFromPlatform)
        {
            isOpening = true;
        }
        else if (isOpening)
        {
            isOpening = false;
        }
    }

    public void ChangeActivation(bool _condition)
    {
        isPlayerHanginFromPlatform = _condition;
    }

}//Class
