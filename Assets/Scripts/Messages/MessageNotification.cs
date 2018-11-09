using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MessageNotification : MonoBehaviour {

    static public UnityEvent OnNewMessage = new UnityEvent();
    static public MessageObject newMessage;

    private MessageDisplay display;
    private Animator anim;

    public MenuSystemManager pageMenu;
    public MenuSystemManager messageMenu;
    public Toggle mail;

    public float notificationDuration = 5;

    static public MessageNotification instance;

    static public bool isNewMessage;

    private void Awake()
    {
        display = GetComponent<MessageDisplay>();
        anim = GetComponent<Animator>();

        OnNewMessage.AddListener(UpdateDisplayDelay);
        instance = this;
    }

    static public void NewMessage()
    {
        MessageNotificationIcon.ShowNotification(true);
        Handheld.Vibrate();
        isNewMessage = true;
        OnNewMessage.Invoke();
    }

    public void DirectMessage(MessageObject message)
    {
        newMessage = message;
        MessageNotificationIcon.ShowNotification(true);
        Handheld.Vibrate();
        isNewMessage = true;
        UpdateDisplayDelay();
    }

    static public void SeenMessage()
    {
        isNewMessage = false;
        MessageNotificationIcon.ShowNotification(false);
    }

    void UpdateDisplayDelay()
    {
        Invoke("UpdateDisplay", .1f);
    }

    void UpdateDisplay()
    {
        display.mail = newMessage;
        display.DisplayMessage();
        anim.SetTrigger("Visible");
        StopAllCoroutines();
        StartCoroutine(NotificationTimer());
    }

    public void ViewMessage()
    {
        mail.isOn = true;
        if (pageMenu.GetCurrentMenu() == "Mail") messageMenu.SwitchMenu("Buffer");
        else messageMenu.SwitchMenu("Puzzle");
        pageMenu.SwitchMenu("Mail");
        anim.SetTrigger("Hidden");
        MessageManager.SetCurrentMessage(newMessage);
        StopAllCoroutines();
    }

    IEnumerator NotificationTimer()
    {
        yield return new WaitForSecondsRealtime(notificationDuration);
        anim.SetTrigger("Hidden");
    }

}
