using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainMenu : MonoBehaviour {
    bool startedPressingAnyKey;

    void Start() {
        startedPressingAnyKey = Input.anyKey;
    }

    void Update() {
        // prevent immediate scene change, before user lets go of all keys
        if (startedPressingAnyKey && Input.anyKey) return;
        if (Input.anyKeyDown) SceneManager.LoadScene("MainMenu");
	}
}
