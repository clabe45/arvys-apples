using UnityEngine;

/// <summary>
/// Renders general-purpose GUI in the Game scene
/// </summary>
public class GameGUI : MonoBehaviour {
    public Texture crosshair;

    public void OnGUI() {
        DrawCrosshair();
    }

    private void DrawCrosshair () {
        Rect bounds = new Rect(
            Screen.width / 2 - crosshair.width / 2, Screen.height / 2 - crosshair.height / 2, 
            crosshair.width, crosshair.height
            );
        GUI.DrawTexture(bounds, crosshair);
	}
}
