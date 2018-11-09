using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCredits : MonoBehaviour {

    private void OnEnable()
    {
        var items = GetComponentsInChildren<MenuSystemItem>();
        foreach (MenuSystemItem item in items)
        {
            item.parentManager = transform.root.GetComponent<MenuSystemManager>();
        }
    }

}
