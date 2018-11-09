using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageDisplay : MonoBehaviour {

    public MessageObject mail;
    public bool useCurrentlySelectedMessage;

    [Header("Objects")]
    public TextMeshProUGUI senderObject;
    public TextMeshProUGUI subjectObject;
    public TextMeshProUGUI timeObject;
    public TextMeshProUGUI messageObject;
    public Image profileObject;
    public GameObject puzzleObject;
    private GameObject oldPuzzle;

    private void Start()
    {
        if(mail != null) mail.SetTime();
    }

    private void OnEnable()
    {
        if (useCurrentlySelectedMessage) mail = MessageManager.currentMessage.message.mail;
        DisplayMessage();
    }

    public void DisplayMessage()
    {
        if (mail == null) return;
        senderObject.text = mail.sender;
        subjectObject.text = mail.subjectLine;
        if(timeObject != null) timeObject.text = mail.timeStamp;
        if (messageObject != null) messageObject.text = mail.message;
        profileObject.sprite = mail.profilePic;
        if (puzzleObject != null)
        {
            if(oldPuzzle != null) Destroy(oldPuzzle);
            if(mail.puzzle != null)
            {
                GameObject newPuzzle = Instantiate(mail.puzzle, puzzleObject.transform);
                newPuzzle.name = "Puzzle";

                oldPuzzle = newPuzzle;
            }
            
        }
    }
}
