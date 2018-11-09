using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultsManager : MonoBehaviour {

    private Animator anim;
    public float messageDelay = 1.5f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        PuzzleManager.OnSendAnswer.AddListener(DisplayResult);
    }

    public void DisplayResult()
    {
        Invoke("DelayResult", .1f);
    }

    void DelayResult()
    {
        if (MessageManager.currentMessage.isCompleted)
        {
            anim.SetBool("isCorrect", true);
            Invoke("NewMessage", messageDelay);
        }
        else
        {
            anim.SetBool("isCorrect", false);
        }

        anim.SetTrigger("showResult");
    }

    void NewMessage()
    {
        MessageNotification.NewMessage();
    }
}
