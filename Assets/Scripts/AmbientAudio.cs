using UnityEngine;

public class AmbientAudio : MonoBehaviour {
    public AudioClip[] audioData;
    public int audioRarity;

    virtual public void Update() {
        if (!GetComponent<AudioSource>().isPlaying && RandomManager.random.Next(0, audioRarity) == 0) {
            GetComponent<AudioSource>().PlayOneShot(audioData[RandomManager.random.Next(0, audioData.Length)]);
        }
    }
}
