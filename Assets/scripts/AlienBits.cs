using UnityEngine;
using System.Collections;

public class AlienBits : MonoBehaviour {

	public GameObject alien_bullet;
	// Use this for initialization
	private int fired = 0;

	
	void Start () {
//		Debug.Log("Started");
	}
	
	// Update is called once per frame
	void Update () {
		// the %chance of NOT firing are: 69.95 + (aliens remaining / total aliens * 30)
		if (fired > 0) {
			fired --;
		}
		else {
			if (StartThing.game_running == -1) {
				foreach (Transform child in transform) {
					child.rigidbody2D.AddForce(new Vector2(Random.Range(-16.0F,16.0F),Random.Range(-16.0F,16.0F)),ForceMode2D.Impulse);
					fired = 25;
				}
			} else { 
				float pct_left = (float)StartThing.aliens_remaining / (float)StartThing.tot_aliens;
				float fire_chance =  94.95F + (pct_left * 5.0F);
				float roll = Random.Range(0.0F,100.0F);
				if (roll > fire_chance) { // alien wanna shoot something
					if (StartThing.game_running == -1) {

					} else {
						RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1), -Vector2.up);
						if (hit.collider != null) {
							if (hit.collider.tag == "Player") {
								Instantiate (alien_bullet, new Vector2 (transform.position.x, transform.position.y - 1), transform.rotation);
								fired = 30;
							}
						} else {
							Instantiate (alien_bullet, new Vector2 (transform.position.x, transform.position.y - 1), transform.rotation);
							fired = 30;
						}
					}
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D hit) {
//		Debug.Log("Hit A Thing");
		if (hit.gameObject.CompareTag("pmissile")) {
		//if (hit.gameObject.tag == "pmissile") {
			StartThing.aliens_remaining --;
			StartThing.score += 50;
			Destroy(hit.gameObject);
			Destroy(gameObject);
		}
	}

}
