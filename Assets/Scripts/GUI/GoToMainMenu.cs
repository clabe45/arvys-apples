using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainMenu : MonoBehaviour {
    void Update () {
        // prevent immediate scene change, before user realizes that he/she won or lost
        if (Time.timeSinceLevelLoad > 0.5 && Input.anyKeyDown) SceneManager.LoadScene("MainMenu");
	}
}
