using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Destroyer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			SceneManager.LoadScene("game-over");
		} else {
			if (other.gameObject.transform.parent) {
				Destroy (other.gameObject.transform.parent);
			} else {
				Destroy (other.gameObject);
			}
		}
	}
}
