using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	public int pickupScore = 0;
	public Transform player;
	public int curScore = 0;

	public void Score(int amount) {
		pickupScore += amount;
	}

	void OnDisable() {
		PlayerPrefs.SetInt ("score", curScore);
	}

	void Update() {
		curScore = (int)(pickupScore + player.position.x);
	}

	void OnGUI() {
		GUI.Label (new Rect (10, 10, 100, 30), "Score: " + curScore); 
	}
}
