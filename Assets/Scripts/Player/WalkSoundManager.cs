using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

// wait for Danny
public class WalkSoundManager : MonoBehaviour {
    /*private enum Material {
        GRASS, ROCK, WOOD, SAND, WATER
    }

    public AudioClip grassStep, rockStep, waterStep;
    public int maxRaycastDistance;
    // let's assume terrain is located at (0, ~, 0) where ~ is anything
    public float terrainWidth, terrainDepth;
    public Texture2D terrainTexture;
    public Color grassColor, sandColor;
    public Transform rootPlayer;

    Material lastMaterial;

    public void Start() {}

    public void Update() {
        bool moving = rootPlayer.GetComponent<Rigidbody>().velocity.sqrMagnitude > 0;
        if (moving) {
            Material currMaterial = GetCurrentMaterial();
            if (currMaterial != lastMaterial) {
                rootPlayer.GetComponent<FirstPersonController>().
            }
        }
    }

    private Material GetCurrentMaterial() {
        GameObject objectUnder = GetObjectUnderPlayer();
        switch (objectUnder.tag) {
            case "Rock": return Material.ROCK;
            case "Crate": return Material.WOOD;
            case "River": return Material.WATER;
            case "Terrain": {
                    Vector2 texel = WorldToTexel(rootPlayer.transform.position);
                    Color sample = terrainTexture.GetPixel((int) texel.x, (int) texel.y);
                    if (sample.Equals(grassColor)) return Material.GRASS;
                    if (sample.Equals(sandColor)) return Material.SAND;
                    throw new System.Exception("a new color has been added to the terrain");
                }
            default: {
                    throw new System.Exception("a new material has been added to the game");
                }
        }
    }

    private GameObject GetObjectUnderPlayer() {
        int layerMask = ~(1 << 2);  // exclude "Ignore Raycast"
        RaycastHit hit;
        if (Physics.Raycast(rootPlayer.position, Vector3.down, out hit, maxRaycastDistance, layerMask))
            return hit.transform.gameObject;
        throw new System.Exception("There should always be _something_ underneath the player");
    }

    /// <summary>
    /// Converts a world positoin to the corresponding texel of <code>terrainTexture</code>
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    private Vector2 WorldToTexel(Vector3 world) {
        return new Vector2(
            Mathf.Floor((world.x + terrainWidth / 2) / terrainWidth * terrainTexture.width),
            Mathf.Floor((world.z + terrainDepth / 2) / terrainDepth * terrainTexture.height)
            );
    }*/
}
