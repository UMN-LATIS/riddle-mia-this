using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleToggle : MonoBehaviour {

    public string prompt;
    public TextMeshProUGUI promptObject;
    public PuzzleToggleHelper[] toggles;

    private void Start()
    {
        PuzzleManager.OnSendAnswer.AddListener(CheckAnswer);
    }

    private void OnEnable()
    {
        promptObject.text = prompt;
        PuzzleSendManager.SetVisibilty(SendButtonState.Active);
    }

    void CheckAnswer()
    {
        if (MessageManager.currentMessage.isCompleted) return;
        for (int i = 0; i < toggles.Length; i++)
        {
            if(toggles[i].toggleObject.isOn != toggles[i].isOn)
            {
                Debug.Log("INCORRECT");
                return;
            }
        }
        
        Debug.Log("CORRECT");
        MessageManager.currentMessage.isCompleted = true;
        PuzzleManager.numberCompleted++;
    }

    [System.Serializable]
    public class PuzzleToggleHelper
    {
        public Toggle toggleObject;
        public bool isOn;
    }
}
