using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleAnimation : MonoBehaviour {

    private Animator anim;
    private Toggle toggle;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        toggle = GetComponent<Toggle>();

        toggle.onValueChanged.AddListener(CheckValue);
    }

    private void OnEnable()
    {
        CheckValue(toggle.isOn);
    }

    void CheckValue(bool value)
    {
        anim.SetBool("isActive", value);
    }

}
