using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageAnimation : MonoBehaviour {

    private Button[] buttons;
    static public Button selectedButton;

    static private MessageAnimation instance;

    public float delay = .2f;
    public MenuSystemManager menu;

    public float delayOutro = .3f;
    public float delayOutroSwitch = .7f;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>(true);
        instance = this;
    }

    private void OnEnable()
    {
        StartCoroutine(Intro());
    }

    static public void PlayOutro(Button targetButton)
    {
        selectedButton = targetButton;
        instance.StartCoroutine(instance.Outro());
    }

    IEnumerator Intro()
    {
        //Debug.Log("TESTING");
        //yield return new WaitForSeconds(1);
        for(int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].gameObject.activeInHierarchy) buttons[i].GetComponent<Animator>().SetTrigger("Intro");
            else continue;
            yield return new WaitForSecondsRealtime(delay);
        }
    }

    IEnumerator Outro()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            if(buttons[i] != selectedButton) buttons[i].GetComponent<Animator>().SetTrigger("Outro");
        }

        yield return new WaitForSecondsRealtime(delayOutro);

        selectedButton.GetComponent<Animator>().SetTrigger("Outro");

        yield return new WaitForSecondsRealtime(delayOutroSwitch);

        menu.SwitchMenu("Puzzle");
    }

}
