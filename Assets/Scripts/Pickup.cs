using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
	public int powerupAmount = 100;
	HUD hud;

	void Start() {
		hud = GameObject.Find("Main Camera").GetComponent<HUD> ();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			hud.Score (powerupAmount);
			Destroy (this.gameObject);
		}
	}
}
