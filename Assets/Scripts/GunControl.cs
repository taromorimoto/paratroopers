using UnityEngine;
using System.Collections;

public class GunControl : MonoBehaviour {

	GameObject barrel;
	Vector3 direction;

	// Use this for initialization
	void Start () {
		barrel = this.transform.FindChild("GunBarrel").gameObject;
	}
	
	// Update is called once per frame
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
		barrel.transform.localEulerAngles = direction;
	}
}
