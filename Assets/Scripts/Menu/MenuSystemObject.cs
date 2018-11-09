using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//Author: Charles McGregor
//Last Edit: 4/28/2015
//Description: Manages menu items in object and menu data
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CanvasGroup))]
public class MenuSystemObject : MonoBehaviour {

	//Variables
	[SerializeField] private string menuName = ""; //Name of menu
	public string MenuName {get{return menuName;}}
	
	public Selectable firstSelection = null; //First menu item that is selected

	private Animator anim; //The animator attached to the game object
	private List<Selectable> menuOptions = new List<Selectable>(); //List of menus objects ranging from buttons to sliders
    private List<bool> defaultInteractable = new List<bool>();
	private float startTime; //Start time for outro

	private bool startingOutro; //Starting the outro animation

    [HideInInspector] public UnityEvent OnOutroStart;

	void Awake()
	{
		anim = GetComponent<Animator>();
		Selectable[] selectablesArray = GetComponentsInChildren<Selectable>(true);
		foreach(Selectable newSelectable in selectablesArray)
		{
			menuOptions.Add(newSelectable);
            defaultInteractable.Add(newSelectable.interactable);
		}
	}

    private void OnEnable()
    {
        InitializeSelection();
    }

    //Initializes the first selection of object
    public void InitializeSelection()
	{
#if !UNITY_WEBGL
        if (firstSelection != null)
        {
            if (Time.timeScale != 0) Invoke("SelectFirstSelection", .2f);
            else SelectFirstSelection();
        }
#endif

    }

    void SelectFirstSelection()
    {
        ExecuteEvents.Execute<ISelectHandler>(firstSelection.gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.selectHandler);
        EventSystem.current.SetSelectedGameObject(firstSelection.gameObject);
    }

    //Plays the outro of the menu
    //Returns true when finished, false while still playing
    public bool Outro()
	{
        if (anim == null) return true;
		if(anim.runtimeAnimatorController == null)
		{
			return true;
		}

		if(!startingOutro)
		{
			if(anim.isInitialized) anim.SetTrigger("Outro");
			startingOutro = true;
			startTime = Time.unscaledTime;
            if (GetComponent<CanvasGroup>() != null)
            {
                GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
            EventSystem.current.SetSelectedGameObject(null);
            OnOutroStart.Invoke();
        }
		else
		{
			if(anim.GetCurrentAnimatorClipInfo(0).Length > 0)
			{
				if(Time.unscaledTime - startTime >= anim.GetCurrentAnimatorClipInfo(0)[0].clip.length)
				{
					startingOutro = false;
					return true;
				}
			}
		}

		return false;
	}

	//Enables all of the menu options
	public void EnableOptions()
	{
        for(int i = 0; i < menuOptions.Count; i++)
        {
            if (menuOptions[i] != null) menuOptions[i].enabled = true;
            else
            {
                menuOptions.RemoveAt(i);
                i--;
            }
        }
        if (GetComponent<CanvasGroup>() != null)
        {
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        InitializeSelection();
	}

	//Disables all of the menu options
	public void DisableOptions()
	{
		if(menuOptions == null || menuOptions.Count <= 0) return;
		foreach(Selectable selectableObject in menuOptions)
		{
            if (selectableObject != null) selectableObject.enabled = false;
		}
    }

    public void AddMenuItem(MenuSystemItem targetItem)
	{
		if(!menuOptions.Contains(targetItem.GetComponent<Selectable>())) menuOptions.Add(targetItem.GetComponent<Selectable>());
	}
}
