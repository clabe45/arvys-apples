using UnityEngine;

public class HideCursor : MonoBehaviour {
	void Start () {
        if (Cursor.lockState != CursorLockMode.Confined)
            Cursor.lockState = CursorLockMode.Confined; // transition; see http://answers.unity.com/answers/1119750/view.html
        Cursor.lockState = CursorLockMode.Locked;
	}
}
