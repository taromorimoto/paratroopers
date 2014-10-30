using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrooperGroundManager : MonoBehaviour {

    bool gameOver = false;
    Vector3 direction = new Vector3(0, 0, 0);
    Paratrooper attacker;
    
    void MoveAttacker(float x, float y) {
        attacker.transform.position += new Vector3(x, y) * Time.deltaTime * 10;
    }

	void Update() {
        if (attacker) {
            // This is a superhack. I know.
            
            float x = Mathf.Abs(attacker.transform.position.x);
            float y = attacker.transform.position.y;

            if (x < 1.6f) {
                // Attacker has hit the turret
                GunControl gun = GameObject.Find("Gun").GetComponent<GunControl>();
                gun.ExplodeAndDie();
                gameObject.SetActive(false);
                attacker.gameObject.GetComponent<Rigidbody2D>().gravityScale = 5;
                attacker.gameObject.GetComponent<Rigidbody2D>().drag = 0;
                attacker.gameObject.GetComponent<Rigidbody2D>().fixedAngle = false;
                attacker.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(-direction.x * 30, 30), ForceMode2D.Impulse);
                attacker.gameObject.GetComponent<Rigidbody2D>().AddTorque(200*direction.x);
                direction.x = 0;
                
            } else if (x >= 4.6f && y < 0.1f) {
                // Move towards turret on ground
                attacker.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                MoveAttacker(direction.x, 0);
            } else if (x < 4.6f && y <= 6) {
                // Move up next to turret
                attacker.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                MoveAttacker(0, 1);
            } else if (y > 6) {
                // Move towards turret on the turret
                MoveAttacker(direction.x, 0);
            }
        }
    
        if (gameOver) {
            return;
        }
    
        List<Paratrooper> left = new List<Paratrooper>();
        List<Paratrooper> right = new List<Paratrooper>();
        
        GameObject[] paratroopers = GameObject.FindGameObjectsWithTag("Paratrooper");
        for (int i = 0; i < paratroopers.Length; i++) {
            Paratrooper para = paratroopers[i].GetComponent<Paratrooper>();
            if (para && para.HasLandedOnLeft()) {
                left.Add(para);
                continue;
            }
            if (para && para.HasLandedOnRight()) {
                right.Add(para);
                continue;
            }
        }
        if (left.Count >= 4) {
            gameOver = true;
            attacker = FindAttacker(left);
            direction.x = 1;
        }
        if (right.Count >= 4) {
            gameOver = true;
            attacker = FindAttacker(right);
            direction.x = -1;
        }
    }
    
    Paratrooper FindAttacker(List<Paratrooper> paratroopers) {
        float dist = float.MaxValue;
        Paratrooper closest = null;
        
        for (int i = 0; i < paratroopers.Count; i++) {
            float x = Mathf.Abs(paratroopers[i].transform.position.x);
            float y = paratroopers[i].transform.position.y;
            if (x < dist && y < 0.1f) {
                dist = x;
                closest = paratroopers[i];
            }
        }
        closest.animator.SetBool("Walk",true);
        
        print("GameOver is coming. Too many troopers has landed.");
        return closest;
    }
}


