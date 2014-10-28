using UnityEngine;
using System.Collections;
using UnityEngine.UI; // for enabling UI -elements

public class GameOverTextHandler : MonoBehaviour {

	Text gameOverText;
	bool isGameOver = false;


	// Use this for initialization
	void Start () {

		//gameObject.active = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) && isGameOver == true) { 
			Application.LoadLevel (0);
		}
	
	}


	void GameOver(){
		gameOverText = gameObject.GetComponent<Text>();
		gameOverText.text = "GAME OVER \n PRESS 'I' FOR INSTRUCTIONS \n PRESS space bar FOR KEYBOARD PLAY \n OR PRESS 'J' FOR JOYSTIC PLAY";
			isGameOver = true;
			

		//BackGroundPanel.active = true;
		//gameObject.active = true;

	}

}
