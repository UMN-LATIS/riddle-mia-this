using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardScrollScript : MonoBehaviour
{
    public GameObject padding;

    private float preferredHeight = 410f;
    private float smoothTime = 0.2F;
    private float yVelocity;
    private LayoutElement layoutElement;
    // Use this for initialization
    void Start()
    {
        layoutElement = padding.GetComponentInChildren<LayoutElement>();
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR
#else

        if (TouchScreenKeyboard.visible && Screen.orientation == ScreenOrientation.Portrait)
        {
            ScrollRect puzzleScroll = GetComponentInChildren<ScrollRect>();
            if (Mathf.Abs(puzzleScroll.normalizedPosition.y) < 0.01f && layoutElement.preferredHeight == preferredHeight)
            {
                return;
            }
            layoutElement.preferredHeight = preferredHeight;

            float newPosition = Mathf.SmoothDamp(puzzleScroll.normalizedPosition.y, 0, ref yVelocity, smoothTime);
            puzzleScroll.normalizedPosition = new Vector2(0, newPosition);
        }
        else {
            if (Mathf.Abs(layoutElement.preferredHeight) > 0.01f)
            {
                float newHeight = Mathf.SmoothDamp(layoutElement.preferredHeight, 0, ref yVelocity, smoothTime);
                layoutElement.preferredHeight = newHeight;
            }

        } 
#endif

    }
}
