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
	bool hasLanded = false;
	float elapsed = 0;
	float parachuteOpenDelay;
	bool hasParachute = true;
	
	Paratrooper paratrooperBelow = null;

	void Start () {

		//animator = trooper.GetComponent<Animator>();
		parachuteOpenDelay = Random.Range(parachuteDelayMin, parachuteDelayMax);
		CloseParachute();
	}

	void Update () {

		//	You can call animations Walk (boolean) and Death (trigger) with these:
		//		animator.SetBool("Walk",true);
		//		animator.SetTrigger("Death");

		elapsed += Time.deltaTime;

		if (!hasLanded && hasParachute && !parachute.activeSelf && elapsed > parachuteOpenDelay) {
			rigidbody2D.drag = parachuteDrag;
			parachute.SetActive(true);
			print ("Parachute opened");
		}
	}
	
	void CloseParachute() {
		rigidbody2D.drag = 0;
		parachute.SetActive(false);
	}
	
	public void ParachuteHit() {
		CloseParachute();
		hasParachute = false;
		print("Parachute hit");
	}
	
	public void KillAndDieByFalling() {
		if (paratrooperBelow != null) {
			paratrooperBelow.KillAndDieByFalling();
		}
		Destroy(gameObject);
		print("Paratrooper killed another paratrooper");
	}
	
	void OnCollisionEnter2D(Collision2D c) {
		if (c == null || hasLanded) return;

		if (c.gameObject.name == "Ground") {
			if (parachute.activeSelf) {
				hasLanded = true;
				parachute.SetActive(false);
				print("Paratrooper has landed");
			} else {
				Destroy(gameObject);
				print("Paratrooper killed when landing without parachute");
			}
			return;
		}

		Paratrooper otherParatrooper = c.gameObject.GetComponent<Paratrooper>();
		if (otherParatrooper) {
			hasLanded = true;
			paratrooperBelow = otherParatrooper;
			if (parachute.activeSelf) {
				parachute.SetActive(false);
				print("Paratrooper has landed safely on top of another paratrooper");
			} else {
				KillAndDieByFalling();
			}
			return;
		}

		Projectile projectile = c.gameObject.GetComponent<Projectile>();
		if (projectile) {
            Destroy(gameObject);
            Destroy(c.gameObject);
			print("Paratrooper hit and destroyed");
		}
	}
}
