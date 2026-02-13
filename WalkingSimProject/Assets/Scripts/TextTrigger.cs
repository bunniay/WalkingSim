using UnityEngine;
using TMPro;
using System.Collections;

public class TextTrigger : MonoBehaviour
{
    public TextMeshProUGUI textComponent;

    [TextArea]
    public string message;

    [Header("Typing Settings")]
    [Range(0.01f, 0.2f)]
    public float typingSpeed = 0.05f; // Lower = faster

    private Coroutine typingCoroutine;

    private void Start()
    {
        textComponent.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        textComponent.gameObject.SetActive(true);

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        textComponent.gameObject.SetActive(false);

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);
    }

    IEnumerator TypeText()
    {
        textComponent.text = "";

        foreach (char letter in message)
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}