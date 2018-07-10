using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour {
    public int interpolationEndTime;
    public GameObject lightObject;
    public float maxFog, minLight;

    void Update() {
        IncreaseFog();
    }

    private void IncreaseFog() {
        float progress = Time.timeSinceLevelLoad / interpolationEndTime; // percentage
        RenderSettings.fogDensity = CosineInterpolate(0, maxFog, progress); // fog starts at 0%
    }

    private void DecreaseLight() {
        float progress = Time.timeSinceLevelLoad / interpolationEndTime; // percentage
        lightObject.GetComponent<Light>().intensity = CosineInterpolate(1, minLight, progress);  // light starts at 100%
    }

    private float CosineInterpolate(float y1, float y2, float t) {
        float mu2 = (1 - (float)System.Math.Cos(t * System.Math.PI)) / 2;
        return y1 * (1 - mu2) + y2 * mu2;
    }
}
