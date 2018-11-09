using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SendButtonState {Hidden, Disabled, Active}

public class PuzzleSendManager : MonoBehaviour {

    public Button sendButton;
    static private PuzzleSendManager instance;
    public Button helpButton;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        SetVisibilty(SendButtonState.Hidden);
        if (MessageManager.currentMessage.message.mail.answerHint == "") helpButton.gameObject.SetActive(false);
        else helpButton.gameObject.SetActive(true);
    }

    static public void SetVisibilty(SendButtonState state)
    {
        switch (state)
        {
            case SendButtonState.Hidden:
                instance.sendButton.interactable = false;
                instance.sendButton.gameObject.SetActive(false);
                break;
            case SendButtonState.Disabled:
                instance.sendButton.interactable = false;
                instance.sendButton.gameObject.SetActive(true);
                break;
            case SendButtonState.Active:
                instance.sendButton.interactable = true;
                instance.sendButton.gameObject.SetActive(true);
                break;
            default:
                instance.sendButton.interactable = true;
                instance.sendButton.gameObject.SetActive(true);
                break;
        }
    }

}
