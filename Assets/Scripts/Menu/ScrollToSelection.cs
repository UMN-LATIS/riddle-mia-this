using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollToSelection : MonoBehaviour {

    public float scrollSpeed = 10f;
    public AnimationCurve scrollCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(.2f, 1));

    ScrollRect scrollObject;
    RectTransform scrollRectTranform;
    RectTransform contentRectTransform;
    RectTransform selectedRectTransform;

    void Awake()
    {
        scrollObject = GetComponent<ScrollRect>();
        scrollRectTranform = GetComponent<RectTransform>();
        contentRectTransform = scrollObject.content;
    }

    void Update()
    {
        UpdateScrollToSelected();
    }

    void UpdateScrollToSelected()
    {
        GameObject selected = EventSystem.current.currentSelectedGameObject;
        ScrollToObject(selected);
    }

    public void ScrollToObject(GameObject targetObject, bool reselect = false)
    {
        if (targetObject == null || targetObject.transform.parent != contentRectTransform.transform) return;
        if (!reselect && selectedRectTransform == targetObject.GetComponent<RectTransform>()) return;
        selectedRectTransform = targetObject.GetComponent<RectTransform>();

        StopCoroutine(Scroll(targetObject));
        StartCoroutine(Scroll(targetObject));
    }

    private IEnumerator Scroll(GameObject targetObject)
    {
        float selectedDifference = -selectedRectTransform.localPosition.y;
        float contentHeightDifference = (contentRectTransform.rect.height - scrollRectTranform.rect.height);

        float selectedPosition = (contentRectTransform.rect.height - selectedDifference);
        float currentScrollRectPosition = scrollObject.normalizedPosition.y * contentHeightDifference;
        float above = currentScrollRectPosition - (selectedRectTransform.rect.height / 2) + scrollRectTranform.rect.height;
        float below = currentScrollRectPosition + (selectedRectTransform.rect.height / 2);

        float timer = 0;
        float percentage = 0;

        while(timer <= scrollCurve.keys[scrollCurve.length - 1].time)
        {
            timer += Time.unscaledDeltaTime;
            percentage = scrollCurve.Evaluate(timer);

            // check if selected is out of bounds
            if (selectedPosition > above)
            {
                float step = selectedPosition - above;
                float newY = currentScrollRectPosition + step;
                float newNormalizedY = newY / contentHeightDifference;
                scrollObject.normalizedPosition = Vector2.Lerp(scrollObject.normalizedPosition, new Vector2(0, newNormalizedY), percentage);
            }
            else if (selectedPosition < below)
            {
                float step = selectedPosition - below;
                float newY = currentScrollRectPosition + step;
                float newNormalizedY = newY / contentHeightDifference;
                scrollObject.normalizedPosition = Vector2.Lerp(scrollObject.normalizedPosition, new Vector2(0, newNormalizedY), percentage);
            }

            yield return new WaitForEndOfFrame();
        }
    }

}
