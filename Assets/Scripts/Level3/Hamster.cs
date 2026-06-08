using CameraShake;
using System.Collections;
using UnityEngine;

public class Hamster : MonoBehaviour
{
    [Header("Essentials")]
    [SerializeField] SpriteRenderer sr;

    [SerializeField] AnimationPlayer animPlayer;

    [SerializeField] Vector3 leftWall;
    [SerializeField] Vector3 rightWall;

    [SerializeField] CharacterHealth chHealthScript;

    public float maxHP;
    public float currentHP { get; private set; }


    bool isRestStarted = false;

    bool isRunAttacking = false;

    bool isJumpAttacking = false;

    bool isRecovering = false;

    bool isStarted = false;

    float recoverTimer = 0;

    bool canDamage = false;

    GameObject ch;
    CharacterController chScript;

    [SerializeField] AudioClip wallHitSound;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip fallSound;
    [SerializeField] GameObject wallParticle;
    void Start()
    {
        ch = GameObject.FindGameObjectWithTag("Player");
        chScript = ch.GetComponent<CharacterController>();
        currentHP = maxHP;
        StartCoroutine(StartAnimation());
    }

    void Update()
    {
        if (isStarted && !GameManager.Instance.isGameStopped)
        {
            if (isRecovering)
            {
                if (!isRestStarted)
                {
                    StartRest();
                }
                recoverTimer += Time.deltaTime;
                if (recoverTimer >= 2)
                {
                    isRecovering = false;
                    recoverTimer = 0;
                    isRestStarted = false;
                }
            }
            else
            {
                if (isJumpAttacking || isRunAttacking) return;

                int rng = Random.Range(0, 2);
                if (rng == 0)
                {
                    //Start run attack
                    RunAttack();
                }
                else if (rng == 1)
                {
                    //Start jump attack 
                    JumpAttack();
                }

            }
        }
    }

    public void TakeDamage()
    {
        currentHP -= 10;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Dead();
            isStarted = false;
        }
    }
    void Dead()
    {
        GameManager.Instance.WinGame();
        DeadAnimation();
    }
    void DeadAnimation()
    {
        SoundManager.Instance.PlaySoundEffect(winSound, 0.5f);
        StartCoroutine(PlayDeadAnimation());
    }
    void StartRest()
    {
        isRestStarted = true;
        animPlayer.ChangeState("HamsterIdle");
    }

    void RunAttack()
    {
        isRunAttacking = true;
        canDamage = true;
        print("Run Attacking");
        StartCoroutine(StartRunAttack());

    }
    void JumpAttack()
    {
        print("Jump Attacking");
        isJumpAttacking = true;
        StartCoroutine(StartJumpAttack());
    }
    IEnumerator PlayDeadAnimation()
    {
        Vector3 originalPos = transform.position;

        // Shake
        float shakeDuration = 0.2f;
        float shakeMagnitude = 0.1f;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.position = originalPos + new Vector3(offsetX, offsetY, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;

        // Fall down
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + new Vector3(0, -5, 0);

        elapsed = 0f;
        float duration = 1f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPos, endPos, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
    }
    IEnumerator StartJumpAttack()
    {
        //Gets little bit smaller 
        //Jumps to the players position 
        //Grows while doing it 
        //It checks if it hit the player
        Transform trans = transform;

        Vector3 startScale = new Vector3(1, 1, 1);
        Vector3 endScale = new Vector3(0.8f, 0.8f, 1);

        float elapsed = 0;
        float duration = 0.2f;

        elapsed = 0;
        while (elapsed < 0.6f)
        {
            float t = elapsed / duration;
            trans.localScale = Vector3.Lerp(startScale, endScale, t);

            elapsed += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.4f);

        Vector3 startPos = transform.position;
        Vector3 targetPos = new Vector3(ch.transform.position.x, trans.position.y, 0);

        float jumpDuration = 0.7f;
        float jumpHeight = 4f;

        float timer = 0f;

        float previousY = transform.position.y;

        if (startPos.x - targetPos.x > 0)
        {

            trans.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            trans.eulerAngles = new Vector3(0, 0, 0);
        }
        animPlayer.ChangeState("HamsterJump");
        bool isFallAnimStarted = false;


        while (timer < jumpDuration)
        {
            float t = timer / jumpDuration;

            // Move
            Vector3 pos = Vector3.Lerp(startPos, targetPos, t);

            // Arc
            pos.y += Mathf.Sin(t * Mathf.PI) * jumpHeight;

            transform.position = pos;

            if (pos.y < previousY && !isFallAnimStarted)
            {
                animPlayer.ChangeState("HamsterFall");
                isFallAnimStarted = true;
            }
            // Grow while airborne
            trans.localScale = Vector3.Lerp(endScale, startScale, t);

            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        trans.localScale = startScale;
        CameraShaker.Presets.ShortShake2D(0.2f, 0.15f, 30, 10);
        SoundManager.Instance.PlaySoundEffect(fallSound, 1);

        Instantiate(wallParticle, trans.position, Quaternion.identity);


        if (Vector2.Distance(transform.position, ch.transform.position) < 1.5f)
        {
            Debug.Log("Hamster hit player");
            chHealthScript.TakeDamage(10);
            // chScript.TakeDamage(...);
        }


        isJumpAttacking = false;
        isRecovering = true;
    }

    IEnumerator StartRunAttack()
    {
        Transform trans = transform;


        bool playerOnRight = ch.transform.position.x > transform.position.x;

        Vector3 backTarget;

        if (playerOnRight)
        {
            backTarget = transform.position + Vector3.left * 2;
        }
        else
        {
            backTarget = transform.position + Vector3.right * 2f;
        }

        if (playerOnRight)
        {
            trans.eulerAngles = Vector3.zero;
        }
        else
        {
            trans.eulerAngles = new Vector3(0, 180, 0);
        }

        animPlayer.ChangeState("HamsterWalk");

        while (Vector2.Distance(transform.position, backTarget) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, backTarget, 5 * Time.deltaTime);

            yield return null;
        }


        playerOnRight = ch.transform.position.x > transform.position.x;

        Vector3 targetWallPosition = Vector3.zero;

        if (playerOnRight)
        {
            trans.eulerAngles = Vector3.zero;
            targetWallPosition = rightWall;
        }
        else
        {
            trans.eulerAngles = new Vector3(0, 180, 0);
            targetWallPosition = leftWall;
        }

        animPlayer.ChangeState("HamsterRun");

        bool playerHit = false;


        while (Mathf.Abs(transform.position.x - targetWallPosition.x) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWallPosition, 12 * Time.deltaTime);

            if (!playerHit &&
                Vector2.Distance(transform.position, ch.transform.position) < 1.5f)
            {
                playerHit = true;

                Debug.Log("Hamster hit player");

                // chScript.TakeDamage(...);
            }

            yield return null;
        }

        CameraShaker.Presets.ShortShake2D(0.3f, 0.2f, 40, 15);
        SoundManager.Instance.PlaySoundEffect(wallHitSound, 1);
        Instantiate(wallParticle, targetWallPosition, Quaternion.identity);


        animPlayer.ChangeState("HamsterIdle");

        isRunAttacking = false;
        isRecovering = true;
    }

    IEnumerator StartAnimation()
    {
        Transform trans = sr.transform;
        Vector3 startRotation = new Vector3(0, 180, 0);
        Vector3 middleRotation = new Vector3(0, 180, 65);
        float elapsed = 0;
        float duration = 0.2f;

        elapsed = 0;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            trans.eulerAngles = Vector3.Lerp(startRotation, middleRotation, t);

            elapsed += Time.deltaTime;
            yield return null;
        }
        CameraShaker.Presets.ShortShake2D(0.2f, 0.2f, 30, 12);
        yield return new WaitForSeconds(0.5f);
        elapsed = 0;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            trans.eulerAngles = Vector3.Lerp(middleRotation, startRotation, t);

            elapsed += Time.deltaTime;
            yield return null;
        }
        CameraShaker.Presets.ShortShake2D(0.3f, 0.3f, 40, 16);

        trans.eulerAngles = startRotation;
        isStarted = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isRunAttacking && canDamage)
        {


            canDamage = false;
            collision.gameObject.GetComponent<CharacterHealth>().TakeDamage(5);

        }
    }
}//Class
