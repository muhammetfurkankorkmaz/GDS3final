using CameraShake;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PC : MonoBehaviour
{
    [SerializeField] Cat catScript;
    [SerializeField] GameObject video;
    [SerializeField] Image fillImage;
    [SerializeField] GameObject miniGameBarObject;
    [SerializeField] Color fullColor;
    [SerializeField] GameObject pcParticle;
    [SerializeField] GameObject interactButton;
    [Header("Variables")]
    [SerializeField] float pulseSpeed = 6f;
    [SerializeField] float pulseAmount = 0.15f;
    Color originalColor;
    Vector3 originalScale;
    float fillAmount;

    [SerializeField] SpriteRenderer interactButtonImage;
    [SerializeField] Material pressMaterial;

    Material buttonOriginalMaterial;

    float decreaseTimer;

    public bool isGameWon { get; private set; } = false;

    VideoPlayer videoPlayer;

    bool isGameOpen;
    void Start()
    {
        videoPlayer = video.GetComponent<VideoPlayer>();
        originalColor = fillImage.color;
        originalScale = interactButton.transform.localScale;

        buttonOriginalMaterial = interactButtonImage.material;
        InputController.Instance.onInteractButtonPress += IncreaseFill;

    }

    void Update()
    {
        if (!isGameOpen || isGameWon) return;

        float currentPulseSpeed = Mathf.Lerp(12f, 24f, fillAmount);

        float scale =
            1f + Mathf.Sin(Time.time * currentPulseSpeed) * pulseAmount;

        interactButton.transform.localScale = originalScale * scale;

        decreaseTimer += Time.deltaTime;
        if (decreaseTimer >= 0.05f)
        {
            decreaseTimer = 0;
            DecreaseFill();
        }
        CheckVideo();
    }
    void IncreaseFill()
    {
        if (!isGameOpen || isGameWon) return;
        fillAmount += 0.05f - fillAmount * 0.005f;
        fillAmount = Mathf.Clamp(fillAmount, 0, 1);
        fillImage.fillAmount = fillAmount / 1;
        CheckWinCondition();
        StartCoroutine(ButtonFlash());
        UpdateFillVisuals();

    }
    void DecreaseFill()
    {
        fillAmount -= 0.005f + fillAmount * 0.008f;
        fillAmount = Mathf.Clamp(fillAmount, 0, 1);
        fillImage.fillAmount = fillAmount / 1;
        UpdateFillVisuals();
    }
    void CheckWinCondition()
    {
        if (fillAmount >= 1)
        {
            if (pcParticle != null)
                Instantiate(pcParticle, transform.position, Quaternion.identity);
            isGameWon = true;
            videoPlayer.Stop();
            CameraShaker.Presets.ShortShake2D(0.08f, 0.1f, 30, 5);
            interactButton.SetActive(false);
            catScript.CatJump();
        }
    }
    void CheckVideo()
    {
        if (fillAmount <= 0)
        {
            videoPlayer.Stop();

        }
        else
        {
            if (!videoPlayer.isPlaying)
            {
                videoPlayer.Play();
            }
        }
    }
    public void OpenPC()
    {
        if (isGameOpen || isGameWon) return;
        miniGameBarObject.SetActive(true);
        interactButton.SetActive(true);
        isGameOpen = true;
        video.SetActive(true);
        videoPlayer.Play();
        StartCoroutine(VideoStop());
    }
    IEnumerator ButtonFlash()
    {

        interactButtonImage.material = pressMaterial;

        yield return new WaitForSeconds(0.05f);
        interactButtonImage.material = buttonOriginalMaterial;

    }
    IEnumerator VideoStop()
    {
        yield return new WaitForSeconds(0.03f);
        videoPlayer.Stop();
    }
    void UpdateFillVisuals()
    {
        fillImage.fillAmount = fillAmount;

        float t = Mathf.Clamp01(fillAmount);
        fillImage.color = Color.Lerp(originalColor, fullColor, t);
    }
}//Class
