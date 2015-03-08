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
