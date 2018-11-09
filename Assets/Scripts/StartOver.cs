using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartOver : MonoBehaviour {

    public void resetAndStartover() {
        PlayerPrefs.DeleteKey("NumberCompleted");
        PlayerPrefs.Save();
        PuzzleManager.numberCompleted = 0;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
