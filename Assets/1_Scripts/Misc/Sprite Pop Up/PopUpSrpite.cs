using System.Collections;
using UnityEngine;

public class PopUpSprite : MonoBehaviour
{
    public float popUpDuration = 2f; 
    public float popUpHeight = 2f;
    public float fadeDuration = 2f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        StartCoroutine(PopUpAndFade());
    }

    private IEnumerator PopUpAndFade()
    {
        Vector3 startPosition = transform.localPosition;
        Vector3 targetPosition = startPosition + new Vector3(0, popUpHeight, 0);

        float elapsedTime = 0;

        // Pop Up
        while (elapsedTime < popUpDuration)
        {
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / popUpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPosition;

        // Fade Out
        elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        Destroy(gameObject);
    }
}
