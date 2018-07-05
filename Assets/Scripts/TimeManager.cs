using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour {
    public int margin;
    public Font font;
    public int winTime;
    public GameObject lightObject;
    public float endFog, endLight;

    void Update() {
        if (Time.timeSinceLevelLoad >= winTime) SceneManager.LoadScene("Win");
        IncreaseFog();
    }

    private void IncreaseFog() {
        float progress = Time.timeSinceLevelLoad / winTime; // percentage
        RenderSettings.fogDensity = CosineInterpolate(0, endFog, progress); // fog starts at 0%
    }

    private void DecreaseLight() {
        float progress = Time.timeSinceLevelLoad / winTime; // percentage
        lightObject.GetComponent<Light>().intensity = CosineInterpolate(1, endLight, progress);  // light starts at 100%
    }

    private float CosineInterpolate(float y1, float y2, float t) {
        float mu2 = (1 - (float)System.Math.Cos(t * System.Math.PI)) / 2;
        return y1 * (1 - mu2) + y2 * mu2;
    }

    void OnGUI () {
        DrawTime();
    }

    private void DrawTime() {
        int totalSeconds = Mathf.FloorToInt(Time.timeSinceLevelLoad);
        int minutes = totalSeconds / 60, seconds = totalSeconds % 60;

        string text = PadLeft(minutes) + ":" + PadLeft(seconds);
        Vector2 textSize = GUI.skin.label.CalcSize(new GUIContent(text));
        GUI.skin.font = font;
        GUI.Label(new Rect(Screen.width - margin - textSize.x, 0 + margin, textSize.x, textSize.y), text);
    }

    private string PadLeft(int x, char padder='0', int length=2) {
        string s = x.ToString();
        while (s.Length < length) s = padder + s;
        return s;
    }
}
