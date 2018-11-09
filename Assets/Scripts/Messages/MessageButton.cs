using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MessageButton : MonoBehaviour, ISubmitHandler, IPointerClickHandler
{
    private MessageDisplay message;

    void Awake()
    {
        message = GetComponent<MessageDisplay>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnSubmit(eventData);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        MessageAnimation.PlayOutro(GetComponent<Button>());
        MessageManager.SetCurrentMessage(message.mail);
    }
}
