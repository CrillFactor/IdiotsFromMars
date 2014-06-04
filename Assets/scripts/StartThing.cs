using UnityEngine;
using System.Collections;

// IDIOTS FROM MARS
// uses font: RENEGADE MASTER by Chaos Fonts available at 1001freefonts.com
public class StartThing : MonoBehaviour {
	public Texture2D controlTexture;
	public static int score = 0;
	public static int highscore = 0;
	public static int wave = 0;
	public float delay_per_alien = 0.025F;
	private float delay = 0.0F;
	public int g_rows = 1;
	public int b_rows = 2;
	public int r_rows = 2;
	public int a_cols = 9;
	public static int tot_aliens = 45;
	public float row_spacing = 1.3F;
	public float speed_sidestep = 0.3F;
	public float speed_advance = 0.7F;
	private int tot_rows;
	private int i, j;
	private float xo, yo;
	public static int aliens_remaining = 0;
	private Vector2 new_position;
	private int cur_direction = 1;
	private int next_direction = 1;
	public GameObject greenguy;
	public GameObject blueguy;
	public GameObject redguy;
	public GameObject alieneye;
	public GameObject playership;
	private GameObject[] alienarray;
	public static int game_running = 0;
	private int wait_to_respawn = 0;
	public GUIStyle styledefault;

	// Use this for initialization
	void Start () {
	//	LaunchShip();
	//	LaunchAliens();
	}
	
	// Update is called once per frame
	void Update () {
		if (game_running == 1) {
//			Debug.Log(delay);
			delay += Time.deltaTime;
			if ((delay > (delay_per_alien * aliens_remaining))) {
				// ALIEN MOVEMENT GOES HERE
				alienarray = GameObject.FindGameObjectsWithTag("alien");
				foreach (GameObject dude in alienarray) {
					xo = dude.transform.position.x;
					yo = dude.transform.position.y;
					xo += (speed_sidestep * cur_direction);
					if (cur_direction == 0){
						yo -= speed_advance;
					}
					if (cur_direction == next_direction){
						if ((xo < -8.0F) && (cur_direction == -1)){
							// if even a single alien is over the line,
							// SET NEXT DIRECTION TO 1 (POSITIVE)
							next_direction = 1;
						}
						else if ((xo > 8.0F) && (cur_direction == 1)){
							// SET NEXT DIRECTION TO -1 (NEGATIVE)
							next_direction = -1;
							Debug.Log(xo);
						}
					}
					dude.transform.position = new Vector2(xo,yo);

					foreach (Transform child in dude.transform) {
						child.rigidbody2D.AddForce(new Vector2(Random.Range(-8.0F,8.0F),Random.Range(-8.0F,8.0F)),ForceMode2D.Impulse);
					}

				}
				if (cur_direction != next_direction){
					cur_direction += next_direction;
				}
				delay = 0;
				// RANDOMLY ADD FORCE TO CHILD OBJECTS (EYES)
			}
			// CREATE A NEW FLEET IF THE OLD ONE IS GONE
			if (aliens_remaining == 0)
			{
				wait_to_respawn++;
				if (wait_to_respawn > 100) {
					
					delay = 0;
					LaunchAliens();
					// OPTIONALLY: decrease delay_per_alien
					delay_per_alien -= 0.0025F;
					if (delay_per_alien < 0.005F) {
						delay_per_alien = 0.0025F;
						speed_sidestep += 0.05F;
					}
					// OPTIONALLY: increase speed_advance
					// OPTIONALLY: increase speed_sidestep
					// OPTIONALLY: increment a "wave"
					wait_to_respawn = 0;
				}
			}
		}
	}
	
	void LaunchAliens () {
		destroyAllAliens();
		tot_rows = g_rows + b_rows + r_rows;
		tot_aliens = tot_rows * a_cols;
		aliens_remaining = tot_aliens;
		cur_direction = 1;
		next_direction = 1;
		for (i = 1; i <= tot_rows; i++) {
			for (j = 1; j <= a_cols; j++) {
				xo = transform.position.x;
				xo += (j * row_spacing);
				yo = transform.position.y;
				yo -= (i * row_spacing);
				new_position.Set(xo,yo);
				if (i <= g_rows) {
					GameObject greenThis;
					GameObject eyeThis;
					greenThis = Instantiate (greenguy, new_position, transform.rotation) as GameObject;
					eyeThis = Instantiate (alieneye, new Vector2(xo, yo), transform.rotation) as GameObject;
					greenThis.GetComponent<DistanceJoint2D>().connectedBody = eyeThis.rigidbody2D;
					eyeThis.transform.parent = greenThis.transform;
					//eyeThis.rigidbody2D.gravityScale = Random.Range(0.5F,5.0F);
				} else if (i <= g_rows + b_rows) {
					// Instantiate (blueguy, new_position, transform.rotation);
					GameObject blueThis, eye1This, eye2This;
					blueThis = Instantiate (blueguy, new_position, transform.rotation) as GameObject;
					eye1This = Instantiate (alieneye, new Vector2(xo, yo), transform.rotation) as GameObject;
					eye2This = Instantiate (alieneye, new Vector2(xo, yo), transform.rotation) as GameObject;
					blueThis.GetComponents<DistanceJoint2D>()[0].connectedBody = eye1This.rigidbody2D;
					blueThis.GetComponents<DistanceJoint2D>()[1].connectedBody = eye2This.rigidbody2D;
					eye1This.transform.parent = blueThis.transform;
					eye2This.transform.parent = blueThis.transform;
					//eye1This.rigidbody2D.gravityScale = Random.Range(0.5F,5.0F);
					//eye2This.rigidbody2D.gravityScale = Random.Range(0.5F,5.0F);
				} else {
					GameObject redThis, eye1This, eye2This;
					redThis = Instantiate (redguy, new_position, transform.rotation) as GameObject;
					eye1This = Instantiate (alieneye, new Vector2(xo, yo), transform.rotation) as GameObject;
					eye2This = Instantiate (alieneye, new Vector2(xo, yo), transform.rotation) as GameObject;
					redThis.GetComponents<DistanceJoint2D>()[0].connectedBody = eye1This.rigidbody2D;
					redThis.GetComponents<DistanceJoint2D>()[1].connectedBody = eye2This.rigidbody2D;
					eye1This.transform.parent = redThis.transform;
					eye2This.transform.parent = redThis.transform;
					//eye1This.rigidbody2D.gravityScale = Random.Range(0.5F,5.0F);
					//eye2This.rigidbody2D.gravityScale = Random.Range(0.5F,5.0F);
				}
			}
		}
		game_running = 1;
	}
	void LaunchShip() {
		GameObject ship;
		new_position.x = -0.04972434F;
		new_position.y = -4.380289F;
		ship = Instantiate (playership, new_position, transform.rotation) as GameObject;
	}
	private void OnGUI() {
//		GUI.Label(new Rect(10,10,100,90), score.ToString(),styledefault);
		GUI.Label(new Rect(10,10,100,90), score.ToString(),styledefault);
//		GUI.Label(new Rect(110,10,100,90), bricks_destroyed.ToString(),styledefault);
//		GUI.Label(new Rect(210,10,100,90), life_lost.ToString(),styledefault);
		//GUI.Label(new Rect(Screen.width-350,10,350,90),"idiots from space",styledefault);

		if (game_running == -1) {
			GUI.Button(new Rect((Screen.width/2.0f)-40,10,120,120),"game over",styledefault);
			GUI.Label (new Rect ((Screen.width/2.0f)-175,(Screen.height)-100, 300,100), "press m for menu", styledefault);
			bool rDown = Input.GetKeyDown("m");
			if (rDown) {
				destroyAllAliens();
				game_running = 0;
			}
		}
		if (game_running == 0) {
			// REPLACE WITH MAIN SCREEN GRAPHICS
			GUI.Label (new Rect ((Screen.width/2.0f)-100,(Screen.height/2.0f)-100,200,200), controlTexture);
			GUI.Label (new Rect ((Screen.width/2.0f)-175,(Screen.height)-50, 300,100), "press fire to start", styledefault);
			bool fDown = Input.GetButtonDown("Fire1");
			if (fDown) {
				score = 0;
				LaunchShip();
				LaunchAliens();
			}
		}
	}

	void destroyAllAliens() {
		// GET RID OF REMAINING ALIENS
		alienarray = GameObject.FindGameObjectsWithTag("alien");
		foreach (GameObject dude in alienarray) {
			Destroy(dude);
		}
	}
}
