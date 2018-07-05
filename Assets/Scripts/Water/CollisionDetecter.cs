using UnityEngine;

public class CollisionDetecter : MonoBehaviour {
    public void OnTriggerEnter(Collider other) {
        if (other.tag == "RootPlayer") {
            Transform playerCharacter = other.transform.GetChild(0).GetChild(0);   // custom player child (with my scripts)
            playerCharacter.GetComponent<PlayerHealth>().inWater = true;
        }
    }
    public void OnTriggerExit(Collider other) {
        if (other.tag == "RootPlayer") {
            Transform playerCharacter = other.transform.GetChild(0).GetChild(0);   // custom player child (with my scripts)
            playerCharacter.GetComponent<PlayerHealth>().inWater = false;
        }
    }
}
