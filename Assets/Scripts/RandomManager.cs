using UnityEngine;

/// <summary>
/// A global RNG to prevent duplicates in instant in time
/// </summary>
public class RandomManager : MonoBehaviour {
    public static System.Random random = new System.Random();
}
