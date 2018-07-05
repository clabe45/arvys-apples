using UnityEngine;

public class ItemAppleGreen : ItemApple {
    public float poisonChance;
    public Transform redApple, yellowApple;

    bool Poisoned;

    public void Start() {
        Poisoned = RandomManager.random.NextDouble() < poisonChance;
    }

    public override void Use(GameObject player) {
        Destroy(gameObject);    // destroy apple
        if (Poisoned) {
            player.GetComponent<PlayerHealth>().Poison();
        }
        else {
            Special(player);
        }
    }

    private void Special(GameObject player) {
        int used = 8, free = 1;
        int r = RandomManager.random.Next(0, used+free);
        if (r < 4) RestoreHealth(player);
        else if (r < 8) DecreaseHealth(player, 5);
    }

    private void RestoreHealth(GameObject player) {
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        health.Health = 100;
        health.Hunger = 100;
        health.Thirst = 100;
        if (health.Poisoned) health.Heal();
    }

    private void DecreaseHealth(GameObject player, int amount=5) {
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        health.Health -= amount;
        health.Hunger -= amount;
        health.Thirst -= amount;
    }
}
