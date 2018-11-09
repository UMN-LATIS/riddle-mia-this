using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleNumeric : MonoBehaviour {

    public string prompt;
    public TextMeshProUGUI promptObject;

    public int answer;
    public TMP_InputField inputField;

    private void Start()
    {
        PuzzleManager.OnSendAnswer.AddListener(CheckAnswer);
        inputField.onEndEdit.AddListener(CheckSend);
    }

    private void OnEnable()
    {
        promptObject.text = prompt;
        CheckSend(inputField.text);
    }

    void CheckAnswer()
    {
        if (MessageManager.currentMessage.isCompleted) return;
        if(int.Parse(inputField.text) == answer)
        {
            Debug.Log("CORRECT");
            MessageManager.currentMessage.isCompleted = true;
            PuzzleManager.numberCompleted++;
        }
        else
        {
            Debug.Log("INCORRECT");
        }
    }

    void CheckSend(string text)
    {
        if (text != "") PuzzleSendManager.SetVisibilty(SendButtonState.Active);
        else PuzzleSendManager.SetVisibilty(SendButtonState.Disabled);
    }
}
