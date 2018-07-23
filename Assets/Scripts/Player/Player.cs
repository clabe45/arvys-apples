using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    [HideInInspector]
    public bool inWater;

    public void Win() {
        SceneManager.LoadScene("Win");
    }

    public void Lose() {
        Time.timeScale = 0; // just in case it lags, remove if unnecessary
        GetComponent<PlayerHealth>().WriteStats();
        SceneManager.LoadScene("Lose");
    }
}
