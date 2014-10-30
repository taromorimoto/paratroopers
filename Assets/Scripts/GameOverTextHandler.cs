using UnityEngine;
using System.Collections;
using UnityEngine.UI; // for enabling UI -elements

public class GameOverTextHandler : MonoBehaviour {

	Text gameOverText;
	bool isGameOver = false;
    float elapsedAfterGameOver = 0;

	void Update () {
    
        if (isGameOver) {
            elapsedAfterGameOver += Time.deltaTime;
        }
    
		if (Input.GetKeyDown (KeyCode.Space) && isGameOver && elapsedAfterGameOver > 2) { 
			Application.LoadLevel (0);
		}
	}

	void GameOver(){
		gameOverText = gameObject.GetComponent<Text>();
		GameObject.Find("GameManager").BroadcastMessage("TurnMenuVisible");
		gameOverText.text = "GAME OVER \n PRESS 'I' FOR INSTRUCTIONS \n PRESS space bar FOR KEYBOARD PLAY";
		isGameOver = true;
			

		//BackGroundPanel.active = true;
		//gameObject.active = true;
	}
}
