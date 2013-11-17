using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {


	public Vector2 Speed;
	// Use this for initialization
	void Start () {
		Speed = new Vector2 (5, 5);
	}
	
	// Update is called once per frame
	void Update () {
		// 1 - Retrieve axis information
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		
		// 2 - Movement per direction
		/*Vector3 movement = new Vector3(
			Speed.x * inputX,
			Speed.y * inputY,0);

		movement *= Time.deltaTime;
		transform.Translate(movement);*/
		Vector2 movement = new Vector2(
			Speed.x *10* inputX,
			Speed.y *10* inputY);
		rigidbody2D.AddForce (movement);

	}
}
