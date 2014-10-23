using UnityEngine;
using System.Collections;

public class Paratrooper : MonoBehaviour {

	// I suck at programming, feel free to do changes or delete all.
	// Love, Juuso

	public GameObject trooper;
	public GameObject parachute;
	public float parachuteDrag = 2.5f;
	public float parachuteDelayMin = 0.5f;
	public float parachuteDelayMax = 1.5f;

	//Animator animator;
	bool fallingWithoutParachute = true;
	bool isOnGround = false;
	float elapsed = 0;
	float parachuteOpenDelay;

	void Start () {

		//animator = trooper.GetComponent<Animator>();
		parachuteOpenDelay = Random.Range(parachuteDelayMin, parachuteDelayMax);
		rigidbody2D.drag = 0;
		parachute.SetActive(false);
	}

	void Update () {

		//	You can call animations Walk (boolean) and Death (trigger) with these:
		//		animator.SetBool("Walk",true);
		//		animator.SetTrigger("Death");

		elapsed += Time.deltaTime;

		if (!isOnGround && fallingWithoutParachute && elapsed > parachuteOpenDelay) {
			fallingWithoutParachute = false;
			rigidbody2D.drag = parachuteDrag;
			parachute.SetActive(true);
			print ("Parachute opened");
		}

	}

	void OnCollisionEnter2D(Collision2D c) {
		if (c == null) return;

		if (c.gameObject.name == "Ground") {
			isOnGround = true;
			parachute.SetActive(false);
			print("Paratrooper has landed");
			return;
		}

		Projectile projectile = c.gameObject.GetComponent<Projectile>();
		if (projectile != null) {
			Destroy(gameObject);
			print("Paratrooper hit and destroyed");
		}
	}
}
