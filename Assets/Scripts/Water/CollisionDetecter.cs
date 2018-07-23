using UnityEngine;

public class CollisionDetecter : MonoBehaviour {
    public Transform playerCharacter;  // the child with all my custom scripts

    public void OnTriggerEnter(Collider other) {
        if (other.tag == "RootPlayer") {
            playerCharacter.GetComponent<Player>().inWater = true;
        }
    }
    public void OnTriggerExit(Collider other) {
        if (other.tag == "RootPlayer") {
            playerCharacter.GetComponent<Player>().inWater = false;
        }
    }
}
