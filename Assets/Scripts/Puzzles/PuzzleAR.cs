using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PuzzleAR : MonoBehaviour {

    public MessageObject targetMessage;
    public bool universalARTrigger;

    private void Update()
    {
        if ((GetComponent<Renderer>() != null && GetComponent<Renderer>().enabled) || (GetComponent<Canvas>() != null && GetComponent<Canvas>().enabled))
        {
            if (!universalARTrigger && targetMessage != null && MessageManager.currentMessage.message.mail != targetMessage) return;
            if (MessageManager.currentMessage == null || MessageManager.currentMessage.isCompleted) return;
            Debug.Log("CORRECT AR PUZZLE");
            MessageManager.currentMessage.isCompleted = true;
            PuzzleManager.numberCompleted++;
            MessageNotification.NewMessage();
        }
        
    }

}
