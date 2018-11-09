using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class UIMenuAutoSwitcher : MonoBehaviour {

    public string targetMenu;
    public float delay = 0f;
    private MenuSystemManager manager;

    public bool lastOpenedMenu = false;

    public UnityEvent OnSwitch;

    void Awake()
    {
        manager = GetComponentInParent<MenuSystemManager>();
    }

    void OnEnable()
    {
        Invoke("SwitchMenu", delay);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    private void SwitchMenu()
    {
        OnSwitch.Invoke();
        if(lastOpenedMenu) manager.LastOpenedMenu();
        else manager.SwitchMenu(targetMenu);
    }

    public void ResetTimer()
    {
        CancelInvoke();
        Invoke("SwitchMenu", delay);
    }
}
