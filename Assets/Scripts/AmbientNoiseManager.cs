using UnityEngine;

public class AmbientNoiseManager : AmbientAudio {
    public GameObject player;
    public AudioClip musicClip;
    public int musicStartTime;
    public float musicMinInterval, musicMaxInterval;

    float nextMusicPlay;

    // Update is called once per frame
    override public void Update () {
        base.Update();
        if (Time.time < musicStartTime) return;

        if (Time.time >= nextMusicPlay) {
            player.GetComponent<AudioSource>().PlayOneShot(musicClip);
            nextMusicPlay = Time.time + (float)RandomManager.random.NextDouble() * (musicMaxInterval - musicMaxInterval) + musicMinInterval;
        }
	}
}
