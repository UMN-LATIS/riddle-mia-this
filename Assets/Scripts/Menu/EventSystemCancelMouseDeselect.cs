using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class EventSystemCancelMouseDeselect : MonoBehaviour {

	private GameObject previousGameObject;

	void Update()
	{
		CheckSelection();
		if(Input.GetMouseButtonUp(0))
		{
			ExecuteEvents.Execute<ISelectHandler>(previousGameObject, new BaseEventData(EventSystem.current), ExecuteEvents.selectHandler);
			EventSystem.current.SetSelectedGameObject(previousGameObject);
		}
	}

	void CheckSelection()
	{
		if(EventSystem.current.currentSelectedGameObject != null) previousGameObject = EventSystem.current.currentSelectedGameObject;
	}

}
