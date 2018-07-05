using UnityEngine;

public class PlayerStats : MonoBehaviour {
    private static int time, health, hunger, thirst;

    public static void Set(int time, int health, int hunger, int thirst) {
        PlayerStats.time = time;
        PlayerStats.health = health;
        PlayerStats.hunger = hunger;
        PlayerStats.thirst = thirst;
    }

    public static string Fill(string placeholderText) {
        return placeholderText
            .Replace("%t", time.ToString())
            .Replace("%h", health.ToString())
            .Replace("%g", hunger.ToString())
            .Replace("%s", thirst.ToString());
    }
}
