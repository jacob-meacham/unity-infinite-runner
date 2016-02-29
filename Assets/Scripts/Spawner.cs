using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public GameObject[] objs;
	public float spawnMin = 1f;
	public float spawnMax = 2f;
	public float yMin = 0f;
	public float yMax = 10f;

	// Use this for initialization
	void Start () {
		Spawn ();
	}

	void Spawn() {
		Vector3 newPosition = transform.position;
		newPosition.y += Random.Range (-5, 5) * 0.2f;
		newPosition.y = Mathf.Clamp (newPosition.y, yMin, yMax);
		Instantiate (objs [Random.Range (0, objs.Length)], newPosition, Quaternion.identity);
		Invoke ("Spawn", Random.Range (spawnMin, spawnMax));
	}
}
