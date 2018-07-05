using UnityEngine;

/// <summary>
/// Signifies that its owning GameObject is an item, along with holding item-specific data
/// </summary>
public class Item : MonoBehaviour {
    public Texture icon;
    public bool inInventory;

    public virtual void Use(GameObject user) { }
}
