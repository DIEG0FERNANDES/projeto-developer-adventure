using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextTrigger : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    [Header("ArrowLeft")]
    public Image displayImage1;
    [Header("ArrowUp")]
    public Image displayImage2;
    [Header("ArrowDown")]
    public Image displayImage3;
    [Header("ArrowRight")]
    public Image displayImage4;
    private CanvasGroup textCanvasGroup;
    private CanvasGroup imageCanvasGroup1;
    private CanvasGroup imageCanvasGroup2;
    private CanvasGroup imageCanvasGroup3;
    private CanvasGroup imageCanvasGroup4;
    private bool hasDisplayed = false;

    private void Start()
    {
        textCanvasGroup = displayText.GetComponent<CanvasGroup>();
        imageCanvasGroup1 = displayImage1.GetComponent<CanvasGroup>();
        imageCanvasGroup2 = displayImage2.GetComponent<CanvasGroup>();
        imageCanvasGroup3 = displayImage3.GetComponent<CanvasGroup>();
        imageCanvasGroup4 = displayImage4.GetComponent<CanvasGroup>();

        if (textCanvasGroup != null) textCanvasGroup.alpha = 0;
        if (imageCanvasGroup1 != null) imageCanvasGroup1.alpha = 0;
        if (imageCanvasGroup2 != null) imageCanvasGroup2.alpha = 0;
        if (imageCanvasGroup3 != null) imageCanvasGroup3.alpha = 0;
        if (imageCanvasGroup4 != null) imageCanvasGroup4.alpha = 0;

        displayText.gameObject.SetActive(false);
        displayImage1.gameObject.SetActive(false);
        displayImage2.gameObject.SetActive(false);
        displayImage3.gameObject.SetActive(false);
        displayImage4.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //
        {
            if (!hasDisplayed)
            {
                displayText.text = "USE      /      /      /      PARA MOVER-SE!";
                displayText.gameObject.SetActive(true);
                displayImage1.gameObject.SetActive(true);
                displayImage2.gameObject.SetActive(true);
                displayImage3.gameObject.SetActive(true);
                displayImage4.gameObject.SetActive(true);

                StartCoroutine(FadeIn(textCanvasGroup));
                StartCoroutine(FadeIn(imageCanvasGroup1));
                StartCoroutine(FadeIn(imageCanvasGroup2));
                StartCoroutine(FadeIn(imageCanvasGroup3));
                StartCoroutine(FadeIn(imageCanvasGroup4));
                hasDisplayed = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeOut(textCanvasGroup));
            StartCoroutine(FadeOut(imageCanvasGroup1));
            StartCoroutine(FadeOut(imageCanvasGroup2));
            StartCoroutine(FadeOut(imageCanvasGroup3));
            StartCoroutine(FadeOut(imageCanvasGroup4));
        }
    }

    private IEnumerator FadeIn(CanvasGroup canvasGroup)
    {

        float duration = 1f;
        float elapsed = 0f;

        canvasGroup.alpha = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
         canvasGroup.alpha = 1;
    }

    private IEnumerator FadeOut(CanvasGroup canvasGroup)
    {

        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1 - (elapsed / duration));
            yield return null;
        }

        canvasGroup.alpha = 0;
        canvasGroup.gameObject.SetActive(false);
    }
}
