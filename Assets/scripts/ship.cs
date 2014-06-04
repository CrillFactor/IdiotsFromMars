using UnityEngine;
using System.Collections;

public class ship : MonoBehaviour {

	public float bullet_delay = 1.0F;
	private float shot = 0.0F;
	private float moveInput;
	public float moveSpeed = 4.0F;
	private float xo,yo;
	private Vector2 newPosition;
	public GameObject bullet;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (shot > 0.0F) { shot -= Time.deltaTime; }
		if (shot < 0.0F) { shot = 0.0F; }
		moveInput = Input.GetAxis ("Horizontal");
		if (moveInput > 0) {
			moveInput = -1;
		} else if (moveInput < 0) {
			moveInput = 1;
		}
		if ((moveInput == 1.0F) || (moveInput == -1.0F)) {
			xo = transform.position.x;
			yo = transform.position.y;
			moveInput *= moveSpeed * Time.deltaTime;
			xo -= moveInput;
			if (xo < -8.0F) { xo = -8.0F; }
			if (xo > 8.0F) { xo = 8.0F; }
			transform.position = new Vector2(xo, yo);
		}


		bool fDown = Input.GetButtonDown("Fire1");
        bool fHeld = Input.GetButton("Fire1");
        //bool fUp = Input.GetButtonUp("Fire");
		if (((fDown) || (fHeld)) && (shot == 0.0F)) {
			xo = transform.position.x;
			yo = transform.position.y;
			Instantiate (bullet, new Vector2(xo,yo+0.5F),transform.rotation);
			shot = bullet_delay;
//			Debug.Log("Fired");

		}
	}
	void OnCollisionEnter2D(Collision2D hit) {
//		Debug.Log("Hit A Thing");
		if (hit.gameObject.CompareTag("amissile")) {
		//if (hit.gameObject.tag == "pmissile") {
			// StartThing.aliens_remaining --;
			//		blueThis.GetComponents<DistanceJoint2D>()[0].connectedBody = eye1This.rigidbody2D;
			//		blueThis.GetComponents<DistanceJoint2D>()[1].connectedBody = eye2This.rigidbody2D;
			Destroy(hit.gameObject);
			Destroy(gameObject);
			StartThing.game_running = -1;
		}
	}

}
