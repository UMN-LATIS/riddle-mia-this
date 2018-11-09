using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SentientBytes.Themo;

public class ColorManager : MonoBehaviour {

    private Color mainColor;
    private Color secondaryColor;

    public AnimationCurve colorCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    static private ColorManager instance;

    private void Awake()
    {
        instance = this;
    }

    static public void SwitchColor(Color mainColor, Color secondaryColor, float transitionLength)
    {
        instance.StartCoroutine(instance.ChangeColor(mainColor, secondaryColor, transitionLength));
    }

    IEnumerator ChangeColor(Color mainColor, Color secondaryColor, float transitionLength)
    {
        Color prevMain = ThemeManager.Instance.Themes[0].Color;
        Color prevSecondary = ThemeManager.Instance.Themes[1].Color;

        float timer = 0;
        float percentage;

        while(timer < transitionLength)
        {
            timer += Time.deltaTime;
            percentage = timer / transitionLength;

            ThemeManager.Instance.Themes[0].Color = Color.Lerp(prevMain, mainColor, colorCurve.Evaluate(percentage));
            ThemeManager.Instance.Themes[1].Color = Color.Lerp(prevSecondary, secondaryColor, colorCurve.Evaluate(percentage));

            yield return new WaitForEndOfFrame();
        }
    }

}
