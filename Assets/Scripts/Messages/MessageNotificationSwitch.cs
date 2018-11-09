using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MessageNotificationSwitch : MonoBehaviour, IPointerClickHandler, ISubmitHandler {

    public MenuSystemManager menu;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSubmit(eventData);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        if (MessageNotification.isNewMessage) menu.SwitchMenu("Messages");
    }
}
