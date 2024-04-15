using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GOFade : MonoBehaviour
{
    public float fadeDuration = 1f;
    public bool fadeOutOnStart = false;
    public bool fadeInOnStart = false;

    private Image image;
    private Color originalColor;
    private float currentAlpha;
    private float timer;
    public bool fadingIn = false;
    public bool fadingOut = false;

    public static GOFade instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
        currentAlpha = originalColor.a;

        if (fadeInOnStart)
            StartFadeIn();
        if (fadeOutOnStart)
            StartFadeOut();
    }

    void Update()
    {
        if (fadingIn)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, originalColor.a, timer / fadeDuration);
            SetAlpha(alpha);

            if (timer >= fadeDuration)
            {
                fadingIn = false;
                timer = 0f;
            }
        }

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
        image.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
    }

    public void StartFadeIn()
    {
        if (!fadingIn)
        {
            fadingOut = false;
            fadingIn = true;
            timer = 0f;
            SetAlpha(0f);
        }
    }

    public void StartFadeOut()
    {
        if (!fadingOut)
        {
            fadingIn = false;
            fadingOut = true;
            timer = 0f;
            SetAlpha(originalColor.a);
        }
    }
}
