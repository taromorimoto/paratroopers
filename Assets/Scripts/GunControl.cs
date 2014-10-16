using UnityEngine;
using System.Collections;

public class GunControl : MonoBehaviour {

	public Transform gunBarrelEnd;
	public GameObject projectile;
	public int projectileVelocity = 2000;
	public float turnSpeed = 2;
	public int turnAngleLimit = 80;

	GameObject barrel;
	Vector3 direction;
	float velocity = 0;

	void Start () {
		barrel = this.transform.FindChild("GunBarrel").gameObject;
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) {
			GameObject projectileInstance = (GameObject)Instantiate(projectile, gunBarrelEnd.position, gunBarrelEnd.rotation);
			projectileInstance.GetComponent<Rigidbody2D>().AddForce(gunBarrelEnd.up * projectileVelocity);
			velocity = 0;
		} else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			velocity = +turnSpeed;
		} else if (Input.GetKeyDown(KeyCode.RightArrow)) {
			velocity = -turnSpeed;
		}

		direction.z += velocity;

		if (direction.z > turnAngleLimit) {
			direction.z = turnAngleLimit;
			velocity = 0;
		}
		if (direction.z < -turnAngleLimit) {
			direction.z = -turnAngleLimit;
			velocity = 0;
		}

		barrel.transform.localEulerAngles = direction;
	}
}
