using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public float maxHP;
    public float currentHP { get; private set; }

    [SerializeField] SpriteRenderer sr;
    [SerializeField] Material flashMaterial;
    Material originalMaterial;
    [SerializeField] AudioClip hurtSound;
    [SerializeField] GameObject hurtParticle;
    void Start()
    {
        originalMaterial = sr.material;
        currentHP = maxHP;
    }

    void Update()
    {

    }
    public void TakeDamage(float _dmageAmount)
    {
        currentHP -= _dmageAmount;
        GameManager.Instance.HitStop();
        SoundManager.Instance.PlaySoundEffect(hurtSound, 1);
        Instantiate(hurtParticle, transform.position, Quaternion.identity);
        StartCoroutine(HitFlash());

        if (currentHP <= 0)
        {
            Dead();
        }
    }
    IEnumerator HitFlash()
    {
        sr.material = flashMaterial;
        yield return new WaitForSecondsRealtime(0.25f);
        sr.material = originalMaterial;
    }
    void Dead()
    {

    }
}
