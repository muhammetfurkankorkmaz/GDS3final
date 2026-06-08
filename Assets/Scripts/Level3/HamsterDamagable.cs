using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterDamagable : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;

    [SerializeField] Material damageMaterial;
    Material originalMaterial;
    [SerializeField] GameObject damagableIndicator;

    [SerializeField] Hamster hamsterScript;
    [SerializeField] AudioClip hurtSound;
    [SerializeField] GameObject hurtParticle;



    bool canTakedamage;

    float damageTimer;
    void Start()
    {
        originalMaterial = sr.material;
    }

    void Update()
    {
        if (!canTakedamage)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= 2)
            {
                damageTimer = 0;
                canTakedamage = true;
                OpenDamagable();
            }
        }
    }

    void OpenDamagable()
    {
        damagableIndicator.SetActive(true);

    }
    void TakeDamage()
    {
        SoundManager.Instance.PlaySoundEffect(hurtSound, 1);
        canTakedamage = false;
        damagableIndicator.SetActive(false);
        StartCoroutine(HitFlash());
        GameManager.Instance.HitStop();
        hamsterScript.TakeDamage();
    }

    IEnumerator HitFlash()
    {
        sr.material = damageMaterial;
        yield return new WaitForSecondsRealtime(0.25f);
        sr.material = originalMaterial;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canTakedamage)
        {
            if (collision.GetComponent<CharacterController>().isOnAir)
            {
                Vector2 hitPoint = collision.ClosestPoint(transform.position);
                Instantiate(hurtParticle, hitPoint, Quaternion.identity);

                TakeDamage();

            }
        }
    }
}
