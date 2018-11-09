using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadingManager : MonoBehaviour {

    public HeadingManagerHelper[] content;
    private int target;
    private Animator anim;
    private int currentContent = 0;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangeHeader(int targetHeader)
    {
        if (currentContent == targetHeader) return;
        target = targetHeader;
        anim.SetTrigger("Switch");
        currentContent = targetHeader;
    }

    public void SwitchInfo()
    {
        for(int i = 0; i < content.Length; i++)
        {
            content[i].SetActive(false);
        }

        content[target].SetActive(true);
    }

    [System.Serializable]
    public class HeadingManagerHelper
    {
        public GameObject button;
        public GameObject info;
        public MenuSystemItem menu;

        public void SetActive(bool value)
        {
            button.SetActive(value);
            info.SetActive(value);
            menu.enabled = value;
        }
    }

}