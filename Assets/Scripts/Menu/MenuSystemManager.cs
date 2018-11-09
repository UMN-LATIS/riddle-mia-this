using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

//Author: Charles McGregor
//Last Edit: 4/28/2015
//Description: Manages transitioning between different MenuSystemObjects
public class MenuSystemManager : MonoBehaviour {

	//Variables
	[SerializeField] private MenuSystemObject startingMenu = null; //First menu that is selected
	[SerializeField] private bool autoSelectStartingMenu = true; //Selects start
	[SerializeField] private bool autofillMenus = true; //Automatically fills the menuList with children of current object
    [SerializeField] private bool clearMenuListOnDisable = false;
	[SerializeField] private List<MenuSystemObject> menuList; //Contains all of the menus this manager governs

	private List<MenuSystemObject> activeMenus = new List<MenuSystemObject>(); //List of all active (open) menus
	private List<MenuSystemObject> transitioningMenus = new List<MenuSystemObject>(); //List of menus that are currently transitioning
	private int amountOfOutros = 0; //Current amount of outros playing
	private bool isTransitioning = false; //Whether the menus are transitioning or not

    static public MenuSystemManager instance;

    private MenuSystemObject lastOpenedMenu;

    void Awake()
	{
        if (instance == null) instance = GetComponent<MenuSystemManager>();
        InitializeManager();
	}

	void Update()
	{
		if(isTransitioning)
		{
			TransitionMenus();
		}
	}

    void OnDisable()
    {
        if (!clearMenuListOnDisable) return;
        activeMenus.Clear();
        transitioningMenus.Clear();
        for (int i = 0; i < menuList.Count; i++)
        {
            menuList[i].gameObject.SetActive(false);
        }
    }

    //Initializes the menus by filling the menuList and selecting the first menu
    void InitializeManager()
	{
		if(autofillMenus) menuList.AddRange(GetComponentsInChildren<MenuSystemObject>(true));

		foreach(MenuSystemObject menuObject in menuList)
		{
			menuObject.gameObject.SetActive(false);
		}

		if(startingMenu != null) activeMenus.Add(startingMenu);
		else if(menuList != null && autoSelectStartingMenu) activeMenus.Add(menuList[0]);
		isTransitioning = true;
	}

	//Begins the process to switch to the next menu
	//nextMenu = The next target menu
	public void SwitchMenu(string nextMenu)
	{
		if(isTransitioning) return;
        if (GetCurrentMenu() == nextMenu) return;
        if(activeMenus.Count > 0) lastOpenedMenu = activeMenus[activeMenus.Count - 1];
        if (MenuExist(nextMenu) && nextMenu != GetCurrentMenu())
		{
            //ExecuteEvents.Execute<IDeselectHandler>(EventSystem.current.currentSelectedGameObject, new BaseEventData(EventSystem.current), ExecuteEvents.deselectHandler);
            MenuSystemObject newMenu = GetMenuObject(nextMenu);
			if(activeMenus.Contains(newMenu))
			{
				ClearActiveMenus(activeMenus.IndexOf(newMenu));
				isTransitioning = true;
			}
			else
			{
				ClearActiveMenus(-1);
				activeMenus.Add(GetMenuObject(nextMenu));
				isTransitioning = true;
            }
		}
    }

    //Begins the process to switch to the next menu
    //nextMenu = The next target menu | keepActive = keep current menu active
    public void SwitchMenu(string nextMenu, bool keepActive)
	{
        if (GetCurrentMenu() == nextMenu) return;
        if (isTransitioning) return;
        //ExecuteEvents.Execute<IDeselectHandler>(EventSystem.current.currentSelectedGameObject, new BaseEventData(EventSystem.current), ExecuteEvents.deselectHandler);
        MenuSystemObject newMenu = GetMenuObject(nextMenu);
        lastOpenedMenu = activeMenus[activeMenus.Count - 1];
        if (activeMenus.Contains(newMenu) && nextMenu != GetCurrentMenu())
		{
			ClearActiveMenus(activeMenus.IndexOf(newMenu));
			isTransitioning = true;
        }
		else
		{
			if(!keepActive) ClearActiveMenus(-1);
			activeMenus.Add(GetMenuObject(nextMenu));
			isTransitioning = true;
        }
	}

	//Checks to see if a menu exists
	//targetMenu = the menu that is being searched for
	//Returns true if exists, false if not
	bool MenuExist(string targetMenu)
	{
		foreach(MenuSystemObject menuObject in menuList)
		{
			if(menuObject.MenuName == targetMenu) return true;
		}

		Debug.LogError("MENU ERROR: " + targetMenu + " does not exist.");
		return false;
	}

	//Grabs a menu from the menuList
	//targetMenu = menu that will be grabed
	//Returns MenuSystemObject if true, null if not
	MenuSystemObject GetMenuObject(string targetMenu)
	{
		foreach(MenuSystemObject menuObject in menuList)
		{
			if(menuObject.MenuName == targetMenu) return menuObject;
		}

		return null;
	}

	//Clears the active menu list
	//index = clears up to the given index, insert -1 to clear all of list
	void ClearActiveMenus(int index)
	{
		if(index > activeMenus.Count || activeMenus.Count == 0) return;
		for(int i = activeMenus.Count-1; i > index; i--)
		{
			transitioningMenus.Add(activeMenus[i]);
			activeMenus.RemoveAt(i);
		}
	}

	//Transitions menus into and out of menus that are queued
	//Returns true if finished transitioning, false if is not finished
	bool TransitionMenus()
	{
		foreach(MenuSystemObject menuObject in transitioningMenus)
		{
			if(menuObject == null) continue;
			if(menuObject.Outro())
			{
				amountOfOutros++;
				menuObject.DisableOptions();
				menuObject.gameObject.SetActive(false);
			}
		}

		if(amountOfOutros == transitioningMenus.Count)
		{
			transitioningMenus.Clear();
			amountOfOutros=0;
			isTransitioning = false;

			DisableButtons();

			if(activeMenus.Count > 0)
			{
				activeMenus[activeMenus.Count-1].gameObject.SetActive(true);
				activeMenus[activeMenus.Count-1].EnableOptions();
				//activeMenus[activeMenus.Count-1].InitializeSelection();
			}

			return true;
		}
		else
		{
			return false;
		}
	}

	//Disables last active menu items
	void DisableButtons()
	{
		foreach(MenuSystemObject menuObject in activeMenus)
		{
			if(menuObject != null) menuObject.DisableOptions();
		}
	}

	public string GetCurrentMenu()
	{
		if(activeMenus.Count <=0) return "";
		return activeMenus[activeMenus.Count-1].MenuName;
	}

    public void NextMenu()
    {
        if (activeMenus.Count <= 0) return;

        int nextMenu = 0;

        for (int i = 0; i < menuList.Count; i++)
        {
            if (menuList[i].MenuName == activeMenus[activeMenus.Count - 1].MenuName)
            {
                nextMenu = i + 1;
            }
        }

        //int nextMenu = menuList.BinarySearch(activeMenus[activeMenus.Count - 1]) + 1;

        if (nextMenu >= menuList.Count) nextMenu = 0;
        SwitchMenu(menuList[nextMenu].MenuName);
    }

    public void PreviousMenu()
    {
        if (activeMenus.Count <= 0) return;

        int prevMenu = 0;

        for (int i = 0; i < menuList.Count; i++)
        {
            if(menuList[i].MenuName == activeMenus[activeMenus.Count - 1].MenuName)
            {
                prevMenu = i - 1;
            }
        }

        //int prevMenu = menuList.IndexOf(activeMenus[activeMenus.Count - 1]) -1;

        if (prevMenu < 0) prevMenu = menuList.Count - 1;
        SwitchMenu(menuList[prevMenu].MenuName);
    }

    public void LastOpenedMenu()
    {
        SwitchMenu(lastOpenedMenu.MenuName);
    }

    public void ClearMenus()
    {
        transitioningMenus.Clear();
    }

    public bool ContainsMenu(string menu)
    {
        if (GetMenuObject(menu) != null) return true;
        else return false;
    }
}
