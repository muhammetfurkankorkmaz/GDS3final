using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level4 : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] SpriteRenderer sr;
    void Start()
    {
        StartCoroutine(StartCoroutine());
    }

    IEnumerator StartCoroutine()
    {
        yield return new WaitForSeconds(2);
        sr.sprite = sprites[1];
        yield return new WaitForSeconds(2);
        sr.sprite = sprites[2];
        yield return new WaitForSeconds(2);
        sr.sprite = sprites[3];
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");
    }
}
