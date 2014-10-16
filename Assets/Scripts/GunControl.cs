using UnityEngine;
using System.Collections;

public class GunControl : MonoBehaviour {

	public Transform gunBarrelEnd;
	public GameObject projectile;
	public int projectileVelocity = 3500;

	GameObject barrel;
	Vector3 direction;

	void Start () {
		barrel = this.transform.FindChild("GunBarrel").gameObject;
	}
	
	void Update () {
		if (Input.GetKey (KeyCode.LeftArrow)) {
			direction.z += 2;
			if (direction.z > 90) direction.z = 90;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			direction.z -= 2;
			if (direction.z < -90) direction.z = -90;
		} else {
			direction.z *= 0.9f;
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			GameObject projectileInstance = (GameObject)Instantiate(projectile, gunBarrelEnd.position, gunBarrelEnd.rotation);
			projectileInstance.GetComponent<Rigidbody2D>().AddForce(gunBarrelEnd.up * projectileVelocity);
		}
		
		barrel.transform.localEulerAngles = direction;
	}
}
