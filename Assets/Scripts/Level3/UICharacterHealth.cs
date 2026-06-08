using UnityEngine;
using UnityEngine.UI;

public class UICharacterHealth : MonoBehaviour
{
    [SerializeField] CharacterHealth chHealthScript;
    [SerializeField] Image hpBar;
    void Update()
    {
        hpBar.fillAmount = chHealthScript.currentHP / chHealthScript.maxHP;
    }
}//Class
