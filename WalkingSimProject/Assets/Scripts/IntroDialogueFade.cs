using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroDialogueFade : MonoBehaviour
{
    [Header("Dialogue (TextMeshPro)")]
    public TMP_Text dialogueText;        // Drag your TextMeshProUGUI here
    public string[] dialogueLines;
    public float textDelay = 2f;

    [Header("Intro Sprite (Optional)")]
    public Image introImage;             // Drag your UI Image here (optional)
    public bool hideSpriteAfterFade = true;

    [Header("Fade Settings")]
    public CanvasGroup fadePanel;        // Drag black panel with CanvasGroup here
    public float fadeDuration = 2f;

    private void Start()
    {
        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        // Start fully black
        fadePanel.alpha = 1f;

        // Make sure sprite is visible during black screen
        if (introImage != null)
            introImage.enabled = true;

        // Play dialogue lines
        foreach (string line in dialogueLines)
        {
            dialogueText.text = line;
            yield return new WaitForSeconds(textDelay);
        }

        dialogueText.text = "";

        // Fade from black to scene
        yield return StartCoroutine(FadeIn());

        // Hide sprite after fade if enabled
        if (hideSpriteAfterFade && introImage != null)
            introImage.enabled = false;
    }

    IEnumerator FadeIn()
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadePanel.alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);
            yield return null;
        }

        fadePanel.alpha = 0f;
    }
}