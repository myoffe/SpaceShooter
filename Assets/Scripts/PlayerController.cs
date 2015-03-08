using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundry {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public float speed;
	public float tilt;
	public Boundry boundry;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate = 0.5f;

	private float nextFire = 0.0f;

	void Update() {
		if (Input.GetButton("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate(shot, transform.position, transform.rotation);
		}
	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		var rigidbody = GetComponent<Rigidbody> ();
		var movement = new Vector3 (moveHorizontal, 0, moveVertical);

		rigidbody.velocity = movement * speed;
		rigidbody.position = new Vector3 (
			Mathf.Clamp (rigidbody.position.x, boundry.xMin, boundry.xMax),
			0,
			Mathf.Clamp (rigidbody.position.z, boundry.zMin, boundry.zMax)
		);

		rigidbody.rotation = Quaternion.Euler(0, 0, rigidbody.velocity.x * -tilt);
	}
}
