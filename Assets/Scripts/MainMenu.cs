using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject creditsTAB;
    [SerializeField] GameObject howToPlayTAB;

    [SerializeField] AnimationPlayer startAnim;
    [SerializeField] AnimationPlayer howToPlayAnim;
    [SerializeField] AnimationPlayer creditsAnim;
    [SerializeField] AnimationPlayer quitAnim;

    [SerializeField] AudioClip paperAudio;

    bool isCreditsPressedBefore = false;
    bool isHowToPlayPressedBefore = false;

    [SerializeField] GameObject cookieMan;

    [SerializeField] Transform[] buttonPos;

    int currentButtonNumber = 0;

    bool hasTakenJoyInput = false;
    float joyTimer;

    private void Start()
    {
        InputController.Instance.onInteractButtonPress += ApplyButton;
    }
    private void Update()
    {
        if (hasTakenJoyInput)
        {
            joyTimer += Time.deltaTime;
            if (joyTimer >= 0.5f)
            {
                hasTakenJoyInput = false;
                joyTimer = 0;
            }
        }
        JoyStickInputController();
    }
    void JoyStickInputController()
    {
        if (hasTakenJoyInput) return;
        float joyYInput = Input.GetAxisRaw("Vertical");
        if (joyYInput == 1)
        {
           
            currentButtonNumber--;
            if (currentButtonNumber == -1)
            {
                currentButtonNumber = 3;
            }
            cookieMan.transform.position = buttonPos[currentButtonNumber].transform.position;
            hasTakenJoyInput = true;
        }
        else if (joyYInput == -1)
        {
            currentButtonNumber++;
            if (currentButtonNumber == 4)
            {
                currentButtonNumber = 0;
            }
            cookieMan.transform.position = buttonPos[currentButtonNumber].transform.position;
            hasTakenJoyInput = true;
        }
    }
    void ApplyButton()
    {
        if (currentButtonNumber == 0)
        {
            StartButton();
        }
        else if (currentButtonNumber == 1)
        {
            HowToPlayButton();
        }
        else if (currentButtonNumber == 2)
        {
            CreditsButton();
        }
        else if (currentButtonNumber == 3)
        {
            QuitButton();
        }
    }

    public void StartButton()
    {
        startAnim.ChangeState("StartPress");
        SoundManager.Instance.PlaySoundEffect(paperAudio, 1);
        StartCoroutine(LateStarts());
    }
    public void CreditsButton()
    {
        if (isHowToPlayPressedBefore)
        {
            howToPlayAnim.ChangeState("HowToPlayRecover");
            isHowToPlayPressedBefore = false;
        }
        SoundManager.Instance.PlaySoundEffect(paperAudio, 1);

        creditsAnim.ChangeState("CreditsPress");
        howToPlayTAB.SetActive(false);
        creditsTAB.SetActive(true);
        isCreditsPressedBefore = true;
    }
    public void HowToPlayButton()
    {
        if (isCreditsPressedBefore)
        {
            creditsAnim.ChangeState("CreditsRecover");
            isCreditsPressedBefore = false;
        }
        SoundManager.Instance.PlaySoundEffect(paperAudio, 1);

        isHowToPlayPressedBefore = true;
        howToPlayAnim.ChangeState("HowToPlayPress");
        howToPlayTAB.SetActive(true);
        creditsTAB.SetActive(false);

    }
    public void QuitButton()
    {
        SoundManager.Instance.PlaySoundEffect(paperAudio, 1);

        quitAnim.ChangeState("QuitPress");
        StartCoroutine(LateQuit());
    }
    IEnumerator LateQuit()
    {
        yield return new WaitForSeconds(0.4f);
        Application.Quit();

    }
    IEnumerator LateStarts()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level 0");


    }

}
