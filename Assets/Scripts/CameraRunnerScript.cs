using UnityEngine;
using System.Collections;

public class CameraRunnerScript : MonoBehaviour {
	public Color colorBegin;
	public Color colorEnd;
	public Transform player;
	public Camera camera;
	
	// Update is called once per frame
	void Update () {
		Vector3 newPosition = new Vector3 (player.position.x + 6, player.position.y-1, -10);
		if (newPosition.y < -2) {
			newPosition.y = -2;
		}
		transform.position = newPosition;

		camera.backgroundColor = Color.Lerp(colorBegin, colorEnd, player.position.x / 1000f);
	}
}
