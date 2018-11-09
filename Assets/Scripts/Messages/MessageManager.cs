using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour {

    static public MessageManagerHelper currentMessage;
    static private List<MessageManagerHelper> messages = new List<MessageManagerHelper>();

    static public void SetCurrentMessage(MessageObject message)
    {
        for(int i = 0; i < messages.Count; i++)
        {
            if (message == messages[i].message.mail)
            {
                currentMessage = messages[i];
                if (currentMessage.message.mail == MessageNotification.newMessage) MessageNotification.SeenMessage();
                return;
            }
        }
        
    }

    private void Awake()
    {
        messages = new List<MessageManagerHelper>();


        //messages.AddRange(GetComponentsInChildren<MessageDisplay>());
        MessageDisplay[] messageDisplay = GetComponentsInChildren<MessageDisplay>(true);

        for(int i = 0; i < messageDisplay.Length; i++)
        {
            messages.Add(new MessageManagerHelper(messageDisplay[i]));
        }

        CheckMessages();

        MessageNotification.OnNewMessage.AddListener(GrabNewMessage);
        if (PlayerPrefs.HasKey("NumberCompleted"))
        {
            PuzzleManager.numberCompleted = PlayerPrefs.GetInt("NumberCompleted");
            GrabNewMessage();
        }

    }

    private void OnEnable()
    {
        CheckMessages();
    }

    public void CheckMessages()
    {
        for(int i = 0; i < messages.Count; i++)
        {
            MessageObject targetMessage = messages[i].message.mail;
            if(PuzzleManager.numberCompleted >= targetMessage.puzzlesCompletedToShow)
            {
                messages[i].message.gameObject.SetActive(true);
            }
        }
    }

    public class MessageManagerHelper
    {
        public MessageDisplay message;
        public bool isCompleted = false;

        public MessageManagerHelper(MessageDisplay targetMessage)
        {
            message = targetMessage;
        }
    }

    void GrabNewMessage()
    {
        for (int i = 0; i < messages.Count; i++)
        {
            MessageObject targetMessage = messages[i].message.mail;
            if (PuzzleManager.numberCompleted == targetMessage.puzzlesCompletedToShow)
            {
                PlayerPrefs.SetInt("NumberCompleted", PuzzleManager.numberCompleted);
                PlayerPrefs.Save();
                MessageNotification.newMessage = targetMessage;
                return;
            }
        }
    }
}
