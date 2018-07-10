using UnityEngine;

public class PlayerStats : MonoBehaviour {
    private static int health, hunger, thirst;

    public static void Set(int health, int hunger, int thirst) {
        PlayerStats.health = health;
        PlayerStats.hunger = hunger;
        PlayerStats.thirst = thirst;
    }

    public static string Fill(string placeholderText) {
        return placeholderText
            .Replace("%h", health.ToString())
            .Replace("%g", hunger.ToString())
            .Replace("%s", thirst.ToString());
    }
}
