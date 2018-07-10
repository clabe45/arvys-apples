using UnityEngine;

public class Settings : MonoBehaviour {
    public static class Immutable {}
    public static class Mutable {
        public static KeyCode RUN_KEY = KeyCode.LeftShift,
            PAUSE_KEY = KeyCode.Escape;
    }
}
