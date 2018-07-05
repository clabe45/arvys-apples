﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public void Lose() {
        Time.timeScale = 0; // just in case it lags, remove if unnecessary
        GetComponent<PlayerHealth>().WriteStats();
        SceneManager.LoadScene("Lose");
    }
}
