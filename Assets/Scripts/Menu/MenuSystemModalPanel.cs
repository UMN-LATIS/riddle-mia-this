using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;


//Author: Charles McGregor
//Last Edit: 4/28/2015
//Description: WIP Manager of a modal menu (Ex: "Are you sure?" "Yes" "No")
public class MenuSystemModalPanel : MonoBehaviour {

	public Text prompt;
	public Button confirmButton;
	public Button cancelButton;

	public static MenuSystemModalPanel modalPanel;

	public static MenuSystemModalPanel Instance()
	{
		if(!modalPanel)
		{
			modalPanel = FindObjectOfType(typeof(MenuSystemModalPanel)) as MenuSystemModalPanel;
			if(!modalPanel) Debug.LogError("There needs to be one active ModalPanel script on a GameObject in your scene.");
		}

		return modalPanel;
	}

	public void Choice(string prompt, UnityAction confirmEvent, UnityAction cancelEvent)
	{
		confirmButton.onClick.RemoveAllListeners();
		confirmButton.onClick.AddListener(confirmEvent);
	}

	void ClosePanel()
	{

	}
}
