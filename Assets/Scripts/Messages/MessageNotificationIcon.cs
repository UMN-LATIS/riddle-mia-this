using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageNotificationIcon : MonoBehaviour {

    private Animator anim;
    static private MessageNotificationIcon instance;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        instance = this;
    }

    static public void ShowNotification(bool value)
    {
        instance.anim.SetBool("isVisible", value);
    }

}
