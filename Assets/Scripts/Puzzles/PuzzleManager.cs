using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleManager : MonoBehaviour {

    public static UnityEvent OnSendAnswer = new UnityEvent();
    public static UnityEvent OnAskHelp = new UnityEvent();

    public static int numberCompleted = 0;

    public void SendAnswer()
    {
        if (MessageManager.currentMessage.isCompleted) return;
        OnSendAnswer.Invoke();
    }

    public void AskHelp()
    {
        OnAskHelp.Invoke();
    }

}
