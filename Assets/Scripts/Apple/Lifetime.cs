using UnityEngine;

public class Lifetime : MonoBehaviour {
    public float lifeTime;  // now refers to total life span, instead of time 'til rot
    //public Mesh rottedMesh;
    //public Texture rottedIcon;

    [HideInInspector]
    public float born = 0;
    //bool rotted;

    void Start() {
        born = Time.time;
    }

    void Update () {
        //if (Time.time > born + lifeTime) Rot();
        bool beingHeld = !!transform.GetComponent<Item>().inInventory;
        if (Time.time > born + lifeTime && !beingHeld)  // only die if not being used (like being in inventory)
            Destroy(gameObject);   // die
	}

    private void Rot() {
        //GetComponent<MeshFilter>().mesh = rottedMesh;
        //GetComponent<Item>().icon = rottedIcon;
        //rotted = true;
    }
}
