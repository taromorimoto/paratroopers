using UnityEngine;
using System.Collections;

public class TrooperGroundManager : MonoBehaviour {

    bool gameOver = false;

	void Update () {
        if (gameOver) return;
    
        int leftCount = 0;
        int rightCount = 0;
        
        GameObject[] paratroopers = GameObject.FindGameObjectsWithTag("Paratrooper");
        for (int i = 0; i < paratroopers.Length; i++) {
            var para = paratroopers[i].GetComponent<Paratrooper>();
            if (para && para.HasLandedOnLeft()) {
                leftCount++;
                continue;
            }
            if (para && para.HasLandedOnRight()) {
                rightCount++;
                continue;
            }
        }
        if (leftCount >= 4 || rightCount >= 4) {
            GameObject.Find("GameManager").BroadcastMessage("GameOver", "Too many troopers has landed.");
            gameOver = true;
        }
	}
}
