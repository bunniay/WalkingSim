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

    [Header("Fade Settings")]
    public CanvasGroup fadePanel;
    public float fadeDuration = 2f;

    private void Start()
    {
        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        //Start fully black
        fadePanel.alpha = 1f;

        //intro image is invisible here 
        if (introImage != null)
        {
            Color imgColor = introImage.color;
            introImage.color = new Color(imgColor.r, imgColor.g, imgColor.b, 0f);
        }

        //Small delay before image fades in (optional)
        yield return new WaitForSeconds(0.1f);

        //Fade image in
        if (introImage != null)
            yield return StartCoroutine(FadeImage(0f, 1f));

        // Play dialogue
        foreach (string line in dialogueLines)
        {
            yield return StartCoroutine(TypeLine(line));
            yield return new WaitForSeconds(lineDelay);
        }

        dialogueText.text = "";

        // Fade entire screen (image + text + black panel)
        yield return StartCoroutine(FadeCanvas(1f, 0f));
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

    IEnumerator FadeCanvas(float start, float end)
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadePanel.alpha = Mathf.Lerp(start, end, time / fadeDuration);
            yield return null;
        }

        fadePanel.alpha = end;
    }

    IEnumerator FadeImage(float start, float end)
    {
        float time = 0f;
        Color baseColor = introImage.color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(start, end, time / fadeDuration);
            introImage.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
            yield return null;
        }

        introImage.color = new Color(baseColor.r, baseColor.g, baseColor.b, end);
    }
}