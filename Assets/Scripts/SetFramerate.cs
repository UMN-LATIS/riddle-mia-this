using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFramerate : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;
    }

}
