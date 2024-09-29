using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Fade : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private CanvasGroup fadeBG;

    public float Duration => duration;

    public void Show(float duration = -1)
    {
        if (duration < 0)
            duration = this.duration;

        StartCoroutine(FadeBG(0, 1, duration));
    }

    public void Hide(float duration = -1)
    {
        if (duration < 0)
            duration = this.duration;

        StartCoroutine(FadeBG(1, 0, duration));
    }

    private IEnumerator FadeBG(float from, float to, float duration)
    {
        float timer = 0;

        while (timer <= duration)
        {
            timer += Time.deltaTime;
            float ratio = timer / duration;
            fadeBG.alpha = Mathf.Lerp(from, to, ratio);
            yield return null;
        }

        fadeBG.alpha = to;
    }
}
