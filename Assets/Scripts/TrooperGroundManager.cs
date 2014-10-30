using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrooperGroundManager : MonoBehaviour {

    bool gameOver = false;
    List<Paratrooper> attackers = null;
    
    void MoveAttacker(Paratrooper attacker, float x, float y) {
        attacker.transform.position += new Vector3(x, y) * Time.deltaTime * 10;
    }
    
    bool UpdateAttacker(Paratrooper attacker) {
        // This is a superhack. I know.
        
        float x = Mathf.Abs(attacker.transform.position.x);
        float y = attacker.transform.position.y;
        
        if (x < 1.6f) {
            // Attacker has hit the turret
            GunControl gun = GameObject.Find("Gun").GetComponent<GunControl>();
            gun.ExplodeAndDie();
            attacker.gameObject.GetComponent<Rigidbody2D>().gravityScale = 5;
            attacker.gameObject.GetComponent<Rigidbody2D>().drag = 0;
            attacker.gameObject.GetComponent<Rigidbody2D>().fixedAngle = false;
            attacker.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(-attacker.move.x * 30, 30), ForceMode2D.Impulse);
            attacker.gameObject.GetComponent<Rigidbody2D>().AddTorque(200*attacker.move.x);
            attacker.move.x = 0;
            return false;
            
        } else if (x >= 4.6f && y < 0.1f) {
            // Move towards turret on ground
            attacker.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            MoveAttacker(attacker, attacker.move.x, 0);
        } else if (x < 4.6f && y <= 6) {
            // Move up next to turret
            attacker.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            MoveAttacker(attacker, 0, 1);
        } else if (y > 6) {
            // Move towards turret on the turret
            MoveAttacker(attacker, attacker.move.x, 0);
        }
        return true;
    }
    
    void Update() {
        if (attackers != null) {
            attackers = attackers.FindAll(UpdateAttacker);

            if (attackers.Count == 0) {
                gameObject.SetActive(false);
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
            attackers = SetupAttackers(left, 1);
        }
        if (right.Count >= 4) {
            gameOver = true;
            attackers = SetupAttackers(right, -1);
        }
    }
    
    List<Paratrooper> SetupAttackers(List<Paratrooper> paratroopers, float direction) {
        for (int i = 0; i < paratroopers.Count; i++) {
            paratroopers[i].animator.SetBool("Walk",true);
            paratroopers[i].move.x = direction;
        }
        return paratroopers;
    }
}


