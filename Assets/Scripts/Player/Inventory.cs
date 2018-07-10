using UnityEngine;

public class Inventory : MonoBehaviour {

    public float guiBoxSize, guiBoxMargin, guiBoxTexSize, scrollDeltaPerSlotChange, dropItemForce;
    public Texture slotTexture, slotSelectedTexture;
    public Transform rootPlayer;
    /// <summary>
    /// The right hand bone
    /// </summary>
    public Transform hand;

    InventoryData inventory;
    public void Start() {
        inventory = new InventoryData(5, this);
    }

    public void Update() {
        CheckChangeCurrentSlot();
        CheckUseItem();
    }
    
    // involves physics
    public void FixedUpdate() {
        CheckDropItem();
    }

    private void CheckChangeCurrentSlot() {
        // by scrolling
        float wheel = Input.GetAxis("Mouse ScrollWheel");
        // down -> right
        if (wheel < 0) {
            inventory.currentSlot = (inventory.currentSlot + 1) % inventory.Length;
        // up -> left
        } else if (wheel > 0) {
            inventory.currentSlot = posmod((inventory.currentSlot - 1), inventory.Length);
        }

        // by number key
        if (Input.GetKeyDown(KeyCode.Alpha1)) inventory.currentSlot = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) inventory.currentSlot = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) inventory.currentSlot = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4)) inventory.currentSlot = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5)) inventory.currentSlot = 4;
    }

    private void CheckDropItem() {
        if (Input.GetKeyDown(KeyCode.E) && inventory[inventory.currentSlot]) {
            RemoveCurrentItem();
        }
    }

    private void CheckUseItem() {
        // RMB
        if (Input.GetMouseButtonDown(1) && inventory[inventory.currentSlot] 
            && inventory[inventory.currentSlot].GetComponent<Item>()) {
            inventory[inventory.currentSlot].GetComponent<Item>().Use(gameObject);
        }
    }

    /// <summary>
    /// Performs modulo, always returning a positive number (like math modulo, I think)
    /// </summary>
    /// <param name="x"></param>
    /// <param name="m"></param>
    /// <returns></returns>
    private int posmod(int x, int m) {
        return (x % m + m) % m;
    }

    public void LateUpdate() {
        inventory.LateUpdate();
    }

    public void OnGUI () {
		for (int i=0; i<inventory.Length; i++) { 
            Rect outside = new Rect(
                Screen.width - (guiBoxMargin + guiBoxSize) * /*backwards*/(inventory.Length-i), //backwards
                Screen.height - guiBoxMargin - guiBoxSize, 
                guiBoxSize, 
                guiBoxSize);
            GUI.DrawTexture(outside, i == inventory.currentSlot ? slotSelectedTexture : slotTexture);
            if (inventory[i]) {
                float rat = guiBoxTexSize / guiBoxSize; // from box size to box tex size
                float off = (guiBoxSize - guiBoxTexSize) / 2;   // center
                Rect inside = new Rect(outside.x + off, outside.y + off, rat * outside.width, rat * outside.height);
                Texture itemTexture = inventory[i].GetComponent<Item>().icon;
                GUI.DrawTexture(inside, itemTexture);
            }
        }
	}

    /// <summary>
    /// Adds the item to the first free slot or replaces the current one
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(GameObject item) {
        if (!item.GetComponent<Item>()) throw new System.ArgumentException("GameObject is not an item!");
        int slot = 0;
        while (slot<inventory.Length && inventory[slot]) slot++;
        if (slot == inventory.Length) return;
        inventory[slot] = item;
    }

    public void RemoveItem(int slot) {
        // restart die timer, if possible
        if (inventory[slot].GetComponent<Lifetime>())
            inventory[slot].GetComponent<Lifetime>().born = Time.time;
        inventory[slot] = null;
    }

    public void RemoveCurrentItem() {
        RemoveItem(inventory.currentSlot);
    }

    public GameObject GetItem(int slot) {
        return inventory[slot];
    }

    public GameObject GetCurrentItem() {
        return GetItem(inventory.currentSlot);
    }

    public bool IsFull() {
        for (int i=0; i<inventory.Length; i++) {
            if (!inventory[i]) return false;
        }
        return true;
    }

    public bool isEmpty() {
        for (int i = 0; i < inventory.Length; i++) {
            if (inventory[i]) return false;
        }
        return true;
    }
}

public class InventoryData {
    Inventory parent;
    GameObject[] items;
    int currentSlotBack;
    public int currentSlot {
        get { return currentSlotBack; }
        set {
            if (items[currentSlot]) {
                UnholdItem(items[currentSlot]);
            }
            currentSlotBack = value;
            if (items[currentSlot]) HoldItem(items[currentSlot]);
        }
    }

    public int Length {
        get { return items.Length; }
    }

    public GameObject this[int i] {
        get { return items[i]; }
        set {
            if (!value && i != currentSlot) throw new System.InvalidOperationException("Cannot drop item that's not in hand");
            GameObject itemBefore = items[i];
            items[i] = value;
            if (itemBefore) UnholdItem(itemBefore);
            if (value) AddItem(value, i == currentSlot);  // still, value == items[i]
            // after to prevent clashes with setting transform and applying force, I think so
            bool dropping = itemBefore && !value;
            if (dropping && itemBefore.GetComponent<Rigidbody>()) RemoveItem(itemBefore);
        }
    }

    public InventoryData(int numbSlots, Inventory parent) {
        items = new GameObject[numbSlots];
        this.parent = parent;
    }

    public void LateUpdate() {
        UpdateCurrentItem();
    }

    /// <summary>
    /// Puts the item in the players inventory
    /// </summary>
    /// <param name="item"></param>
    /// <param name="inCurrent"></param>
    private void AddItem(GameObject item, bool inCurrent) {
        if (item.GetComponent<Rigidbody>()) item.GetComponent<Rigidbody>().isKinematic = true; //assuming no items are by default kinematic
        if (item.GetComponent<Collider>()) item.GetComponent<Collider>().isTrigger = true;

        if (!inCurrent) UnholdItem(item);   // technically not _un_holding it, but you get the idea
        else HoldItem(item);
        item.GetComponent<Item>().inInventory = true;
    }

    /// <summary>
    /// Puts the item in the player's hand
    /// </summary>
    /// <param name="item"></param>
    private void HoldItem(GameObject item) {
        item.SetActive(true);
    }

    // must be called every frame, but after HoldItem when item is switched, somehow
    private void UpdateCurrentItem() {
        GameObject ci = items[currentSlot];
        if (!ci) return;
        Transform hand = parent.hand; // shorthand, no pun intended
        Vector3 offset = new Vector3(0, 0, 0.3f);    // from hand to item
        Vector3 handEnd = hand.TransformPoint(Vector3.left + offset);
        ci.transform.position = handEnd; // just happens to be left because of all the transformations
        ci.transform.rotation = parent.rootPlayer.transform.rotation;   // only include rotation around y-axis
        // should be no scale settings
    }

    /// <summary>
    /// Puts the item back into the "real world"
    /// </summary>
    /// <param name="item"></param>
    private void RemoveItem(GameObject item) {
        item.SetActive(true);
        item.transform.parent = GameObject.FindGameObjectWithTag("Dynamic").transform;
        //effectively disable physics
        if (item.GetComponent<Rigidbody>()) {
            Rigidbody rigidbody = item.GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            rigidbody.AddForce(parent.transform.forward * parent.dropItemForce, ForceMode.Impulse);
        }
        // don't set isTrigger to false here, set it after it leaves the player's capsule (in Item#Update)
        item.GetComponent<Item>().inInventory = false;
    }
    
    /// <summary>
    /// Removes the item from the player's hand
    /// </summary>
    /// <param name="item"></param>
    private void UnholdItem(GameObject item) {
        item.SetActive(false);
    }
}