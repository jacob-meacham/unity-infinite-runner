using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
	int score = 0;
	// Use this for initialization
	void Start () {
		score = PlayerPrefs.GetInt ("score");
	}

	void OnGUI() {
		GUI.Label (new Rect (Screen.width / 2 - 40, 50, 80, 30), "GAME OVER");
		GUI.Label (new Rect (Screen.width / 2 - 40, 300, 200, 30), "You killed " + score + " children");

		if (GUI.Button (new Rect (Screen.width / 2 - 30, 350, 80, 30), "Retry?")) {
			SceneManager.LoadScene (0);
		}
	}
}
