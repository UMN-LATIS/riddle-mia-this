using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartScript : MonoBehaviour {

    public GameObject TapToStartGroup;
    public GameObject ResumeGroupPortrait;
    public GameObject ResumeGroupLandScape;
    public bool ignoreStateChange = false; 

    // Use this for initialization
    void Start()
    {

        checkOrientationAndReveal();
    }

    public void restartGame()
    {
        ignoreStateChange = true;
        PlayerPrefs.DeleteKey("NumberCompleted");
    }

    // Update is called once per frame
    void Update()
    {
        checkOrientationAndReveal();
    }

    void checkOrientationAndReveal() {
        if(ignoreStateChange) {
            return;
        }
        if (PlayerPrefs.HasKey("NumberCompleted"))
        {
            TapToStartGroup.SetActive(false);
            if (Screen.orientation == ScreenOrientation.Landscape)
            {
                ResumeGroupLandScape.SetActive(true);
                ResumeGroupPortrait.SetActive(false);
            }
            else
            {
                ResumeGroupPortrait.SetActive(true);
                ResumeGroupLandScape.SetActive(false);
            }
        }
        else
        {
            TapToStartGroup.SetActive(true);
            ResumeGroupPortrait.SetActive(false);
            ResumeGroupLandScape.SetActive(false);
        }
    }
}
