using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    AnimationPlayer animPlayerScript;
    void Start()
    {
        animPlayerScript = GetComponent<AnimationPlayer>();
    }

    void Update()
    {
        
    }
    public void ChangeToSitAnimation()
    {
        animPlayerScript.ChangeState("CatSit");
    }
}//Class
