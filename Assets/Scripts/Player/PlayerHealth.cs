using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    public float hungerDecrementTime, thirstUpdateTime, poisonDuration, poisonDamageTime;
    public float riverQuenchAmount;
    public Texture healthColor, healthBorderColor, healthPoisonedColor,
        hungerColor, hungerBorderColor, thirstColor, thirstBorderColor;
    public int barWidth, barHeight, margin;
    public GameObject rootPlayer;

    float health_;
    public float Health {
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

    float hunger_;
    public float Hunger {
        get {
            return hunger_;
        }
        set {
            if (value > 100) value = 100;
            if (value < 0) GetComponent<Player>().Lose();
            hunger_ = value;
        }
    }

    float thirst_;
    public float Thirst {
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
        bool runKey = Input.GetKey(Settings.Mutable.RUN_KEY);
        bool moving = rootPlayer.GetComponent<Rigidbody>().velocity.sqrMagnitude > 0;
        if (Time.time >= lastHungerDecrement + hungerDecrementTime) {
            float loss = (runKey && moving) ? 2 : moving ? 0.5f : 1;
            Hunger -= loss;
            lastHungerDecrement = Time.time;
        }
    }
    /// <summary>
    /// Combine effects of increasing and decreasing thirst (based on whether player's in river)
    /// </summary>
    private void UpdateThirst() {
        bool runKey = Input.GetKey(Settings.Mutable.RUN_KEY);
        bool moving = rootPlayer.GetComponent<Rigidbody>().velocity.sqrMagnitude > 0;
        if (Time.time >= lastThirstUpdate + thirstUpdateTime) {
            float gain = inWater ? riverQuenchAmount : 0;
            float loss = (runKey && moving) ? 2 : moving ? 0.5f : 1;
            float delta = gain - loss;
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

        DrawBar(Mathf.FloorToInt(Health), healthColor, Poisoned ? healthPoisonedColor : healthBorderColor, y);   // outline with poison color, if poisoned

        y -= margin + barHeight;
        DrawBar(Mathf.FloorToInt(Thirst), thirstColor, thirstBorderColor, y);

        y -= margin + barHeight;
        DrawBar(Mathf.FloorToInt(Hunger), hungerColor, hungerBorderColor, y);
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
            Mathf.FloorToInt(Health),
            Mathf.FloorToInt(Hunger),
            Mathf.FloorToInt(Thirst));
    }
}
