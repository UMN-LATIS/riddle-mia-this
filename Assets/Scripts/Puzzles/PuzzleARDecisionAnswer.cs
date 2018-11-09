using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleARDecisionAnswer : MonoBehaviour {

    public MessageObject messageYes;
    public MessageObject messageNo;

    public GameObject objectYes;
    public GameObject objectNo;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SubmitResponse(bool value)
    {
        MessageObject targetAnswer;
        if(value)
        {
            targetAnswer = messageYes;
            objectYes.SetActive(true);
        }
        else
        {
            targetAnswer = messageNo;
            objectNo.SetActive(true);
        }
        
        MessageNotification.instance.DirectMessage(targetAnswer);

        anim.SetBool("MadeDecision", true);
    }

}
