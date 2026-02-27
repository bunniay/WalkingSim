using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TriggerFadeToBlack : MonoBehaviour
{
    [Header("UI References")]
    public Image blackScreen;
    public TextMeshProUGUI fadeText;

    [Header("Timing Settings")]
    public float blackFadeDuration = 1f;          // How long black screen takes to fade in
    public float delayBeforeText = 1f;            // delay after black screen
    public float textFadeDuration = 1f;           // text time to fade in

    [Header("Trigger Settings")]
    public string playerTag = "Player";

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag(playerTag))
        {
            triggered = true;
            StartCoroutine(FadeSequence());
        }
    }

    IEnumerator FadeSequence()
    {
        //Fade black screen in
        yield return StartCoroutine(FadeImage(blackScreen, 0f, 1f, blackFadeDuration));

        //adjustable delay
        yield return new WaitForSeconds(delayBeforeText);

        //Fade text in
        yield return StartCoroutine(FadeText(0f, 1f, textFadeDuration));
    }

    IEnumerator FadeImage(Image image, float startAlpha, float endAlpha, float duration)
    {
        float time = 0f;
        Color color = image.color;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            image.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        image.color = new Color(color.r, color.g, color.b, endAlpha);
    }

    IEnumerator FadeText(float startAlpha, float endAlpha, float duration)
    {
        float time = 0f;
        Color color = fadeText.color;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / duration);
            fadeText.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        fadeText.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}