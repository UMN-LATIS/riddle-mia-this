using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

//Author: Charles McGregor
//Last Edit: 4/28/2015
//Description: Allows buttons and other selectables to initiate menu transitions
public class MenuSystemItem : MonoBehaviour, ISubmitHandler, IPointerClickHandler {

	//Variables
	public string targetMenu = ""; //Target menu 
	public bool keepMenuActive = false; //Keep current menu active
	public bool autoAssignManager = true; //Looks in parents to find MenuSystemManger
	public bool enableMouseClick = true; //Allows the mouse to switch the menu

	public MenuSystemManager parentManager; //Target MenuSystemManager to invoke

	void Start()
	{
		if(autoAssignManager) parentManager = GetComponentInParent<MenuSystemManager>();
	}

	//Switches to target menu
	public void OnSubmit (BaseEventData eventData)
	{
		SwitchMenu();
	}
	
	public void OnPointerClick (PointerEventData eventData)
	{
		if(enableMouseClick) SwitchMenu();
	}

	public void SwitchMenu()
	{
		if(targetMenu != "") parentManager.SwitchMenu(targetMenu,keepMenuActive);
	}
}
