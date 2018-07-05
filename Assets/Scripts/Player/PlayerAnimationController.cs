using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {
    public GameObject animPlayer;
    public float groundCheckDist;

    private Vector3 prevPos;

	void Update () {
        // measure movement
        Vector3 p1 = transform.position, p2 = prevPos;

        float speed = Mathf.Sqrt((p1.x - p2.x) * (p1.x - p2.x) + (p1.z - p2.z) * (p1.z - p2.z)) / Time.deltaTime;    // XZ distance
        animPlayer.GetComponent<Animator>().SetBool("Walking", speed > 0);
        animPlayer.GetComponent<Animator>().SetBool("Running", speed > 0 && Input.GetKey(Settings.Mutable.RUN_KEY));

        prevPos = transform.position;

        // raycast to detect if grounded
        RaycastHit hit;
        int layerMask = ~(1 << 2);  // exclude "Ignore Raycast" (itself - player)
        bool grounded = Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDist, layerMask);
        animPlayer.GetComponent<Animator>().SetBool("Grounded", grounded);
	}
}
