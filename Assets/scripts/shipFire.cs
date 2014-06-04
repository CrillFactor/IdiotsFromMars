using UnityEngine;
using System.Collections;

public class shipFire : MonoBehaviour {
	private float xo, yo;
	public float bulletSpeed = 8.0F;
	public float alienBulletSpeed = -3.0F;
	// Use this for initialization
	void Start () {
		if (gameObject.tag=="pmissile") {
			rigidbody2D.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
		}
		else if (gameObject.tag=="amissile") {
			rigidbody2D.AddForce(transform.up * alienBulletSpeed, ForceMode2D.Impulse);
		}
	}
	
	// Update is called once per frame
	void Update () {
		yo = transform.position.y;
		/*
		xo = transform.position.x;
		yo += bulletSpeed * Time.deltaTime;
		transform.position = new Vector2(xo,yo);
		*/
		if (yo > 4.5F) { Destroy(gameObject); } // off the screen
		if (yo < -4.5F) { Destroy(gameObject); }

	}

	// TO DO - MAKE BULLETS DESTROY EACH OTHER
	
	void OnCollisionEnter2D(Collision2D hit) {
		//Debug.Log(hit.gameObject.tag);
		if ((gameObject.tag=="pmissile") && (hit.gameObject.CompareTag("amissile"))) {
			Destroy(hit.gameObject);
			Destroy(gameObject);
		} else if ((gameObject.tag=="amissile") && (hit.gameObject.CompareTag("alien"))) {
			Destroy(gameObject);
		}

	}

	/*
	void OnTriggerEnter(Collider other) {
		Debug.Log("Triggered A Thing");
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
	*/
}
