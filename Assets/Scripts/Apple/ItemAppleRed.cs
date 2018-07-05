using UnityEngine;

public class ItemAppleRed : ItemApple {
    public int fullness;
    public float poisonChance;
    bool Poisoned;

    public void Start() {
        Poisoned = RandomManager.random.NextDouble() < poisonChance;
    }

    public override void Use(GameObject player) {
        if (Poisoned) {
            player.GetComponent<PlayerHealth>().Poison();
        }
        else {
            player.GetComponent<PlayerHealth>().Hunger += fullness;
        }
        Destroy(gameObject);    // destroy apple
    }
}
