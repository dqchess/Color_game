using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour {

    public string previous_level;

	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Level load requested for " + name);
            if (previous_level.Length > 0)
            {
                SceneManager.LoadScene(previous_level);
               //Screen.orientation = ScreenOrientation.Portrait;
            }
            else
            {
                Debug.Log("Quitting App");
                Application.Quit();
            }
                
        }
    }
}
