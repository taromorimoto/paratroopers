using UnityEngine;
using System.Collections;

public class FlipObject : MonoBehaviour {

	public bool facingRight = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
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
