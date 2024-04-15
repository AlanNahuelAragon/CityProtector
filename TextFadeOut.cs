using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFadeOut : MonoBehaviour
{
    public float fadeDuration = 1f;

    private Text text;
    private Color originalColor;
    private float currentAlpha;
    private float timer;
    private bool fadingOut = false;


    void Start()
    {
        text = GetComponent<Text>();
        originalColor = text.color;
        currentAlpha = originalColor.a;

        StartFadeOut();
    }

    void Update()
    {
        if (fadingOut)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(originalColor.a, 0f, timer / fadeDuration);
            SetAlpha(alpha);

            if (timer >= fadeDuration)
            {
                fadingOut = false;
                timer = 0f;
            }
        }
    }

    void SetAlpha(float alpha)
    {
        currentAlpha = alpha;
        text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
    }


    public void StartFadeOut()
    {
        if (!fadingOut)
        {
            fadingOut = true;
            timer = 0f;
            SetAlpha(originalColor.a);
        }
    }
}
