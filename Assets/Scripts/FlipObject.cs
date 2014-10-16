using UnityEngine;
using System.Collections;

public class FlipObject : MonoBehaviour {

	public bool facingRight = true;

	void Start () {
	
	}
	
	void Update () {
		if (facingRight) {
			transform.localScale = new Vector3(1,1,1);
		} else {
			transform.localScale = new Vector3(-1,1,1);
		}
	}

	public void flip() {
		facingRight = !facingRight;
	}
}
