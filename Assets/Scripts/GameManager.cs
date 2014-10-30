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
	public GameObject GameOverTextHolder;
	Text gameOverText;
	float timer;
	float logoTicktime = 0.15f;
	float logoTimeSinceTick;
	int logoLetterCounter = 0;
	int logoLetterCount = 11;
	Text splashScreenText;
	Text infoScreenText;
	bool introShown;
	public bool menuVisible;

	//these are for messaging the EnemySpawner that the game can start
	public GameObject EnemySpawnHolder;
	EnemySpawner em; 

	//highscore variables
	public GameObject scoreTextObject;
	public GameObject highScoreHolderPrefab;
	GameObject highScoreHolder;
	Text scoreText;
	HighScore hs;
	int score;

	void TurnMenuVisible(){
		menuVisible = true;
	}

	void ModifyScore(int scoreChange){
		score += scoreChange;
		if (score < 0) {
			score = 0;
		}

		if (score > hs.currentHighScore) {
		hs.currentHighScore = score;
		}

		SetScoreText();
	}



	void SetScoreText() {
		scoreText.text = " SCORE: \t\t<color=magenta>" + score + "</color>\t\t\t\t HI-SCORE: \t\t" + hs.currentHighScore;
	}


	void PlayIntroMusic () {
		Debug.Log ("Play The theme!");

		}


	// Use this for initialization
	void Start () {
		menuVisible = true;
		introLogoOn = true;
		introShown = false;
		InfoText.active = false;

		highScoreHolder = GameObject.Find("HighScoreHolder(Clone)");
		if (highScoreHolder == null) {
			highScoreHolder = (GameObject)Instantiate(highScoreHolderPrefab);
		}
		hs = highScoreHolder.GetComponent<HighScore>();
		scoreText = scoreTextObject.GetComponent<Text>();
		SetScoreText();
	
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
						"\n" +
						"\n\n (C) 1982 ORION SOFTWARE. INC. ";
			introShown = true;
			
			}
		}


		if(Input.GetKeyDown(KeyCode.I) && introShown == true && menuVisible){

			Debug.Log("Inforuutu");
			BlackBG.active = true;
			IntroText.active =false;
			logo.active = false;
			InfoText.active = true;

			//spagetti-purkkaa. Käsittelen gameover-logiikan GameOverTextHandler scriptissä. 
			//Fiksaa sen et game-over ei näy kun painaa infotekstiä, mut itse scrpti on aktiivinen.
			gameOverText = GameOverTextHolder.GetComponent<Text>();
			gameOverText.text = " ";
			}


		if (Input.GetKeyDown (KeyCode.Space) && introShown == true) {
			InfoText.active =false;
			logoCover.active =false;
			IntroText.active =false;
			logo.active = false;
			BlackBG.active = false;
			scoreText.active = true;

			introShown = true;

			EnemySpawnHolder.SetActive(true);
			menuVisible = false;
			//em = EnemySpawnHolder.GetComponent<EnemySpawner>();
			//em.gameStarted = true;
				
		}
	}




}