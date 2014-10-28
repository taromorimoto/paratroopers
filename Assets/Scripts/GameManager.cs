using UnityEngine;
using System.Collections;
using System.Collections.Generic; // in case of using lists
using UnityEngine.UI; // for enabling UI -elements

public class GameManager : MonoBehaviour {

	public bool introLogoOn;
	public GameObject logoCover;
	public GameObject logo;
	public GameObject IntroText;
	public GameObject InfoText;
	public GameObject BlackBG;
	float timer;
	float logoTicktime = 0.15f;
	float logoTimeSinceTick;
	int logoLetterCounter = 0;
	int logoLetterCount = 11;
	Text splashScreenText;
	Text infoScreenText;
	bool introShown;
	public GameObject EnemySpawnHolder;
	EnemySpawner em; 

	void PlayIntroMusic () {
		Debug.Log ("Play The theme!");

		}


	// Use this for initialization
	void Start () {
		introLogoOn = true;
		introShown = false;
		InfoText.active = false;

	}
	
	// Update is called once per frame
	void Update () {

		if (introLogoOn == true) {
			logoTimeSinceTick += Time.deltaTime;
			if (logoTimeSinceTick > logoTicktime) {
				logoTimeSinceTick -= logoTimeSinceTick;

				var newPos = logoCover.transform.position;
				newPos.x += 40.0f;
				logoCover.transform.position = newPos;
				logoLetterCounter += 1;
				Debug.Log ("Revealed a letter");
				}
		if (logoLetterCounter > logoLetterCount && introShown == false)  {
				introLogoOn = false;
				logoCover.active =false;
				PlayIntroMusic();
				splashScreenText = IntroText.GetComponent<Text>();
				splashScreenText.text = "\n by \n Greg Kuperberg" + 
					"\n\n  2014 remake by" +
						"\n Taro Morimoto, Kristian Pernilä," +
						"\n Juuso Patrikainen & Antti Ruonala " +
						"\n\n\n PRESS ´I'  FOR INSTRUCTIONS " +
						"\n PRESS space bar FOR KEYBOARD PLAY " +
						"\n OR joystic button FOR JOYSTICK adjustment " +
						"\n\n (C) 1982 ORION SOFTWARE. INC. ";
			introShown = true;
				//notificationText = notificationTextObject.GetComponent<Text>();
				//scoreText = scoreTextObject.GetComponent<Text>();
				//SetScoreText();
			}



		}
		if(Input.GetKeyDown(KeyCode.I) && introShown == true){

			Debug.Log("Inforuutu");

			IntroText.active =false;
			logo.active = false;
			InfoText.active = true;

	}

		if (Input.GetKeyDown (KeyCode.Space) && introShown == true) {
			InfoText.active =false;
			logoCover.active =false;
			IntroText.active =false;
			logo.active = false;
			BlackBG.active = false;
			introShown = true;

			em = EnemySpawnHolder.GetComponent<EnemySpawner>();
			em.gameStarted = true;






				}

	}
}
