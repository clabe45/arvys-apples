using UnityEngine;

public class Clicker : MonoBehaviour {
    static Vector3 center = new Vector3(.5f, .5f, 0);

    public float hitRange;
    
    public void Update () {
        Vector3 rayOrigin = transform.GetComponentInParent<Camera>().ViewportToWorldPoint(center);
		if (Input.GetButtonDown("Fire1")) {
            RaycastHit click;
            int layerMask = ~(1 << 2);  // exclude "Ignore Raycast"
            if (Physics.Raycast(rayOrigin, transform.forward, out click, hitRange, layerMask) 
                && click.transform.GetComponent<Item>()) {
                GetComponent<Inventory>().AddItem(click.transform.gameObject);
            }
        }
	}
}
