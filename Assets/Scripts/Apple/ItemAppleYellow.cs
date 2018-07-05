using UnityEngine;

public class ItemAppleYellow : ItemApple {
    public int health;

    public override void Use(GameObject player) {
        PlayerHealth healthComponent = player.GetComponent<PlayerHealth>();
        healthComponent.Health += health;
        if (healthComponent.Poisoned) healthComponent.Heal();

        Destroy(gameObject);
    }
}
