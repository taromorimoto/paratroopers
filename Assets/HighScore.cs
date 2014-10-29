using UnityEngine;
using System.Collections;

public class HighScore : MonoBehaviour {

	public int currentHighScore;
	
	void Start () {
		DontDestroyOnLoad(gameObject);
	}	
}
