using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleARDecision : MonoBehaviour {

    public Animator anim;

	void Update ()
    {
        if ((GetComponent<Renderer>() != null && GetComponent<Renderer>().enabled) || (GetComponent<Canvas>() != null && GetComponent<Canvas>().enabled))
        {
            anim.SetBool("isActive", true);
        }
    }
}
