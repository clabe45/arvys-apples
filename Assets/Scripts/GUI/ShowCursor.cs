using UnityEngine;

public class ShowCursor : MonoBehaviour {
	void Start () {
        Cursor.visible = true;
        if (Cursor.lockState != CursorLockMode.Confined)
            Cursor.lockState = CursorLockMode.Confined; // transition; see http://answers.unity.com/answers/1119750/view.html
        Cursor.lockState = CursorLockMode.None;
	}
}
