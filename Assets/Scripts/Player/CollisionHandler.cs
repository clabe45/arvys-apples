using UnityEngine;

public class CollisionHandler : MonoBehaviour {
    public GameObject playerChar;
    public int caterpillarDamage;   // agh I wish this could be in a caterpillar class, but I have to use 
                                    // character controller's special collision detection system
    public float caterpillarDamageTime;
    public AudioClip caterpillarDamageSound;

    float lastCaterpillarDamageTime;

    public void OnCollisionStay(Collision collision) {
        if (collision.gameObject.tag == "Caterpillar" && Time.time >= lastCaterpillarDamageTime + caterpillarDamageTime) {
            playerChar.GetComponent<PlayerHealth>().Health -= caterpillarDamage;
            collision.transform.GetComponent<AudioSource>().PlayOneShot(caterpillarDamageSound);
            lastCaterpillarDamageTime = Time.time;
        }
    }
}
