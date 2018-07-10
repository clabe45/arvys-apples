using UnityEngine;

// TODO: implement actual (but basic) pathfinding
public class CaterpillarAI : MonoBehaviour {
    public float idleSpeed, speed, rotateIncrement, obstacleRadius, searchRadius, idleZSpin;
    /// <summary>
    /// Overrides default gravity <see cref="DecreaseGravity"/>; of course it should be small because we want our caterpillars
    /// to fly!
    /// </summary>
    public Vector3 gravity;

    private GameObject player/*, river*/;   // can only be populated dynamically (because prefabs and dynamic generation)
    private Quaternion idleRotation;
    private float sqrSearchRadius;

    private enum State {
        HUNTING, IDLE
    }
    private State? state = null;

    public void Start() {
        sqrSearchRadius = searchRadius * searchRadius;

        GetComponent<Rigidbody>().velocity = idleSpeed * transform.forward;
        // can only be populated dynamically (because prefabs and dynamic generation)
        player = GameObject.FindGameObjectWithTag("RootPlayer");
        //river = GameObject.FindGameObjectWithTag("River");
        idleRotation = Quaternion.Euler(0, 0, idleZSpin);
    }

    public void Update() {
        State newState = (player.transform.position - transform.position).sqrMagnitude <= sqrSearchRadius
            ? State.HUNTING
                : State.IDLE;
        // change state / transition code
        if (state != newState) {
            switch (newState) {
                case State.HUNTING: {
                        StartHunting();
                        break;
                    }
                case State.IDLE: {
                        StopHunting();
                        break;
                    }
            }
            state = newState;
        }
    }

    public void FixedUpdate() {
        if (Input.GetKey(KeyCode.LeftControl)) return;
        // run state / update code
        switch (state) {
            case State.HUNTING: {
                    HuntPlayer();
                    break;
                }
            case State.IDLE: {
                    Spin();
                    break;
                }
        }

        DecreaseGravity();
	}

    private void DecreaseGravity() {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce((gravity - Physics.gravity) * rigidbody.mass);
    }

    private void StartHunting() {
        GetComponent<Animator>().SetBool("Crawling", true);
        state = State.HUNTING;
    }

    /*
     * Pathfinding in this game is very simple: if there's no obstacle in the immediate way of the player, go towards him.
     * Otherwise, go into an idle state.
     * Obstalces are defined as:
     *  - Anything physically in the way of the path right in front of it
     *  - The river
     */

    private void HuntPlayer() {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        // my own small "rotate towards" algo (for forward direction)
        Quaternion targetRotation = Quaternion.LookRotation((player.transform.position - transform.position).normalized);
        rigidbody.rotation = Quaternion.RotateTowards(rigidbody.rotation, targetRotation, rotateIncrement);
        rigidbody.velocity = speed * transform.forward; // move forwards
    }

    private void StopHunting() {
        GetComponent<Animator>().SetBool("Crawling", false);
        state = State.IDLE;
    }

    private void Spin() {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.rotation *= idleRotation;  // combine current rotation with "offset" rotation
    }
}
