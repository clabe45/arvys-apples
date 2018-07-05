using UnityEngine;

/// <summary>
/// Fills placeholder text with real stats
/// </summary>
public class TextStatsFiller : MonoBehaviour {
	void Start () {
        UnityEngine.UI.Text t = GetComponent<UnityEngine.UI.Text>();
        t.text = PlayerStats.Fill(t.text);
	}
}
