using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwitch : MonoBehaviour {

    public Color main;
    public Color secondary;
    public float duration = 1;
    public bool useCurrentMessageColor;

    private void OnEnable()
    {
        if (useCurrentMessageColor)
        {
            ColorManager.SwitchColor(MessageManager.currentMessage.message.mail.mainColor, MessageManager.currentMessage.message.mail.secondaryColor, duration);
        }
        else
        {
            ColorManager.SwitchColor(main, secondary, 1);
        }
        
    }

}
