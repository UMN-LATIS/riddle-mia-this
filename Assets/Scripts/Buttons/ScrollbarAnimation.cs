using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarAnimation : MonoBehaviour {

    private Animator anim;
    public float delay = 2;
    private Vector3 oldPosition;
    private Scrollbar scrollbar;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        scrollbar = GetComponent<Scrollbar>();
        oldPosition = scrollbar.handleRect.transform.localPosition;
    }

    private void OnEnable()
    {
        anim.SetBool("isVisible", true);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        CheckMovement();
    }

    void CheckMovement()
    {
        if (scrollbar.handleRect.transform.localPosition != oldPosition)
        {
            anim.SetBool("isVisible", true);
            StopAllCoroutines();
            StartCoroutine(DelayFade());
            oldPosition = scrollbar.handleRect.transform.localPosition;
        }
    }

    IEnumerator DelayFade()
    {
        yield return new WaitForSecondsRealtime(delay);

        anim.SetBool("isVisible", false);
    }
}
