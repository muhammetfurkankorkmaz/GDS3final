using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HamsterHealth : MonoBehaviour
{
    [SerializeField] Image hpBar;

    [SerializeField] Hamster hamsterScript;
    void Update()
    {
        hpBar.fillAmount = hamsterScript.currentHP / hamsterScript.maxHP;
    }
}//Class
