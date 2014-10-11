using UnityEngine;
using System.Collections;

public class Paratrooper : MonoBehaviour {


	// I suck at programming, feel free to do changes or delete all.
	// Love, Juuso


	public GameObject trooper;
	public GameObject parachute;
	
	Animator animator;

	bool parachuteEnabled;
	bool isOnGround;
	float timer;
	float parachuteOpenDelay;


	// Use this for initialization
	void Start () {

		animator = trooper.GetComponent<Animator>();

		parachuteEnabled = false;
		isOnGround = false;
		timer = 0;
		parachuteOpenDelay = 0.5f + Random.value;

	}
	
	// Update is called once per frame
	void Update () {


		//	You can call animations Walk (boolean) and Death (trigger) with these:

		//		animator.SetBool("Walk",true);
		//		animator.SetTrigger("Death");



		timer += Time.deltaTime;

		if (timer >= parachuteOpenDelay)
			parachuteEnabled = true;

		if (Input.GetKeyDown(KeyCode.Space))
			parachuteEnabled = !parachuteEnabled;

	
		if (parachuteEnabled && !isOnGround) {
			rigidbody2D.drag = 2.0f;
			parachute.SetActive(true);
		} else {
			rigidbody2D.drag = 0.0f;
			parachute.SetActive(false);
		}

	}

	void OnCollisionEnter2D(Collision2D c) {
		isOnGround = true;
	}
}
