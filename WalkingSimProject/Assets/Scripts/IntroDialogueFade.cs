using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroDialogueFade : MonoBehaviour
{
    [Header("Dialogue (TextMeshPro)")]
    public TMP_Text dialogueText;
    public string[] dialogueLines;

    [Tooltip("Time between each character appearing")]
    public float typingSpeed = 0.05f;

    [Tooltip("Time to wait after full line is displayed")]
    public float lineDelay = 1.5f;

    [Header("Intro Sprite")]
    public Image introImage;
    public bool hideSpriteAfterFade = true;

    [Header("Fade Settings")]
    public CanvasGroup fadePanel;
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

        // Play dialogue lines with typewriter
        foreach (string line in dialogueLines)
        {
            yield return StartCoroutine(TypeLine(line));
            yield return new WaitForSeconds(lineDelay);
        }

        dialogueText.text = "";

        // Fade from black to scene
        yield return StartCoroutine(FadeIn());

        // Hide sprite after fade if enabled
        if (hideSpriteAfterFade && introImage != null)
            introImage.enabled = false;
    }

    IEnumerator TypeLine(string line)
    {
        dialogueText.text = "";

        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
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