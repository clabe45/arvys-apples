using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public float hungerDecrementTime, thirstUpdateTime, poisonDuration, poisonDamageTime;
    public int riverQuenchAmount;
    public Texture healthColor, healthBorderColor, healthPoisonedColor,
        hungerColor, hungerBorderColor, thirstColor, thirstBorderColor;
    public int barWidth, barHeight, margin;

    int health_;
    public int Health {
        get {
            return health_;
        }
        set {
            // validate here
            if (value > 100) value = 100;
            if (value < 0) GetComponent<Player>().Lose();
            health_ = value;
        }
    }

    int hunger_;
    public int Hunger {
        get {
            return hunger_;
        }
        set {
            if (value > 100) value = 100;
            if (value < 0) GetComponent<Player>().Lose();
            hunger_ = value;
        }
    }

    int thirst_;
    public int Thirst {
        get {
            return thirst_;
        }
        set {
            if (value > 100) value = 100;
            if (value < 0) GetComponent<Player>().Lose();
            thirst_ = value;
        }
    }

    public bool Poisoned { get; private set; }
    private float lastPoisoned, nextPoisonDamage;

    [HideInInspector]
    public bool inWater;

    float lastHungerDecrement, lastThirstUpdate;

    public void Start() {
        Health = 100;
        Hunger = 100;
        Thirst = 100;
    }

    public void Update() {
        DecrementHunger();
        UpdateThirst();
        UpdatePoison();
    }
    private void DecrementHunger() {
        bool running = Input.GetKey(Settings.Mutable.RUN_KEY);
        if (Time.time >= lastHungerDecrement + hungerDecrementTime) {
            int loss = running ? 2 : 1;
            Hunger -= loss;
            lastHungerDecrement = Time.time;
        }
    }
    /// <summary>
    /// Combine effects of increasing and decreasing thirst (based on whether player's in river)
    /// </summary>
    private void UpdateThirst() {
        bool running = Input.GetKey(Settings.Mutable.RUN_KEY);
        if (Time.time >= lastThirstUpdate + thirstUpdateTime) {
            int gain = inWater ? riverQuenchAmount : 0;
            int loss = running ? 2 : 1;
            int delta = gain - loss;
            Thirst += delta;
            lastThirstUpdate = Time.time;
        }
    }
    private void UpdatePoison() {
        if (Time.time >= lastPoisoned + poisonDuration && Poisoned) Poisoned = false;
        if (Poisoned && Time.time >= nextPoisonDamage) {
            Health--;
            nextPoisonDamage = Time.time + poisonDamageTime;
        }
    }

    public void OnGUI() {
        // LAYOUT:
        // hunger
        // thirst
        // health

        // work from bottom to top
        int y = Screen.height;
        y -= margin + barHeight;

        DrawBar(Health, healthColor, Poisoned ? healthPoisonedColor : healthBorderColor, y);   // outline with poison color, if poisoned

        y -= margin + barHeight;
        DrawBar(Thirst, thirstColor, thirstBorderColor, y);

        y -= margin + barHeight;
        DrawBar(Hunger, hungerColor, hungerBorderColor, y);
    }

    public void Poison() {
        Poisoned = true;
        lastPoisoned = Time.time;
        nextPoisonDamage = Time.time + poisonDamageTime;
    }

    public void Heal() {
        Poisoned = false;
    }

    private void DrawBar(int value, Texture color, Texture borderColor, int y) {
        // border
        GUI.DrawTexture(new Rect(margin, y, barWidth, 1), borderColor); // top
        GUI.DrawTexture(new Rect(margin, y + barHeight - 1, barWidth, 1), borderColor); // bottom
        GUI.DrawTexture(new Rect(margin, y + 1, 1, barHeight - 2), borderColor);    // left
        GUI.DrawTexture(new Rect(margin + barWidth - 1, y + 1, 1, barHeight - 2), borderColor); // right

        // filled
        GUI.DrawTexture(new Rect(margin + 1, y + 1, barWidth * (value / 100f) - 2, barHeight - 2), color);
    }

    public void WriteStats() {
        PlayerStats.Set(
            Mathf.FloorToInt(Time.timeSinceLevelLoad),
            Health,
            Hunger,
            Thirst);
    }
}
