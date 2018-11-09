using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class GameObjectStates : MonoBehaviour {

    public UnityEvent OnEnabled;
    public UnityEvent OnDisabled;

    void OnEnable()
    {
        OnEnabled.Invoke();
    }

    void OnDisable()
    {
        OnDisabled.Invoke();
    }
}
