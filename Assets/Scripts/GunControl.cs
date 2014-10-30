using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class GunControl : MonoBehaviour {

	public Transform gunBarrelEnd;
	public GameObject projectile;
	public int projectileVelocity = 2000;
	public float turnSpeed = 2;
	public int turnAngleLimit = 80;
	public int fireDelayBase = 100;
	public int fireDelayContinousBegin = 400;
	public int fireDelayContinousEnd = 400;
	public GameObject deathAnimation;


	GameObject barrel;
	Vector3 direction;
	float velocity = 0;
	Stopwatch fireTimer = new Stopwatch();
	Stopwatch fireEndTimer = new Stopwatch();
	long fireDelay;
	long endFireDelay;
	bool firing = false;
	int bulletCount = 0;
    bool gameOver = false;


	void Start () {
		barrel = this.transform.FindChild("GunBarrel").gameObject;
	}

	void shoot() {
		audio.Play();
		GameObject projectileInstance = (GameObject)Instantiate(projectile, gunBarrelEnd.position, gunBarrelEnd.rotation);
		projectileInstance.GetComponent<Rigidbody2D>().AddForce(gunBarrelEnd.up * projectileVelocity);
		bulletCount++;
		GameObject.Find("GameManager").BroadcastMessage("ModifyScore", -1);
	}
	
	void Update () {
        if (gameOver) return;
    
		bool fireKeyDown = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow);

		// Start firing
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) {
			shoot();
			velocity = 0;
			fireTimer.Reset();
			fireTimer.Start();
			fireDelay = fireDelayContinousBegin;
			firing = true;
		}

		// Handle ending fire
		if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow)) {
			if (bulletCount == 1) {
				endFireDelay = fireDelayBase;
				firing = false;
				bulletCount = 0;
				print("Stopped single fire");
			} else {
				fireEndTimer.Reset();
				fireEndTimer.Start();
				endFireDelay = fireDelayContinousEnd;
			}
		}

		// Check if continous firing has ended after a delay even though no keys are pressed anymore
		if (bulletCount > 1 && fireEndTimer.ElapsedMilliseconds > endFireDelay) {
			fireEndTimer.Stop();
			fireEndTimer.Reset();
			bulletCount = 0;
			firing = false;
			print("Stopped continuos firing");
		}

		// Check for continous fire shots
		if (firing && fireTimer.ElapsedMilliseconds > fireDelay) {
			print("continous");
			print(fireDelay);
			fireTimer.Reset();
			fireTimer.Start();
			fireDelay = fireDelayBase;
			shoot();
		}

		// Check for turning the barrel
		if (!firing && !fireKeyDown) {
			if (Input.GetKey(KeyCode.LeftArrow)) {
				velocity = turnSpeed;
			} else if (Input.GetKey(KeyCode.RightArrow)) {
				velocity = -turnSpeed;
			}
		}

		// Turn the barrel
		direction.z += velocity;

		if (direction.z > turnAngleLimit) {
			direction.z = turnAngleLimit;
			velocity = 0;
		}
		if (direction.z < -turnAngleLimit) {
			direction.z = -turnAngleLimit;
			velocity = 0;
		}

		// Rotate the actual transform
		barrel.transform.localEulerAngles = direction;
	}
    
    public void ExplodeAndDie() {
        gameObject.transform.FindChild("GunBarrel").gameObject.SetActive(false);
        gameObject.transform.FindChild("GunHolder").gameObject.SetActive(false);
        
        // Death explosion animation
        GameObject ps = (GameObject)Instantiate(deathAnimation);
        ps.transform.position = transform.position + new Vector3(0, 2);
        //ps.GetComponent<Rigidbody2D>().AddForce(transform.up * 1300.0f);
        
        GameObject.Find("GameManager").BroadcastMessage("GameOver", "Gun Turret has exloded.");
        gameOver = true;
    }

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Bomb") {
            Destroy(other.gameObject);
            
            ExplodeAndDie();
		}
	}
	

}
