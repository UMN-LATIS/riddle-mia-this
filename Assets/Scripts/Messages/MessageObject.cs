using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Message")]
public class MessageObject : ScriptableObject {

    public string subjectLine;
    public string sender;
    public Sprite profilePic;
    [HideInInspector] public string timeStamp;
    [TextArea()] public string message;
    public Sprite mapHintImage;
    public string mapHint;
    public string puzzleHint;
    public string answerHint;
    public GameObject puzzle;
    public int puzzlesCompletedToShow = 0;

    public Color mainColor;
    public Color secondaryColor;

    public void SetTime()
    {
        timeStamp = System.DateTime.Now.ToString("hh:mmtt").ToLower();
    }
}
