using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageHint : MonoBehaviour {

    public Image mapImage;
    public TextMeshProUGUI mapText;
    public TextMeshProUGUI puzzleText;
    public TextMeshProUGUI answerText;
    private Animator anim;

    public Toggle firstSelection;
    public MenuSystemManager menu;

    private void Awake()
    {
        PuzzleManager.OnAskHelp.AddListener(ShowHint);
        anim = GetComponent<Animator>();
    }
    
    void ShowHint()
    {
        firstSelection.isOn = true;
        menu.SwitchMenu("Hint Map");
        mapImage.sprite = MessageManager.currentMessage.message.mail.mapHintImage;
        mapText.text = MessageManager.currentMessage.message.mail.mapHint;
        puzzleText.text = MessageManager.currentMessage.message.mail.puzzleHint;
        answerText.text = MessageManager.currentMessage.message.mail.answerHint;

        anim.SetBool("isActive", true);
    }

    public void HideHint()
    {
        anim.SetBool("isActive", false);
    }

}
